using OpenSC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public static class ComboBoxSystemObjectDropAdapter
    {

        public static void ReceiveSystemObjectDrop(this ComboBox comboBox)
        {
            comboBox.AllowDrop = true;
            comboBox.Disposed += disposedHandler;
            comboBox.DragEnter += dragEnterHandler;
            comboBox.DragDrop += dragDropHandler;
            if (!dropDataDictionary.TryGetValue(comboBox, out DropData dropData))
            {
                dropData = new DropData() { ComboBox = comboBox };
                dropDataDictionary.Add(comboBox, dropData);
            }
        }

        private static void disposedHandler(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
                dropDataDictionary.Remove(comboBox);
        }

        private static void dragEnterHandler(object sender, DragEventArgs e)
        {
            if (!dragEventHandler(sender, e, out DropData dropData))
                return;
        }

        private static void dragDropHandler(object sender, DragEventArgs e)
        {
            if (!dragEventHandler(sender, e, out DropData dropData))
                return;
            if (!(e.Data.GetData(typeof(SystemObjectReference)) is SystemObjectReference systemObjectReference))
                return;
            ISystemObject systemObject = systemObjectReference.Object;
            ComboBox comboBox = ((ComboBox)sender);
            if (comboBox.ContainsValue(systemObject))
                comboBox.SelectByValue(systemObject);
        }

        private static bool dragEventHandler(object sender, DragEventArgs e, out DropData dropData)
        {
            dropData = null;
            e.Effect = DragDropEffects.None;
            if (!(sender is ComboBox comboBox))
                return false;
            if (!e.Data.GetDataPresent(typeof(SystemObjectReference)))
                return false;
            if (!dropDataDictionary.TryGetValue(comboBox, out dropData))
                return false;
            e.Effect = DragDropEffects.Link;
            return true;
        }

        private static Dictionary<ComboBox, DropData> dropDataDictionary = new();

        private class DropData
        {
            public ComboBox ComboBox { get; set; }
        }

    }

}
