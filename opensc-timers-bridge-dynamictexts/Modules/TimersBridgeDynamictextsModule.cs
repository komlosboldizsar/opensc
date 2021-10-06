using OpenSC.Model;
using OpenSC.Model.Timers.DynamicTextFunctions;
using OpenSC.Model.Variables;

namespace OpenSC.Modules
{

    [Module("timers-bridge-dynamictexts", "Timers (bridge to dynamic texts)", "TODO")]
    [DependsOnModule(typeof(TimersModelModule))]
    public class TimersBridgeDynamictextsModule : DynamictextsBridgeModuleBase<TimersModelModule>
    {

        protected override void registerDynamicTextFunctions()
        {
            DynamicTextFunctionRegister.Instance.RegisterFunction(new TimerHhMmSs());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new TimerTotalMinutes());
            DynamicTextFunctionRegister.Instance.RegisterFunction(new TimerTotalSeconds());
        }

    }

}
