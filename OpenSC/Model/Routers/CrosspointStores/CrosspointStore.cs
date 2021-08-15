using OpenSC.Logger;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers.Triggers;
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
            IdChanged = null;
            NameChanged = null;
            StoredInputChanged = null;
            StoredOutputChanged = null;
            ClearInputAfterTakeChanged = null;
            ClearOutputAfterTakeChanged = null;
        }
        #endregion

        #region Restoration
        public override void TotallyRestored()
        {
            base.TotallyRestored();
            restoreStoredInput();
            restoreStoredOutput();
        }
        #endregion

        #region Property: ID
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, int> IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set => setProperty(this, ref id, value, IdChanged, validator: ValidateId);
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!CrosspointStoreDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, string> NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, validator: ValidateName);
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: StoredInput
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, RouterInput> StoredInputChanged;

        private string __storedInputId; // "Temp foreign key"

        [PersistAs("stored_input")]
        private string _storedInputId
        {
            get => (storedInput != null) ? string.Format("router.{0}.input.{1}", storedInput.Router.ID, storedInput.Index) : null;
            set { __storedInputId = value; }
        }

        private RouterInput storedInput;

        public RouterInput StoredInput
        {
            get => storedInput;
            set
            {
                if (!setProperty(this, ref storedInput, value, StoredInputChanged))
                    return;
                if (Autotake)
                    Take();
            }
        }

        private void restoreStoredInput()
        {
            string[] storedInputIdParts = __storedInputId?.Split('.');
            if (storedInputIdParts?.Length != 4)
                return;
            if ((storedInputIdParts[0] != "router") || (storedInputIdParts[2] != "input"))
                return;
            if (!int.TryParse(storedInputIdParts[1], out int storedInputRouterId))
                return;
            if (!int.TryParse(storedInputIdParts[3], out int storedInputIndex))
                return;
            StoredInput = RouterDatabase.Instance.GetTById(storedInputRouterId)?.GetInput(storedInputIndex);
        }
        #endregion

        #region Property: StoredOutput
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, RouterOutput> StoredOutputChanged;

        private string __storedOutputId; // "Temp foreign key"

        [PersistAs("stored_output")]
        private string _storedOutputId
        {
            get => (storedOutput != null) ? string.Format("router.{0}.output.{1}", storedOutput.Router.ID, storedInput.Index) : null;
            set { __storedOutputId = value; }
        }

        private RouterOutput storedOutput;

        public RouterOutput StoredOutput
        {
            get => storedOutput;
            set => setProperty(this, ref storedOutput, value, StoredOutputChanged);
        }

        private void restoreStoredOutput()
        {
            string[] storedOutputIdParts = __storedOutputId?.Split('.');
            if (storedOutputIdParts?.Length != 4)
                return;
            if ((storedOutputIdParts[0] != "router") || (storedOutputIdParts[2] != "output"))
                return;
            if (!int.TryParse(storedOutputIdParts[1], out int storedOutputRouterId))
                return;
            if (!int.TryParse(storedOutputIdParts[3], out int storedOutputIndex))
                return;
            StoredOutput = RouterDatabase.Instance.GetTById(storedOutputRouterId)?.GetOutput(storedOutputIndex);
        }
        #endregion

        #region Property: ClearInputAfterTake
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> ClearInputAfterTakeChanged;

        [PersistAs("clear_input_after_take")]
        private bool clearInputAfterTake;

        public bool ClearInputAfterTake
        {
            get => clearInputAfterTake;
            set => setProperty(this, ref clearInputAfterTake, value, ClearInputAfterTakeChanged);
        }
        #endregion

        #region Property: ClearOutputAfterTake
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> ClearOutputAfterTakeChanged;

        [PersistAs("clear_output_after_take")]
        private bool clearOutputAfterTake;

        public bool ClearOutputAfterTake
        {
            get => clearOutputAfterTake;
            set => setProperty(this, ref clearOutputAfterTake, value, ClearOutputAfterTakeChanged);
        }
        #endregion

        #region Property: Autotake
        public event PropertyChangedTwoValuesDelegate<CrosspointStore, bool> AutotakeChanged;

        private bool autotake;

        public bool Autotake
        {
            get => autotake;
            set => setProperty(this, ref autotake, value, AutotakeChanged);
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
