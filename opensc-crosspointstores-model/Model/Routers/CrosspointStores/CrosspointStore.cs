using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers.CrosspointStores
{

    public class CrosspointStore : ModelBase
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
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, RouterInput> StoredInputChanged;

        [PersistAs("stored_input")]
        private RouterInput storedInput;

        public RouterInput StoredInput
        {
            get => storedInput;
            set
            {
                if (!this.setProperty(ref storedInput, value, StoredInputChanged))
                    return;
                if (Autotake)
                    Take();
            }
        }
        #endregion

        #region Property: StoredOutput
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, RouterOutput> StoredOutputChanged;

        [PersistAs("stored_output")]
        private RouterOutput storedOutput;

        public RouterOutput StoredOutput
        {
            get => storedOutput;
            set
            {
                AfterChangePropertyDelegate<RouterOutput> afterChangeDelegate = (ov, nv) =>
                {
                    if (importInputAfterOutputSet)
                        StoredInput = nv?.CurrentInput;
                };
                this.setProperty(ref storedOutput, value, StoredOutputChanged, null, afterChangeDelegate);
            }
        }
        #endregion

        #region Property: ClearInputAfterTake
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> ClearInputAfterTakeChanged;

        [PersistAs("clear_input_after_take")]
        private bool clearInputAfterTake;

        public bool ClearInputAfterTake
        {
            get => clearInputAfterTake;
            set => this.setProperty(ref clearInputAfterTake, value, ClearInputAfterTakeChanged);
        }
        #endregion

        #region Property: ClearOutputAfterTake
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> ClearOutputAfterTakeChanged;

        [PersistAs("clear_output_after_take")]
        private bool clearOutputAfterTake;

        public bool ClearOutputAfterTake
        {
            get => clearOutputAfterTake;
            set => this.setProperty(ref clearOutputAfterTake, value, ClearOutputAfterTakeChanged);
        }
        #endregion

        #region Property: ImportInputAfterOutputSet
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> ImportInputAfterOutputSetChanged;

        [PersistAs("import_input_after_output_set")]
        private bool importInputAfterOutputSet;

        public bool ImportInputAfterOutputSet
        {
            get => importInputAfterOutputSet;
            set => this.setProperty(ref importInputAfterOutputSet, value, ImportInputAfterOutputSetChanged);
        }
        #endregion

        #region Property: Autotake
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> AutotakeChanged;

        private bool autotake;

        public bool Autotake
        {
            get => autotake;
            set => this.setProperty(ref autotake, value, AutotakeChanged);
        }
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
