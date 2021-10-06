using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.Tables
{

    public abstract class CustomDataGridViewComboBoxItem<T>
        where T : class
    {

        public T Value { get; private set; }

        public string Label { get => ToString(); }

        public CustomDataGridViewComboBoxItem(T value)
        {
            Value = value;
        }

        public class NullItem : CustomDataGridViewComboBoxItem<T>
        {

            private string label;

            public NullItem(string label) : base(null)
            {
                this.label = label;
            }

            public override string ToString()
            {
                return label;
            }

        }

    }

}
