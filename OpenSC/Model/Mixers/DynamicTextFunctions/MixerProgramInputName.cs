using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.DynamicTextFunctions
{

    class MixerProgramInputName : IDynamicTextFunction
    {

        public string FunctionName => nameof(MixerProgramInputName);

        public string Description => "The name of the input that is selected as program on the given mixer.";

        public int ParameterCount => 1;

        public DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public string[] ArgumentDescriptions => new string[]
        {
            "ID of the mixer."
        };

        public IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Mixer mixer = MixerDatabase.Instance.GetTById((int)arguments[0]);
            return new Substitute(mixer);
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private Mixer mixer;

            public Substitute(Mixer mixer)
            {

                if (mixer == null)
                {
                    CurrentValue = "?";
                    return;
                }
                this.mixer = mixer;

                mixer.OnProgramInputNameChanged += onProgramInputNameChangedHandler;
                CurrentValue = mixer.OnProgramInputName;

            }

            private void onProgramInputNameChangedHandler(Mixer mixer, string newName)
            {
                CurrentValue = newName;
            }

        }

    }

}
