using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public interface ICustomDataGridViewComboBoxItem
    {
        object ObjValue { get; }
        string Label { get; }
    }

    public class CustomDataGridViewComboBoxItem<T> : ICustomDataGridViewComboBoxItem
        where T : class
    {

        public T Value { get; private set; }
        public object ObjValue => Value;
        public virtual string Label => Value?.ToString() ?? "";

        public CustomDataGridViewComboBoxItem(T value) => Value = value;

        public class NullItem : CustomDataGridViewComboBoxItem<T>
        {
            private string label;
            public NullItem(string label) : base(null) => this.label = label;
            public override string Label => label;
        }

    }

}
