using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public delegate void RouterInputSourceSourceNameChanged(IRouterInputSource inputSource, string newName);
    public delegate void RouterInputSourceTallyChanged(IRouterInputSource inputSource, bool newState);

    public interface IRouterInputSource
    {

        string SourceName { get; }
        string GetSourceName(List<object> recursionChain);
        event RouterInputSourceSourceNameChanged SourceNameChanged;

        bool RedTally { get; }
        bool GreenTally { get; }

        bool GetRedTally(List<object> recursionChain);
        bool GetGreenTally(List<object> recursionChain);

        event RouterInputSourceTallyChanged RedTallyChanged;
        event RouterInputSourceTallyChanged GreenTallyChanged;

    }

}
