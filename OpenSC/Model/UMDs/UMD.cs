using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.UMDs
{

    public delegate void UmdTextChanging(UMD umd, string oldText, string newText);
    public delegate void UmdTextChanged(UMD umd, string oldText, string newText);

    public delegate void UmdTallyChanging(UMD umd, int index, bool oldState, bool newState);
    public delegate void UmdTallyChanged(UMD umd, int index, bool oldState, bool newState);

    public interface UMD
    {

        string Text { get; set; }
        void SetTally(int index, bool state);

        event UmdTextChanging TextChanging;
        event UmdTextChanged TextChanged;

        event UmdTallyChanging TallyChanging;
        event UmdTallyChanged TallyChanged;

    }
}
