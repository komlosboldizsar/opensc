using OpenSC.GUI.GeneralComponents.DragDrop;
using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DragDrop
{

    public static class GeneralObjectDropAdapter
    {

        private class Handlers<TControl>
            where TControl : Control
        {

            public static Handlers<TControl> Instance { get; } = new();
            private Handlers() => ObjectDropAdapter<TControl>.Handle(receiverCanHandle, receiverDragResponder, receiverValueSetter);

            private bool receiverCanHandle(TControl receiver, DragEventArgs eventArgs, object tag)
                => getControlData(receiver)?.CanHandle(receiver, eventArgs, tag) ?? false;

            private bool receiverDragResponder(TControl receiver, DragEventArgs eventArgs, object tag)
                => getControlData(receiver)?.DragDesponder(receiver, eventArgs, tag) ?? false;

            private void receiverValueSetter(TControl receiver, IEnumerable<object> objects, DragEventArgs eventArgs, object tag)
                => getControlData(receiver)?.ValueSetter(receiver, objects, eventArgs, tag);

            public void RegisterControl(
                TControl control,
                ObjectDropAdapter<TControl>.CanHandleDelegate canHandle,
                ObjectDropAdapter<TControl>.DragDesponderDelegate dragResponder,
                ObjectDropAdapter<TControl>.ValueSetterDelegate valueSetter)
                => controlDataDictionary[control] = new(canHandle, dragResponder, valueSetter);

            private ControlData getControlData(TControl control)
            {
                if (controlDataDictionary.TryGetValue(control, out ControlData controlData))
                    return controlData;
                return null;
            }

            private Dictionary<TControl, ControlData> controlDataDictionary = new();

            private record ControlData(ObjectDropAdapter<TControl>.CanHandleDelegate CanHandle,
                ObjectDropAdapter<TControl>.DragDesponderDelegate DragDesponder,
                ObjectDropAdapter<TControl>.ValueSetterDelegate ValueSetter);

            public static bool DefaultCanHandleDelegate(TControl receiver, DragEventArgs eventArgs, object tag) => true;
            public static bool DefaultDragDesponderDelegate(TControl receiver, DragEventArgs eventArgs, object tag) => true;

        }

        public static ObjectDropAdapter<TControl>.IDropSettingManager ReceiveObjectDropCustom<TControl>(this TControl control,
            ObjectDropAdapter<TControl>.ValueSetterDelegate valueSetter,
            object tag = null,
            ObjectDropAdapter<TControl>.CanHandleDelegate canHandle = null,
            ObjectDropAdapter<TControl>.DragDesponderDelegate dragResponder = null)
            where TControl : Control
        {
            canHandle ??= Handlers<TControl>.DefaultCanHandleDelegate;
            dragResponder ??= Handlers<TControl>.DefaultDragDesponderDelegate;
            Handlers<TControl>.Instance.RegisterControl(control, canHandle, dragResponder, valueSetter);
            return ObjectDropAdapter<TControl>.ReceiveObjectDrop(control, tag);
        }  

    }

}
