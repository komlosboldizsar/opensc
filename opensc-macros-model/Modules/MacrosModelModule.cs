using OpenSC.Model;
using OpenSC.Model.Macros;

namespace OpenSC.Modules
{

    [Module("macros-model", "Macros (model)", "TODO")]
    public class MacrosModelModule : IModule
    {

        public void Initialize()
        {
            registerDatabases();
        }

        private void registerDatabases()
        {
            MasterDatabase.Instance.RegisterSingletonDatabase(typeof(MacroDatabase));
        }

    }

}
