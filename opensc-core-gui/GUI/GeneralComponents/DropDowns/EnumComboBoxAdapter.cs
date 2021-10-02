using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public class EnumComboBoxAdapter<T> : IComboBoxAdapter
        where T : Enum
    {

        private Type givenType;
        private Type enumType;

        private Dictionary<T, string> translations;
        private string nullTranslation;
        private const string DEFAULT_NULL_TRANSLATION = "(null)";

        public bool ContainsNull => (Nullable.GetUnderlyingType(givenType) != null);

        private List<IItemProxy> proxyList = new List<IItemProxy>();

        public bool ContainsListCollection => false;

        public EnumComboBoxAdapter(Dictionary<T, string> translations = null, string nullTranslation = "")
            => construct(typeof(T), translations, nullTranslation);

        public EnumComboBoxAdapter(Type enumType, Dictionary<T, string> translations = null, string nullTranslation = "")
            => construct(enumType, translations, nullTranslation);

        private void construct(Type givenType, Dictionary<T, string> translations = null, string nullTranslation = "")
        {
            this.givenType = givenType;
            Type _enumType = Nullable.GetUnderlyingType(this.givenType);
            this.enumType = (_enumType != null) ? _enumType : givenType;
            if (!enumType.IsEnum && !Nullable.GetUnderlyingType(enumType).IsEnum)
                throw new ArgumentException();
            this.translations = translations ?? new Dictionary<T, string>();
            this.nullTranslation = nullTranslation;
            createProxyList();
        }

        public IList GetList() =>  proxyList;

        private void createProxyList()
        {
            if (ContainsNull)
                proxyList.Add(new NullItemProxy(nullTranslation ?? DEFAULT_NULL_TRANSLATION));
            foreach (T item in Enum.GetValues(enumType))
            {
                if (!translations.TryGetValue(item, out string translation))
                    translation = item.ToString();
                proxyList.Add(new ItemProxy(item, translation));
            }
        }

        public object Clone() => new EnumComboBoxAdapter<T>(translations);

        private interface IItemProxy
        {
            object Value { get; }
            string Label { get; }
        }

        private class ItemProxy : IItemProxy
        {
            public object Value => TValue;
            public T TValue { get; private set; }
            public string Label { get; private set; }
            public override string ToString() => Label;
            public ItemProxy(T value, string label)
            {
                TValue = value;
                Label = label;
            }
        }

        private class NullItemProxy : IItemProxy
        {
            public string Label { get; private set; }
            public object Value => null;
            public override string ToString() => Label;
            public NullItemProxy(string label) => Label = label;
        }

    }

}
