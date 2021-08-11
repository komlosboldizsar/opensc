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
        #endregion

        #region Property: ID
        public delegate void IdChangedDelegate(CrosspointStore crosspointStore, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
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
        public delegate void NameChangedDelegate(CrosspointStore crosspointStore, string oldName, string newName);
        public event NameChangedDelegate NameChanged;

        [PersistAs("name")]
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                ValidateName(value);
                if (value == name)
                    return;
                string oldName = name;
                name = value;
                NameChanged?.Invoke(this, oldName, value);
                RaisePropertyChanged(nameof(Name));
            }
        }

        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Property: StoredInput
        public delegate void StoredInputChangedDelegate(CrosspointStore crosspointStore, RouterInput oldValue, RouterInput newValue);
        public event StoredInputChangedDelegate StoredInputChanged;

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
            get { return storedInput; }
            set
            {
                if (value == storedInput)
                    return;
                RouterInput oldValue = storedInput;
                storedInput = value;
                StoredInputChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(StoredInput));
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
        public delegate void StoredOutputChangedDelegate(CrosspointStore crosspointStore, RouterOutput oldValue, RouterOutput newValue);
        public event StoredOutputChangedDelegate StoredOutputChanged;

        private string __storedOutputId; // "Temp foreign key"

        private string _storedOutputId
        {
            get => (storedOutput != null) ? string.Format("router.{0}.output.{1}", storedOutput.Router.ID, storedInput.Index) : null;
            set { __storedOutputId = value; }
        }

        private RouterOutput storedOutput;

        public RouterOutput StoredOutput
        {
            get { return storedOutput; }
            set
            {
                if (value == storedOutput)
                    return;
                RouterOutput oldValue = storedOutput;
                storedOutput = value;
                StoredOutputChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(StoredOutput));
            }
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
        public delegate void ClearInputAfterTakeChangedDelegate(CrosspointStore crosspointStore, bool oldValue, bool newValue);
        public event ClearInputAfterTakeChangedDelegate ClearInputAfterTakeChanged;

        [PersistAs("clear_input_after_take")]
        private bool clearInputAfterTake;

        public bool ClearInputAfterTake
        {
            get { return clearInputAfterTake; }
            set
            {
                if (value == clearInputAfterTake)
                    return;
                bool oldValue = clearInputAfterTake;
                clearInputAfterTake = value;
                ClearInputAfterTakeChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ClearInputAfterTake));
            }
        }
        #endregion

        #region Property: ClearOutputAfterTake
        public delegate void ClearOutputAfterTakeChangedDelegate(CrosspointStore crosspointStore, bool oldValue, bool newValue);
        public event ClearOutputAfterTakeChangedDelegate ClearOutputAfterTakeChanged;

        [PersistAs("clear_output_after_take")]
        private bool clearOutputAfterTake;

        public bool ClearOutputAfterTake
        {
            get { return clearOutputAfterTake; }
            set
            {
                if (value == clearOutputAfterTake)
                    return;
                bool oldValue = clearOutputAfterTake;
                clearOutputAfterTake = value;
                ClearOutputAfterTakeChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ClearOutputAfterTakeChanged));
            }
        }
        #endregion

        #region Property: StoredOutput
        public delegate void AutotakeChangedDelegate(CrosspointStore crosspointStore, bool oldValue, bool newValue);
        public event AutotakeChangedDelegate AutotakeChanged;

        private bool autotake;

        public bool Autotake
        {
            get { return autotake; }
            set
            {
                if (value == autotake)
                    return;
                bool oldValue = autotake;
                autotake = value;
                AutotakeChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(Autotake));
            }
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
