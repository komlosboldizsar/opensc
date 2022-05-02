using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.TypeDescriptors
{

    public class FlipFlopT : FlipFlopTypeDescriptor
    {

        public static FlipFlopTypeDescriptor Instance { get; } = new FlipFlopT();
        public override bool Input1Used => true;
        public override bool Input2Used => false;
        public override string Input1Name => "Toggle";
        public override string Input2Name => string.Empty;
        public override bool Input1Invertable => true;
        public override bool Input2Invertable => false;

        public override string GetDescription(string input1identifier, bool input1inverted, string input2identifier, bool input2inverted)
            => $"T flip-flop clocked by {edge(input1inverted)} [{identifier(input1identifier)}].";

        protected override bool _calculateState(bool current, bool input1, bool input1edge, bool input2, bool input2edge)
        {
            if (input1 && input1edge)
                return !current;
            return current;
        }

    }

}
