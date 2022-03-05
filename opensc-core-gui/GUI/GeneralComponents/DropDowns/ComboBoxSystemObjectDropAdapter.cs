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

        public static void FilterSystemObjectDropByType<T>(this ComboBox comboBox)
        {
            if (!dropDataDictionary.TryGetValue(comboBox, out DropData dropData))
                return;
            dropData.TypeFilters.Add(new TypeFilter<T>());
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
            if (dropData.TypeFilters.Count > 0) {
                SystemObjectReference systemObjectReference = e.Data.GetData(typeof(SystemObjectReference)) as SystemObjectReference;
                ISystemObject systemObject = systemObjectReference?.Object;
                if (!dropData.TypeFilters.Any(tf => tf.Is(systemObject)))
                    return false;
            }
            e.Effect = DragDropEffects.Link;
            return true;
        }

        private static Dictionary<ComboBox, DropData> dropDataDictionary = new();

        private class DropData
        {
            public ComboBox ComboBox { get; set; }
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
