using OpenSC.Model.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Salvos
{

    public class SalvoCrosspoint : ObjectBase
    {

        public SalvoCrosspoint(RouterOutput output = null, RouterInput input = null)
        {
            Output = output;
            Input = input;
        }

        // "Temp foreign keys"
        public int? __routerId;
        public int? __outputIndex;
        public int? __inputIndex;

        public Router Router => output?.Router;

        public event PropertyChangedTwoValuesDelegate<SalvoCrosspoint, RouterOutput> OutputChanged;
        private RouterOutput output;
        public RouterOutput Output
        {
            get => output;
            set => this.setProperty(ref output, value, OutputChanged);
        }

        public event PropertyChangedTwoValuesDelegate<SalvoCrosspoint, RouterInput> InputChanged;
        private RouterInput input;
        public RouterInput Input
        {
            get => input;
            set => this.setProperty(ref input, value, InputChanged);
        }

        public void StoreCurrentInput() => Input = Output?.CurrentInput;

        internal void RestoreAssociations()
        {
            if (__routerId == null)
                return;
            Router router = RouterDatabase.Instance.GetTById((int)__routerId);
            if (router == null)
                return;
            if (__outputIndex != null)
                Output = router.GetOutput((int)__outputIndex);
            if (__inputIndex != null)
                Input = router.GetInput((int)__inputIndex);
        }

    }

}
