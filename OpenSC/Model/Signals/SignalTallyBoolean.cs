using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public class SignalTallyBoolean : BooleanBase
    {

        private ExternalSignal signal;
        private ExternalSignalTally tally;
        private SignalTallyColor color;

        public SignalTallyBoolean(ExternalSignal signal, ExternalSignalTally tally, SignalTallyColor color) :
            base(getName(signal, color), getColor(color), getDescription(signal, color))
        {
            this.signal = signal;
            this.tally = tally;
            this.color = color;
            signal.IdChanged += idChangedHandler;
            signal.NameChanged += nameChangedHandler;
            tally.StateChanged += tallyChangedHandler;
            BooleanRegister.Instance.RegisterBoolean(this);
        }

        private void idChangedHandler(ExternalSignal signal, int oldValue, int newValue)
        {
            Name = getName(signal, color);
            Description = getDescription(signal, color);
        }

        private void nameChangedHandler(ExternalSignal signal, string oldName, string newName)
        {
            Description = getDescription(signal, color);
        }

        private void tallyChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState)
        {
            CurrentState = newState;
        }

        private static string getName(ExternalSignal signal, SignalTallyColor color)
            => string.Format("signal.{0}.{1}tally", signal.ID, getColorString(color));
        private static string getDescription(ExternalSignal signal, SignalTallyColor color)
            => string.Format("Signal [(#{0}) {1}] has {2} tally.", signal.ID, signal.Name, getColorString(color));

        private static Color getColor(SignalTallyColor color)
        {
            switch (color)
            {
                case SignalTallyColor.Red:
                    return Color.Red;
                case SignalTallyColor.Yellow:
                    return Color.Yellow;
                case SignalTallyColor.Green:
                    return Color.Green;
            }
            return Color.White;
        }

        private static string getColorString(SignalTallyColor color)
        {
            switch (color)
            {
                case SignalTallyColor.Red:
                    return "red";
                case SignalTallyColor.Yellow:
                    return "yellow";
                case SignalTallyColor.Green:
                    return "green";
            }
            return "unknown";
        }

    }

}
