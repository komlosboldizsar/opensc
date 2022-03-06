using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DragDrop
{

    public class SystemObjectCompositeDropAdapter<TReceiverParent, TReceiverChild>
        where TReceiverParent : Control
        where TReceiverChild : class
    {

        public SystemObjectCompositeDropAdapter(CompositeReceiverChildSelectorDelegate receiverChildSelector, CompositeReceiverDragDesponderDelegate receiverDragDesponder, CompositeReceiverValueSetterDelegate receiverValueSetter, TReceiverChild receiverChildNull)
        {
            this.receiverChildSelector = receiverChildSelector;
            this.receiverDragDesponder = receiverDragDesponder;
            this.receiverValueSetter = receiverValueSetter;
            this.receiverChildNull = receiverChildNull;
        }

        public delegate TReceiverChild CompositeReceiverChildSelectorDelegate(TReceiverParent receiverParent, DragEventArgs eventArgs);
        protected CompositeReceiverChildSelectorDelegate receiverChildSelector { get; init; }
        public delegate bool CompositeReceiverDragDesponderDelegate(TReceiverParent receiverParent, TReceiverChild receiverChild, DragEventArgs eventArgs, object tag);
        protected CompositeReceiverDragDesponderDelegate receiverDragDesponder { get; init; }
        public delegate void CompositeReceiverValueSetterDelegate(TReceiverParent receiverParent, TReceiverChild receiverChild, ISystemObject systemObject, DragEventArgs eventArgs, object tag);
        protected CompositeReceiverValueSetterDelegate receiverValueSetter { get; init; }

        private TReceiverChild receiverChildNull;

        public void ReceiveSystemObjectDrop(TReceiverParent receiverParent, TReceiverChild receiverChild = null, object tag = null)
        {
            if (!dropDataParentDictionary.TryGetValue(receiverParent, out Dictionary<TReceiverChild, DropData> dropDataChildDictionary))
            {
                receiverParent.AllowDrop = true;
                receiverParent.Disposed += disposedHandler;
                receiverParent.DragOver += dragOverHandler;
                receiverParent.DragDrop += dragDropHandler;
                dropDataChildDictionary = new();
                dropDataParentDictionary.Add(receiverParent, dropDataChildDictionary);
            }
            if (!dropDataChildDictionary.TryGetValue(receiverChild ?? receiverChildNull, out DropData dropData))
            {
                dropData = new DropData()
                {
                    ReceiverParent = receiverParent,
                    ReceiverChild = receiverChild,
                    Tag = tag
                };
                dropDataChildDictionary.Add(receiverChild, dropData);
            }
        }

        public void FilterSystemObjectDropByType<TSystemObject>(TReceiverParent receiverParent, TReceiverChild receiverChild = null)
        {
            if (!dropDataParentDictionary.TryGetValue(receiverParent, out Dictionary<TReceiverChild, DropData> dropDataChildDictionary))
                return;
            if (!dropDataChildDictionary.TryGetValue(receiverChild ?? receiverChildNull, out DropData dropData))
                return;
            dropData.TypeFilters.Add(new TypeFilter<TSystemObject>());
        }

        private void disposedHandler(object sender, EventArgs e)
        {
            if (sender is TReceiverParent receiver)
                dropDataParentDictionary.Remove(receiver);
        }

        private void dragOverHandler(object sender, DragEventArgs eventArgs)
        {
            if (!dragEventHandler(sender, eventArgs, out DropData dropData))
                return;
        }

        private void dragDropHandler(object sender, DragEventArgs eventArgs)
        {
            if (!dragEventHandler(sender, eventArgs, out DropData dropData))
                return;
            if (!(eventArgs.Data.GetData(typeof(SystemObjectReference)) is SystemObjectReference systemObjectReference))
                return;
            ISystemObject systemObject = systemObjectReference.Object;
            receiverValueSetter(dropData.ReceiverParent, dropData.ReceiverChild, systemObject, eventArgs, dropData.Tag);
        }

        private bool dragEventHandler(object sender, DragEventArgs eventArgs, out DropData dropData)
        {
            dropData = null;
            eventArgs.Effect = DragDropEffects.None;
            if (!(sender is TReceiverParent receiverParent))
                return false;
            if (!eventArgs.Data.GetDataPresent(typeof(SystemObjectReference)))
                return false;
            if (!dropDataParentDictionary.TryGetValue(receiverParent, out Dictionary<TReceiverChild, DropData> dropDataChildDictionary))
                return false;
            TReceiverChild receiverChild = receiverChildSelector(receiverParent, eventArgs);
            if (!dropDataChildDictionary.TryGetValue(receiverChild ?? receiverChildNull, out dropData))
                return false;
            if (dropData.TypeFilters.Count > 0)
            {
                SystemObjectReference systemObjectReference = eventArgs.Data.GetData(typeof(SystemObjectReference)) as SystemObjectReference;
                ISystemObject systemObject = systemObjectReference?.Object;
                if (!dropData.TypeFilters.Any(tf => tf.Is(systemObject)))
                    return false;
            }
            if (!receiverDragDesponder(receiverParent, receiverChild, eventArgs, dropData.Tag))
                return false;
            eventArgs.Effect = DragDropEffects.Link;
            return true;
        }

        private Dictionary<TReceiverParent, Dictionary<TReceiverChild, DropData>> dropDataParentDictionary = new();

        private class DropData
        {
            public TReceiverParent ReceiverParent { get; set; }
            public TReceiverChild ReceiverChild { get; set; }
            public object Tag { get; set; }
            public List<ITypeFilter> TypeFilters { get; } = new();
        }

        private interface ITypeFilter
        {
            bool Is(object obj);
        }

        private class TypeFilter<T> : ITypeFilter
        {
            public bool Is(object obj) => obj is T;
        }

    }

}
