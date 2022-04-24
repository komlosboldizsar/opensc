using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Salvos
{

    public class SalvoCrosspoint
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

        private RouterOutput output;
        public RouterOutput Output
        {
            get => output;
            set => output = value;
        }

        private RouterInput input;
        public RouterInput Input
        {
            get => input;
            set => input = value;
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
