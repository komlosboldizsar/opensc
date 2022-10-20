using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public abstract class FlipFlopTypeDescriptor
    {

        public abstract bool Input1Used { get; }
        public abstract bool Input2Used { get; }
        public abstract string Input1Name { get; }
        public abstract string Input2Name { get; }
        public abstract bool Input1Invertable { get; }
        public abstract bool Input2Invertable { get; }

        public abstract string GetDescription(string input1identifier, bool input1inverted, string input2identifier, bool input2inverted);
        protected const string INPUT_IDENTIFIER_NULL = "none";
        protected string identifier(string identifier) => identifier ?? INPUT_IDENTIFIER_NULL;
        protected const string WORD_POSEDGE = "posedge";
        protected const string WORD_NEGEDGE = "negedge";
        protected string edge(bool inverted) => inverted ? WORD_NEGEDGE : WORD_POSEDGE;

        public bool CalculateState(bool current, bool input1, bool input1inverted, bool input1edge, bool input2, bool input2inverted, bool input2edge)
        {
            if (input1inverted && Input1Invertable)
                input1 = !input1;
            if (input2inverted && Input2Invertable)
                input2 = !input2;
            return _calculateState(current, input1, input1edge, input2, input2edge);
        }

        protected abstract bool _calculateState(bool current, bool input1, bool input1edge, bool input2, bool input2edge);

    }

}
