using OpenSC.Model.Routers;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.DynamicTextFunctions
{

    [DynamicTextFunction(nameof(ObjectLabel), "The label from a labelset associated with a system object.")]
    public class ObjectLabel : DynamicTextFunctionBase<ObjectLabel.Substitute>
    {

        [DynamicTextFunctionArgument(0, "ID of the router.")]
        public class Arg0 : DynamicTextFunctionArgumentRegisterItem<string, ISystemObject>
        {
            public Arg0() : base(SystemObjectRegister.Instance)
            { }
        }

        [DynamicTextFunctionArgument(1, "ID of the labelset.")]
        public class Arg1 : DynamicTextFunctionArgumentDatabaseItem<Labelset>
        {
            public Arg1() : base(LabelsetDatabase.Instance)
            { }
        }

        public class Substitute : DynamicTextFunctionSubstituteBase
        {

            private ISystemObject systemObject;
            private Labelset labelset;

            public override void Init(object[] argumentObjects)
            {

                ISystemObject systemObject = argumentObjects[0] as ISystemObject;
                if (systemObject == null)
                {
                    CurrentValue = "?";
                    return;
                }

                Labelset labelset = argumentObjects[1] as Labelset;
                if (labelset == null)
                {
                    CurrentValue = "?";
                    return;
                }
                this.labelset = labelset;

                labelset.LabelTextChanged += labelsetLabelChanged;
                CurrentValue = labelset.GetText(systemObject);

            }

            private void labelsetLabelChanged(Label label, string newText)
            {
                if ((label.AssociatedObject == systemObject) && (label.Labelset == labelset))
                    CurrentValue = newText;
            }

        }

    }

}
