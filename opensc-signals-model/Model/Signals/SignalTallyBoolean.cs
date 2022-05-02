using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Signals
{

    public abstract class SignalTallyBoolean : BooleanBase
    {

        protected ISignalTallyState tally;
        protected SignalTallyColor color;
        protected bool registered = false;

        public SignalTallyBoolean(ISignalTallyState tally, SignalTallyColor color) : base()
        {
            this.tally = tally;
            this.color = color;
            Color = getColor(color);
            tally.StateChanged += signalTallyChangedHandler;
        }

        private void signalTallyChangedHandler(ISignalSource signalSource, ISignalTallyState tally, bool newState, List<object> recursionChain)
            => CurrentState = newState;

        protected abstract string getName();
        protected abstract string getDescription();

        protected void updateName() => Identifier = getName();
        protected void updateDescription() => Description = getDescription();
        protected void updateFields()
        {
            updateName();
            updateDescription();
        }

        protected static Color getColor(SignalTallyColor color)
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

        protected static string getColorString(SignalTallyColor color)
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
