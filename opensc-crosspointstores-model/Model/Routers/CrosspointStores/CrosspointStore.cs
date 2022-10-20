using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores
{

    public partial class CrosspointStore : ModelBase
    {

        public const string LOG_TAG = "CrosspointStore";

        #region Persistence, instantiation
        public CrosspointStore()
        { }

        public override void Removed()
        {
            base.Removed();
            StoredInputChanged = null;
            StoredOutputChanged = null;
            ClearInputAfterTakeChanged = null;
            ClearOutputAfterTakeChanged = null;
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = CrosspointStoreDatabase.Instance;
        #endregion

        #region Property: StoredInput
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_storedInput_afterChange))]
        [PersistAs("stored_input")]
        private RouterInput storedInput;

        private void _storedInput_afterChange(RouterInput oldValue, RouterInput newValue)
        {
            if (Autotake)
                Take();
        }
        #endregion

        #region Property: StoredOutput
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_storedOutput_afterChange))]
        [PersistAs("stored_output")]
        private RouterOutput storedOutput;

        private void _storedOutput_afterChange(RouterOutput oldValue, RouterOutput newValue)
        {
            if (importInputAfterOutputSet)
                StoredInput = newValue?.CurrentInput;
        }
        #endregion

        #region Property: ClearInputAfterTake
        [AutoProperty]
        [PersistAs("clear_input_after_take")]
        private bool clearInputAfterTake;
        #endregion

        #region Property: ClearOutputAfterTake
        [AutoProperty]
        [PersistAs("clear_output_after_take")]
        private bool clearOutputAfterTake;
        #endregion

        #region Property: ImportInputAfterOutputSet
        [AutoProperty]
        [PersistAs("import_input_after_output_set")]
        private bool importInputAfterOutputSet;
        #endregion

        #region Property: Autotake
        [AutoProperty]
        [PersistAs("autotake")]
        private bool autotake;
        #endregion

        public void Take()
        {
            if (storedInput == null)
                return;
            if (storedOutput == null)
                return;
            storedOutput.RequestCrosspointUpdate(storedInput);
            if (ClearInputAfterTake)
                StoredInput = null;
            if (ClearOutputAfterTake)
                StoredOutput = null;
        }
        

    }

}
