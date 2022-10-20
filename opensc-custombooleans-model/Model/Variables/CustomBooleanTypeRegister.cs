using OpenSC.Model.Persistence;

namespace OpenSC.Model.Variables
{
    public class CustomBooleanTypeRegister : ModelTypeRegisterBase<CustomBoolean>
    {
        public static ModelTypeRegisterBase<CustomBoolean> Instance { get; } = new CustomBooleanTypeRegister();
        private CustomBooleanTypeRegister() { }
    }
}
