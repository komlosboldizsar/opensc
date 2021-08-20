using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.VTRs.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(VtrTitle), "The title of a the clip played or recorder by a VTR.")]
    public class VtrTitle : DynamicTextFunctionBase<VtrTitle.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the VTR.")]
        public class Arg0 : DynamicTextFunctionArgumentDatabaseItem<Vtr>
        {
            public Arg0() : base(VtrDatabase.Instance)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            public override void Init(object[] argumentObjects)
            {
                Vtr vtr = argumentObjects[0] as Vtr;
                if (vtr == null)
                {
                    CurrentValue = "?";
                    return;
                }
                vtr.TitleChanged += vtrTitleChangedHandler;
                CurrentValue = vtr?.Title ?? "";
            }

            private void vtrTitleChangedHandler(Vtr vtr, string oldTitle, string newTitle)
                => CurrentValue = newTitle ?? "";

        }

    }

}
