using System;

namespace OpenSC.Model.Variables
{

    public delegate void DynamicTextFunctionSubstituteValueChanged(IDynamicTextFunctionSubstitute substitute);

    public class IDynamicTextFunctionSubstitute
    {

        string CurrentValue { get; }

        event DynamicTextFunctionSubstituteValueChanged ValueChanged;

    }

}
