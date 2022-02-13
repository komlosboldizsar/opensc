using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Routers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    [WindowTypeName("routers.routercontrol")]
    public partial class RouterControlForm : ChildWindowWithTitle
    {

        private Router _routerTempRef;

        private Router _router;

        private Router router
        {
            get { return _router; }
            set
            {

                if (value == _router)
                    return;

                if (_router != null)
                {
                    _router.Inputs.ItemsAdded -= inputsChangedHandler;
                    _router.Inputs.ItemsRemoved -= inputsChangedHandler;
                    _router.Outputs.ItemsAdded -= outputsChangedHandler;
                    _router.Outputs.ItemsRemoved -= outputsChangedHandler;
                    Text = HeaderText = "Router crosspoints: ?";
                }

                _router = value;

                if (_router != null)
                {

                    Text = HeaderText = string.Format("Router crosspoints: {0}", router.Name);

                    _router.Inputs.ItemsAdded += inputsChangedHandler;
                    _router.Inputs.ItemsRemoved += inputsChangedHandler;
                    _router.Outputs.ItemsAdded += outputsChangedHandler;
                    _router.Outputs.ItemsRemoved += outputsChangedHandler;
                }

                loadInputs();
                loadOutputs();

            }
        }

        public RouterControlForm()
        {
            InitializeComponent();
        }

        public RouterControlForm(Router router)
        {
            InitializeComponent();
            this._routerTempRef = router;
        }

        private void RouterControlForm_Load(object sender, EventArgs e)
        {
            router = _routerTempRef;
        }      

        private Dictionary<RouterInput, RouterInputControl> inputControls = new Dictionary<RouterInput, RouterInputControl>();

        private void loadInputs()
        {
            flowLayoutPanel1.Controls.Clear();
            inputControls.Clear();
            if (router == null)
                return;
            foreach (RouterInput input in router.Inputs) {
                RouterInputControl control = new RouterInputControl(input, this);
                flowLayoutPanel1.Controls.Add(control);
                inputControls.Add(input, control);
            }
        }

        private void inputsChangedHandler(IEnumerable<Model.General.IObservableEnumerable<RouterInput>.ItemWithPosition> affectedItemsWithPositions)
            => loadInputs();

        public void InputClicked(RouterInputControl input) => SelectedInput = input.Input;

        private RouterInput selectedInput = null;

        private RouterInput SelectedInput
        {
            get { return selectedInput; }
            set
            {
                selectedInput = value;
                foreach (RouterInputControl inputControl in inputControls.Values)
                    inputControl.Selected = (inputControl.Input == value);
            }
        }

        private Dictionary<RouterOutput, RouterOutputControl> outputControls = new Dictionary<RouterOutput, RouterOutputControl>();

        private void loadOutputs()
        {
            outputControls.Clear();
            flowLayoutPanel2.Controls.Clear();
            if (router == null)
                return;
            foreach (RouterOutput output in router.Outputs)
            {
                RouterOutputControl control = new RouterOutputControl(output, this);
                flowLayoutPanel2.Controls.Add(control);
                outputControls.Add(output, control);
            }
        }

        private void outputsChangedHandler(IEnumerable<Model.General.IObservableEnumerable<RouterOutput>.ItemWithPosition> affectedItemsWithPositions)
            => loadOutputs();

        public void OutputClicked(RouterOutputControl output)
        {
            output.Selected = !output.Selected;
            if (output.Selected)
                selectedOutputs.Add(output.Output);
            else
                selectedOutputs.RemoveAll(o => (o == output.Output));
        }

        private List<RouterOutput> selectedOutputs = new List<RouterOutput>();

        private void selectNoOutput()
        {
            selectedOutputs.Clear();
            foreach (RouterOutputControl control in outputControls.Values)
                control.Selected = false;
        }

        #region Persistence
        private const string PERSISTENCE_KEY_ROUTER_ID = "router_id";

        protected override void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        {
            base.restoreBeforeOpen(keyValuePairs);
            _routerTempRef = RouterDatabase.Instance.GetTById((int)keyValuePairs[PERSISTENCE_KEY_ROUTER_ID]);
        }

        public override Dictionary<string, object> GetKeyValuePairs()
        {
            var dict = base.GetKeyValuePairs();
            dict.Add(PERSISTENCE_KEY_ROUTER_ID, router?.ID);
            return dict;
        }
        #endregion

        private void takeButton_Click(object sender, EventArgs e)
        {
            take();
        }

        private void take()
        {
            if ((router == null) || (SelectedInput == null))
                return;
            foreach (RouterOutput output in selectedOutputs)
                router.RequestCrosspointUpdate(output, SelectedInput);
            SelectedInput = null;
            selectNoOutput();
        }

    }

}
