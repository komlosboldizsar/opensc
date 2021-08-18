using System;

namespace OpenSC.Model.Variables
{

    public delegate void DynamicTextFunctionSubstituteValueChanged(IDynamicTextFunctionSubstitute substitute);

    public interface IDynamicTextFunctionSubstitute
    {
        string CurrentValue { get; }

        event DynamicTextFunctionSubstituteValueChanged ValueChanged;
    }

}
