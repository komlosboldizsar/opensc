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
        public delegate void CompositeReceiverValueSetterDelegate(TReceiverParent receiverParent, TReceiverChild receiverChild, IEnumerable<ISystemObject> systemObjects, DragEventArgs eventArgs, object tag);
        protected CompositeReceiverValueSetterDelegate receiverValueSetter { get; init; }

        private TReceiverChild receiverChildNull;

        public void ReceiveSystemObjectDrop(TReceiverParent receiverParent, TReceiverChild receiverChild = null, object tag = null, bool enableMulti = false)
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
                    Tag = tag,
                    EnableMulti = enableMulti
                };
                dropDataChildDictionary.Add(receiverChild ?? receiverChildNull, dropData);
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
            IEnumerable<ISystemObject> systemObjects = null;
            if (eventArgs.Data.GetData(typeof(SystemObjectReference)) is SystemObjectReference singleReference)
                systemObjects = new ISystemObject[] { singleReference.Object };
            if (eventArgs.Data.GetData(typeof(SystemObjectReference[])) is SystemObjectReference[] multiReference)
                systemObjects = multiReference.Select(sro => sro.Object);
            if (systemObjects.Count() == 0)
                return;
            receiverValueSetter(dropData.ReceiverParent, dropData.ReceiverChild, systemObjects, eventArgs, dropData.Tag);
        }

        private bool dragEventHandler(object sender, DragEventArgs eventArgs, out DropData dropData)
        {
            dropData = null;
            eventArgs.Effect = DragDropEffects.None;
            if (!(sender is TReceiverParent receiverParent))
                return false;
            if (!dropDataParentDictionary.TryGetValue(receiverParent, out Dictionary<TReceiverChild, DropData> dropDataChildDictionary))
                return false;
            TReceiverChild receiverChild = receiverChildSelector(receiverParent, eventArgs);
            if (!dropDataChildDictionary.TryGetValue(receiverChild ?? receiverChildNull, out dropData))
                return false;
            bool singlePresent = eventArgs.Data.GetDataPresent(typeof(SystemObjectReference));
            bool multiPresent = eventArgs.Data.GetDataPresent(typeof(SystemObjectReference[]));
            if (!(singlePresent || (dropData.EnableMulti && multiPresent)))
                return false;
            if (dropData.TypeFilters.Count > 0)
            {
                SystemObjectReference firstSystemObjectReference = null;
                if (singlePresent)
                {
                    firstSystemObjectReference = eventArgs.Data.GetData(typeof(SystemObjectReference)) as SystemObjectReference;
                }
                else
                {
                    SystemObjectReference[] systemObjectReferences = eventArgs.Data.GetData(typeof(SystemObjectReference[])) as SystemObjectReference[];
                    if (systemObjectReferences.Length > 0)
                        firstSystemObjectReference = systemObjectReferences[0];
                }
                ISystemObject firstSystemObject = firstSystemObjectReference?.Object;
                if (!dropData.TypeFilters.Any(tf => tf.Is(firstSystemObject)))
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
            public bool EnableMulti { get; set; }
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
