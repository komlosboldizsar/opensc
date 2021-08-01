using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{

    class VtrTitle : IDynamicTextFunction
    {
        public virtual string FunctionName => nameof(VtrTitle);

        public virtual string Description => "The title of a the clip played or recorder by a VTR.";

        public virtual int ParameterCount => 1;

        public virtual DynamicTextFunctionArgumentType[] ArgumentTypes => new DynamicTextFunctionArgumentType[]
        {
            DynamicTextFunctionArgumentType.Integer
        };

        public virtual string[] ArgumentDescriptions => new string[]
        {
            "ID of the VTR."
        };

        public virtual IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments)
        {
            Vtr vtr = VtrDatabase.Instance.GetTById((int)arguments[0]);
            Substitute substitute = new Substitute(vtr);
            substitute.UpdateAfterConstruct();
            return substitute;
        }

        protected class Substitute : DynamicTextFunctionSubstituteBase
        {

            private readonly Vtr vtr;

            public Substitute(Vtr vtr)
            {
                if (vtr == null) {
                    CurrentValue = "?";
                    return;
                }
                this.vtr = vtr;
                vtr.TitleChanged += vtrTitleChangedHandler;
            }

            public void UpdateAfterConstruct()
            {
                CurrentValue = vtr?.Title ?? "";
            }
            private void vtrTitleChangedHandler(Vtr vtr, string oldTitle, string newTitle)
            {
                CurrentValue = newTitle ?? ""; ;
            }

        }

    }

}
