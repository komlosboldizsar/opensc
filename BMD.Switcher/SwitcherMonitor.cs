using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMD.Switcher
{

    public class SwitcherMonitor : IBMDSwitcherCallback, IDisposable
    {

        public IBMDSwitcher Switcher { get; private set; }

        public SwitcherMonitor(IBMDSwitcher switcher)
        {
            Switcher = switcher;
            switcher.AddCallback(this);
        }

        public void Dispose()
        {
            Switcher.RemoveCallback(this);
            SwitcherDisconnected = null;
        }

        public void Notify(_BMDSwitcherEventType eventType, _BMDSwitcherVideoMode coreVideoMode)
        {
            switch (eventType)
            {
                case _BMDSwitcherEventType.bmdSwitcherEventTypeDisconnected:
                    SwitcherDisconnected?.Invoke(Switcher, this);
                    break;
            }
        }

        #region Disconnected event
        public delegate void SwitcherDisconnectedDelegate(IBMDSwitcher switcher, SwitcherMonitor monitor);
        public event SwitcherDisconnectedDelegate SwitcherDisconnected;
        #endregion

        }

}
