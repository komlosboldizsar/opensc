using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.Salvos
{

    public class SalvoCrosspoint
    {

        // "Temp foreign keys"
        public int __routerId;
        public int __outputIndex;
        public int __inputIndex;

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

        internal void RestoreAssociations()
        {
            Router router = RouterDatabase.Instance.GetTById(__routerId);
            if (router == null)
                return;
            Output = router.GetOutput(__outputIndex);
            Input = router.GetInput(__inputIndex);
        }

        public void Take()
        {
            if ((output != null) && (input != null))
                output.RequestCrosspointUpdate(input);
        }

    }

}
