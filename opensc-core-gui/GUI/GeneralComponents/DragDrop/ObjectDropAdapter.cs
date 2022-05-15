using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DragDrop
{

    public class ObjectDropAdapter<TReceiver>
        where TReceiver : Control
    {

        public static void Handle(
            CanHandleDelegate receiverCanHandle,
            DragDesponderDelegate receiverDragDesponder,
            ValueSetterDelegate receiverValueSetter)
        {
            PartedDropAdapter<NoPart>.Instance.ReceiverPartSelector = (_, _) => null;
            PartedDropAdapter<NoPart>.Instance.ReceiverCanHandle = (r, rp, ea, t) => receiverCanHandle(r, ea, t);
            PartedDropAdapter<NoPart>.Instance.ReceiverDragDesponder = (r, rp, ea, t) => receiverDragDesponder(r, ea, t);
            PartedDropAdapter<NoPart>.Instance.ReceiverValueSetter = (r, rp, so, ea, t) => receiverValueSetter(r, so, ea, t);
            PartedDropAdapter<NoPart>.Instance.EmptyPart = NoPart.EMPTY;
        }

        public static void HandleParted<TReceiverPart>(
            PartedCanHandleDelegate<TReceiverPart> receiverCanHandle,
            ReceiverPartSelectorDelegate<TReceiverPart> receiverChildSelector,
            PartedDragDesponderDelegate<TReceiverPart> receiverDragDesponder,
            PartedValueSetterDelegate<TReceiverPart> receiverValueSetter,
            TReceiverPart emptyPart)
            where TReceiverPart : class
        {
            PartedDropAdapter<TReceiverPart>.Instance.ReceiverPartSelector = receiverChildSelector;
            PartedDropAdapter<TReceiverPart>.Instance.ReceiverCanHandle = receiverCanHandle;
            PartedDropAdapter<TReceiverPart>.Instance.ReceiverDragDesponder = receiverDragDesponder;
            PartedDropAdapter<TReceiverPart>.Instance.ReceiverValueSetter = receiverValueSetter;
            PartedDropAdapter<TReceiverPart>.Instance.EmptyPart = emptyPart;
        }


        public delegate bool CanHandleDelegate(TReceiver receiverParent, DragEventArgs eventArgs, object tag);
        public delegate bool DragDesponderDelegate(TReceiver receiverParent, DragEventArgs eventArgs, object tag);
        public delegate void ValueSetterDelegate(TReceiver receiverParent, IEnumerable<object> objects, DragEventArgs eventArgs, object tag);

        public delegate TReceiverPart ReceiverPartSelectorDelegate<TReceiverPart>(TReceiver receiverParent, DragEventArgs eventArgs);
        public delegate bool PartedCanHandleDelegate<TReceiverPart>(TReceiver receiverParent, TReceiverPart receiverChild, DragEventArgs eventArgs, object tag);
        public delegate bool PartedDragDesponderDelegate<TReceiverPart>(TReceiver receiverParent, TReceiverPart receiverChild, DragEventArgs eventArgs, object tag);
        public delegate void PartedValueSetterDelegate<TReceiverPart>(TReceiver receiverParent, TReceiverPart receiverChild, IEnumerable<object> objects, DragEventArgs eventArgs, object tag);

        public static IDropSettingManager ReceiveObjectDrop(TReceiver receiver, object tag = null)
            => ReceiveObjectDropParted<NoPart>(receiver, null, tag);

        public static IDropSettingManager ReceiveObjectDropParted<TReceiverPart>(TReceiver receiver, TReceiverPart receiverPart, object tag = null)
            where TReceiverPart : class
        {
            if (!knownObjects.Contains(receiver))
            {
                receiver.AllowDrop = true;
                receiver.Disposed += disposedHandler;
                receiver.DragOver += dragOverHandler;
                receiver.DragDrop += dragDropHandler;
                knownObjects.Add(receiver);
            }
            return PartedDropAdapter<TReceiverPart>.Instance.ReceiveObjectDrop(receiver, receiverPart, tag);
        }

        public static IDropSettingManager GetDropSettingManager(TReceiver receiver)
            => PartedDropAdapter<NoPart>.Instance.GetDropSettingManager(receiver, null);

        public static IDropSettingManager GetDropSettingManager<TReceiverPart>(TReceiver receiver, TReceiverPart receiverPart)
            where TReceiverPart : class
            => PartedDropAdapter<TReceiverPart>.Instance.GetDropSettingManager(receiver, receiverPart);

        private interface IDropData { }

        public interface IDropSettingManager
        {
            IDropSettingManager FilterByType<T>();
            IDropSettingManager EnableMulti(bool enableMulti = true);
        }

        private interface IPartedDropAdapter
        {
            bool CanHandle(object sender, DragEventArgs eventArgs, ref IDropData dropData);
            bool GetResponse(object sender, DragEventArgs eventArgs, IDropData dropData);
            void SetValue(object sender, DragEventArgs eventArgs, IDropData dropData, IEnumerable<object> objects);
        }

        private class PartedDropAdapter<TReceiverPart> : IPartedDropAdapter
            where TReceiverPart : class
        {

            public static PartedDropAdapter<TReceiverPart> Instance { get; } = new();
            private PartedDropAdapter() => ObjectDropAdapter<TReceiver>.RegisterPartedAdapter(this);

            internal ReceiverPartSelectorDelegate<TReceiverPart> ReceiverPartSelector { get; set; }
            internal PartedCanHandleDelegate<TReceiverPart> ReceiverCanHandle { get; set; }
            internal PartedDragDesponderDelegate<TReceiverPart> ReceiverDragDesponder { get; set; }
            internal PartedValueSetterDelegate<TReceiverPart> ReceiverValueSetter { get; set; }
            internal TReceiverPart EmptyPart { get; set; }

            private class DropData : IDropData
            {
                public TReceiver Receiver { get; set; }
                public TReceiverPart ReceiverPart { get; set; }
                public object Tag { get; set; }
                public bool EnableMulti { get; set; }
                public List<ITypeFilter> TypeFilters { get; } = new();
            }

            private class DropSettingManager : IDropSettingManager
            {

                private DropData dropData;
                public DropSettingManager(DropData dropData) => this.dropData = dropData;

                public IDropSettingManager FilterByType<T>()
                {
                    dropData.TypeFilters.Add(new TypeFilter<T>());
                    return this;
                }

                public IDropSettingManager EnableMulti(bool enableMulti = true)
                {
                    dropData.EnableMulti = enableMulti;
                    return this;
                }

            }

            public IDropSettingManager ReceiveObjectDrop(TReceiver receiver, TReceiverPart receiverPart, object tag)
            {
                TReceiverPart receiverPartNotNull = receiverPart ?? EmptyPart;
                if (!dropDataMasterDictionary.TryGetValue(receiver, out Dictionary<TReceiverPart, DropData> dropDataPartDictionary))
                {
                    dropDataPartDictionary = new();
                    dropDataMasterDictionary.Add(receiver, dropDataPartDictionary);
                }
                if (!dropDataPartDictionary.TryGetValue(receiverPartNotNull, out DropData dropData))
                {
                    dropData = new DropData()
                    {
                        Receiver = receiver,
                        ReceiverPart = receiverPart,
                        Tag = tag
                    };
                    dropDataPartDictionary.Add(receiverPartNotNull, dropData);
                }
                return new DropSettingManager(dropData);
            }

            public IDropSettingManager GetDropSettingManager(TReceiver receiver, TReceiverPart receiverPart)
            {
                TReceiverPart receiverPartNotNull = receiverPart ?? EmptyPart;
                if (!dropDataMasterDictionary.TryGetValue(receiver, out Dictionary<TReceiverPart, DropData> dropDataPartDictionary))
                    return null;
                if (!dropDataPartDictionary.TryGetValue(receiverPartNotNull, out DropData dropData))
                    return null;
                return new DropSettingManager(dropData);
            }

            public bool CanHandle(object sender, DragEventArgs eventArgs, ref IDropData dropData)
            {
                if (!(sender is TReceiver receiver))
                    return false;
                if (!dropDataMasterDictionary.TryGetValue(receiver, out Dictionary<TReceiverPart, DropData> dropDataPartDictionary))
                    return false;
                TReceiverPart receiverPart = ReceiverPartSelector(receiver, eventArgs);
                if (!dropDataPartDictionary.TryGetValue(receiverPart ?? EmptyPart, out DropData myDropData))
                    return false;
                if (!ReceiverCanHandle(receiver, receiverPart, eventArgs, myDropData.Tag))
                    return false;
                dropData = myDropData;
                return true;
            }

            public bool GetResponse(object sender, DragEventArgs eventArgs, IDropData dropData)
            {
                if (!(dropData is DropData myDropData))
                    return false;
                bool singlePresent = eventArgs.Data.GetDataPresent(typeof(ObjectProxy));
                bool multiPresent = eventArgs.Data.GetDataPresent(typeof(ObjectProxy[]));
                if (!(singlePresent || (myDropData.EnableMulti && multiPresent)))
                    return false;
                if (myDropData.TypeFilters.Count > 0)
                {
                    ObjectProxy firstObjectProxy = null;
                    if (singlePresent)
                    {
                        firstObjectProxy = eventArgs.Data.GetData(typeof(ObjectProxy)) as ObjectProxy;
                    }
                    else
                    {
                        ObjectProxy[] objectProxies = eventArgs.Data.GetData(typeof(ObjectProxy[])) as ObjectProxy[];
                        if (objectProxies.Length > 0)
                            firstObjectProxy = objectProxies[0];
                    }
                    object firstObject = firstObjectProxy?.Object;
                    if (!myDropData.TypeFilters.Any(tf => tf.Is(firstObject)))
                        return false;
                }
                return ReceiverDragDesponder(myDropData.Receiver, myDropData.ReceiverPart, eventArgs, myDropData.Tag);
            }

            public void SetValue(object sender, DragEventArgs eventArgs, IDropData dropData, IEnumerable<object> objects)
            {
                if (!(dropData is DropData myDropData))
                    return;
                ReceiverValueSetter(myDropData.Receiver, myDropData.ReceiverPart, objects, eventArgs, myDropData.Tag);
            }

            private static Dictionary<TReceiver, Dictionary<TReceiverPart, DropData>> dropDataMasterDictionary = new();

        }

        private static List<IPartedDropAdapter> partedDropAdapters = new();

        private static void RegisterPartedAdapter<TReceiverPart>(PartedDropAdapter<TReceiverPart> partedDropAdapter)
            where TReceiverPart : class
            => partedDropAdapters.Add(partedDropAdapter);


        private static List<TReceiver> knownObjects = new();

        private static void disposedHandler(object sender, EventArgs e)
        {
            if (sender is TReceiver receiver)
                knownObjects.Remove(receiver);
        }

        private static void dragOverHandler(object sender, DragEventArgs eventArgs)
        {
            eventArgs.Effect = DragDropEffects.None;
            IDropData dropData = null;
            IPartedDropAdapter handler = partedDropAdapters.FirstOrDefault(pda => pda.CanHandle(sender, eventArgs, ref dropData));
            if (handler == null)
                return;
            if (!handler.GetResponse(sender, eventArgs, dropData))
                return;
            eventArgs.Effect = DragDropEffects.Link;
        }

        private static void dragDropHandler(object sender, DragEventArgs eventArgs)
        {
            IDropData dropData = null;
            IPartedDropAdapter handler = partedDropAdapters.FirstOrDefault(pda => pda.CanHandle(sender, eventArgs, ref dropData));
            if (handler == null)
                return;
            IEnumerable<object> objects = null;
            if (eventArgs.Data.GetData(typeof(ObjectProxy)) is ObjectProxy singleReference)
                objects = new object[] { singleReference.Object };
            if (eventArgs.Data.GetData(typeof(ObjectProxy[])) is ObjectProxy[] multiReference)
                objects = multiReference.Select(sro => sro.Object);
            if (objects.Count() == 0)
                return;
            handler.SetValue(sender, eventArgs, dropData, objects);
        }

        private class NoPart
        {
            public static readonly NoPart EMPTY = new();
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
