using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables.TypeDescriptors
{

    public class FlipFlopD : FlipFlopTypeDescriptor
    {

        public static FlipFlopTypeDescriptor Instance { get; } = new FlipFlopD();
        public override bool Input1Used => true;
        public override bool Input2Used => true;
        public override string Input1Name => "Data";
        public override string Input2Name => "Clock";
        public override bool Input1Invertable => false;
        public override bool Input2Invertable => true;

        public override string GetDescription(string input1identifier, bool input1inverted, string input2identifier, bool input2inverted)
            => $"D flip-flop with data input [{identifier(input1identifier)}] clocked by {edge(input2inverted)} [{identifier(input2identifier)}].";

        protected override bool _calculateState(bool current, bool input1, bool input1edge, bool input2, bool input2edge)
        {
            if (input2 && input2edge)
                return input1;
            return current;
        }

    }

}
