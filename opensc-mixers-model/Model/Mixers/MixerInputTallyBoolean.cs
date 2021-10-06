using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers
{

    internal class MixerInputTallyBoolean : BooleanBase
    {

        private MixerInput input;

        private TallyColor color;

        public MixerInputTallyBoolean(MixerInput input, TallyColor color) :
            base(getName(input, color), getColor(color), getDescription(input, color))
        {
            this.input = input;
            this.color = color;
            //input.IndexChanged += indexChangedHandler;
            input.NameChanged += nameChangedHandler;
            input.Mixer.IdChanged += routerIdChangedHandler;
            input.Mixer.NameChanged += routerNameChangedHandler;
            switch (color)
            {
                case TallyColor.Red:
                    CurrentState = input.RedTally;
                    input.RedTallyChanged += tallyChangedHandler;
                    break;
                case TallyColor.Green:
                    CurrentState = input.GreenTally;
                    input.GreenTallyChanged += tallyChangedHandler;
                    break;
            }
        }

        public void Update()
        {
            Name = getName(input, color);
            Description = getDescription(input, color);
        }

        private void tallyChangedHandler(MixerInput output, bool newState)
        {
            CurrentState = newState;
        }

        private void indexChangedHandler(MixerInput output, int oldIndex, int newIndex)
        {
            Name = getName(output, color);
            Description = getDescription(output, color);
        }

        private void nameChangedHandler(MixerInput output, string oldName, string newName)
        {
            Description = getDescription(output, color);
        }
        private void routerIdChangedHandler(IModel mixer, int oldValue, int newValue)
        {
            Name = getName(input, color);
            Description = getDescription(input, color);
        }

        private void routerNameChangedHandler(IModel mixer, string oldName, string newName)
        {
            Description = getDescription(input, color);
        }

        private static string getName(MixerInput input, TallyColor color)
            => string.Format("mixer.{0}.input.{1}.{2}tally", input.Mixer.ID, input.Index, getColorString(color));

        private static Color getColor(TallyColor color)
        {
            switch (color)
            {
                case TallyColor.Red:
                    return Color.Red;
                case TallyColor.Green:
                    return Color.Green;
            }
            return Color.White;
        }

        private static string getDescription(MixerInput input, TallyColor color)
            => string.Format("The [(#{2}) {3}] input of mixer [(#{0}) {1}] has {4} tally.",
                input.Mixer.ID, input.Mixer.Name,
                input.Index, input.Name,
                getColorString(color));

        private static string getColorString(TallyColor color)
        {
            switch (color)
            {
                case TallyColor.Red:
                    return "red";
                case TallyColor.Green:
                    return "green";
            }
            return "unknown";
        }

        public enum TallyColor
        {
            Red,
            Green
        }

    }

}
