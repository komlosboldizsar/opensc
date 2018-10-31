using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    class EnumComboBoxAdapter<T> : IComboBoxAdapter
    {

        private Type enumType;

        private Dictionary<T, string> translations;

        private List<ItemProxy> proxyList = new List<ItemProxy>();

        public bool ContainsNull { get; } = false;

        public bool ContainsListCollection => false;

        public EnumComboBoxAdapter(Dictionary<T, string> translations = null)
        {

            this.enumType = typeof(T);
            if (!enumType.IsEnum)
                throw new ArgumentException();

            if (translations == null)
                this.translations = new Dictionary<T, string>();
            else
                this.translations = translations;

            createProxyList();

        }

        public IList GetList()
        {
            return proxyList;
        }

        private void createProxyList()
        {
            foreach (T item in Enum.GetValues(enumType))
            {
                if (!translations.TryGetValue(item, out string translation))
                    translation = item.ToString();
                proxyList.Add(new ItemProxy(item, translation));
            }
        }

        private class ItemProxy
        {

            public T Value { get; private set; }

            public string Label { get; private set; }

            public ItemProxy(T value, string label)
            {
                Value = value;
                Label = label;
            }

            public override string ToString()
                => Label;

        }

    }

}
