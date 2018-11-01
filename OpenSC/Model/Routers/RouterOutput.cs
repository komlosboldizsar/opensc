using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{

    public delegate void RouterOutputNameChanged(RouterOutput output, string oldName, string newName);
    public delegate void RouterOutputNameChangedPCN();

    public delegate void RouterCrosspointChangedDelegate(RouterOutput output, RouterInput newInput);

    public class RouterOutput : IRouterInputSource
    {

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException();
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                NameChangedPCN?.Invoke();
            }
        }

        public event RouterOutputNameChanged NameChanged;
        public event RouterOutputNameChangedPCN NameChangedPCN;

        public Router Router { get; internal set; }

        private int index;

        public int Index
        {
            get { return index; }
            internal set { index = value; }
        }

        private RouterInput crosspoint;

        public RouterInput Crosspoint
        {
            get { return crosspoint; }
            internal set
            {
                unsubscribeCrosspointEvents();
                crosspoint = value;
                fireChangeEventsAtCrosspointChange();
                RouterCrosspointChanged(this, value);
                subscribeCrosspointEvents();
            }
        }

        public event RouterCrosspointChangedDelegate RouterCrosspointChanged;

        private void subscribeCrosspointEvents()
        {
            if (crosspoint == null)
                return;
            crosspoint.SourceNameChanged += crosspointSourceNameChangedHandler;
            crosspoint.RedTallyChanged += crosspointRedTallyChangedHandler;
            crosspoint.GreenTallyChanged += crosspointGreenTallyChangedHandler;
        }

        private void unsubscribeCrosspointEvents()
        {
            if (crosspoint == null)
                return;
            crosspoint.SourceNameChanged -= crosspointSourceNameChangedHandler;
            crosspoint.RedTallyChanged -= crosspointRedTallyChangedHandler;
            crosspoint.GreenTallyChanged -= crosspointGreenTallyChangedHandler;
        }

        private void fireChangeEventsAtCrosspointChange()
        {
            if(crosspoint == null)
            {
                SourceNameChanged?.Invoke(this, null);
                RedTallyChanged?.Invoke(this, false);
                GreenTallyChanged?.Invoke(this, false);
            }
            else
            {
                SourceNameChanged?.Invoke(this, crosspoint.SourceName);
                RedTallyChanged?.Invoke(this, crosspoint.RedTally);
                GreenTallyChanged?.Invoke(this, crosspoint.GreenTally);
            }
        }

        private void crosspointSourceNameChangedHandler(RouterInput input, string newName)
        {
            SourceNameChanged?.Invoke(this, newName);
        }

        private void crosspointRedTallyChangedHandler(RouterInput input, bool newState)
        {
            RedTallyChanged?.Invoke(this, newState);
        }

        private void crosspointGreenTallyChangedHandler(RouterInput input, bool newState)
        {
            GreenTallyChanged?.Invoke(this, newState);
        }

        public string InputName
        {
            get => crosspoint?.Name;
        }

        public string SourceName
        {
            get => crosspoint?.SourceName;
        }

        public event RouterInputSourceSourceNameChanged SourceNameChanged;

        public bool RedTally =>
            (crosspoint != null) ? crosspoint.RedTally : false;

        public bool GreenTally =>
            (crosspoint != null) ? crosspoint.GreenTally : false;

        public event RouterInputSourceTallyChanged RedTallyChanged;
        public event RouterInputSourceTallyChanged GreenTallyChanged;

    }

}
