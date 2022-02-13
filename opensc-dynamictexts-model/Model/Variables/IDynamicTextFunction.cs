using System;

namespace OpenSC.Model.Variables
{

    public interface IDynamicTextFunction
    {
        string Name { get; }
        string Description { get; }
        int ArgumentCount { get; }
        IDynamicTextFunctionArgument[] Arguments { get; }
        IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments);
    }

}
