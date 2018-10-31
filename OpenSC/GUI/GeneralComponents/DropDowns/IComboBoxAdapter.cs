using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.GeneralComponents.DropDowns
{

    public interface IComboBoxAdapter : IListSource
    {
        bool ContainsNull { get; }
    }

}
