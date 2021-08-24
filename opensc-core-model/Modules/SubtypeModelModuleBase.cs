using OpenSC.Model;

namespace OpenSC.Modules
{

    //[DependsOnModule(typeof(TBasetypeModule))] - CS0416
    public abstract class SubtypeModelModuleBase<TBasetypeModule> : ModelModuleBase
    {

        public override void Initialize()
        {
            base.Initialize();
            registerModelTypes();
        }

        protected abstract void registerModelTypes();

    }

}
