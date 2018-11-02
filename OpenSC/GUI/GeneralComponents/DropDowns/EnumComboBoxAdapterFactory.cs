using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    class EnumComboBoxAdapterFactory<T> : IComboBoxAdapterFactory
    {

        Dictionary<T, string> translations;

        public EnumComboBoxAdapterFactory(Dictionary<T, string> translations = null)
        {
            this.translations = translations;
        }

        public EnumComboBoxAdapter<T> GetOneTyped()
        {
            return new EnumComboBoxAdapter<T>(translations);
        }

        public IComboBoxAdapter GetOne()
            => GetOneTyped();

    }

}
