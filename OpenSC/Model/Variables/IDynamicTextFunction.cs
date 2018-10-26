using System;

namespace OpenSC.Model.Variables
{

    public interface IDynamicTextFunction
    {

        string FunctionName { get; }

        int ParameterCount { get; }

        DynamicTextFunctionArgumentType[] ArgumentTypes { get; }
        
        IDynamicTextFunctionSubstitute GetSubstitute(object[] arguments);

    }

}
