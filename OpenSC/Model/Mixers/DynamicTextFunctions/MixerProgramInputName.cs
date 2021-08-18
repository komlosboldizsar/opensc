﻿using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Mixers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(MixerProgramInputName), "The name of the input that is selected as program on the given mixer.")]
    class MixerProgramInputName : DynamicTextFunctionBase<MixerProgramInputName.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the mixer.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Router>
        {
            public Arg0() : base(RouterDatabase.Instance)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            public override void Init(object[] argumentObjects)
            {
                Mixer mixer = argumentObjects[0] as Mixer;
                if (mixer == null)
                {
                    CurrentValue = "?";
                    return;
                }
                mixer.OnProgramInputNameChanged += onProgramInputNameChangedHandler;
                CurrentValue = mixer.OnProgramInputName;
            }

            private void onProgramInputNameChangedHandler(Mixer mixer, string newName) => CurrentValue = newName;

        }

    }

}
