using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.General;
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

    [WindowTypeName("routers.routercontroltable")]
    public partial class RouterControlTableForm : ChildWindowWithTitle
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
                    _router.Inputs.ItemsChanged -= inputsChangedHandler;
                    _router.Outputs.ItemsChanged -= outputsChangedHandler;
                    Text = HeaderText = "Router crosspoints: ?";
                }

                _router = value;

                if (_router != null)
                {

                    Text = HeaderText = string.Format("Router crosspoints: {0}", router.Name);
                    _router.Inputs.ItemsChanged += inputsChangedHandler;
                    _router.Outputs.ItemsChanged += outputsChangedHandler;
                }

                initializeTable();

            }
        }

        public RouterControlTableForm()
        {
            InitializeComponent();
        }

        public RouterControlTableForm(Router router)
        {
            InitializeComponent();
            this._routerTempRef = router;
        }

        private void RouterControlForm_Load(object sender, EventArgs e)
        {
            router = _routerTempRef;
        }

        

        #region Table

        private static readonly Color BACKCOLOR_CROSSPOINT_ACTIVE = Color.LightPink;
        private static readonly Color ICONCOLOR_CROSSPOINT_ACTIVE = Color.Red;

        private static readonly Color BACKCOLOR_CROSSPOINT_SELECTED = Color.LightBlue;
        private static readonly Color ICONCOLOR_CROSSPOINT_SELECTED = Color.Blue;

        private static readonly Color BACKCOLOR_CROSSPOINT_EMPTY = Color.White;
        private static readonly Color ICONCOLOR_CROSSPOINT_EMPTY = Color.Black;

        private CustomDataGridView<RouterOutputProxy> table;

        private void initializeTable()
        {

            crosspointsTable = new CustomDataGridView<RouterOutputProxy>();
            crosspointsTableContainer.Controls.Clear();
            crosspointsTableContainer.Controls.Add(crosspointsTable);
            crosspointsTable.Dock = DockStyle.Fill;
            table = (CustomDataGridView<RouterOutputProxy>)crosspointsTable;
            table.VerticalHeader = true;

            CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy> builder;

            // Column: output name
            builder = getColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("");
            builder.Width(150);
            builder.DividerWidth(3);
            builder.UpdaterMethod((routerOutputProxy, cell) => { cell.Value = routerOutputProxy.Name; });
            builder.AddChangeEvent(nameof(RouterOutput.Name));
            builder.BuildAndAdd();

            // Column: lock
            builder = getColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Lock");
            builder.Width(30);
            builder.UpdaterMethod((routerOutputProxy, cell) => { cell.Value = ""; });
            builder.AddChangeEvent(nameof(RouterOutput.Name));
            builder.BuildAndAdd();

            // Column: protect
            builder = getColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Protect");
            builder.Width(30);
            builder.DividerWidth(3);
            builder.UpdaterMethod((routerOutputProxy, cell) => { cell.Value = ""; });
            builder.AddChangeEvent(nameof(RouterOutput.Name));
            builder.BuildAndAdd();

            foreach (RouterInput routerInput in router.Inputs)
            {
                builder = getColumnDescriptorBuilderForTable();
                builder.Type(DataGridViewColumnType.SmallIcon);
                builder.Header(routerInput.Name);
                builder.Width(30);
                builder.IconColor(Color.Red);
                builder.IconType(DataGridViewSmallIconCell.IconTypes.Circle);
                builder.IconShown(false);
                builder.CellDoubleClickHandlerMethod((routerOutputProxy, cell, e) => {
                    if (autotake)
                        routerOutputProxy.ActiveCrosspoint = routerInput;
                    else
                        routerOutputProxy.SelectedCrosspoint = routerInput;
                });
                builder.UpdaterMethod((routerOutputProxy, cell) => {
                    DataGridViewSmallIconCell typedCell = ((DataGridViewSmallIconCell)cell);
                    if (routerOutputProxy.ActiveCrosspoint == routerInput)
                    {
                        cell.Style.BackColor = BACKCOLOR_CROSSPOINT_ACTIVE;
                        typedCell.IconShown = true;
                        typedCell.IconColor = ICONCOLOR_CROSSPOINT_ACTIVE;
                    }
                    else if (routerOutputProxy.SelectedCrosspoint == routerInput)
                    {
                        cell.Style.BackColor = BACKCOLOR_CROSSPOINT_SELECTED;
                        typedCell.IconShown = true;
                        typedCell.IconColor = ICONCOLOR_CROSSPOINT_SELECTED;
                    }
                    else
                    {
                        cell.Style.BackColor = BACKCOLOR_CROSSPOINT_EMPTY;
                        typedCell.IconShown = false;
                        typedCell.IconColor = ICONCOLOR_CROSSPOINT_EMPTY;
                    }
                });
                builder.AddChangeEvent(nameof(RouterOutputProxy.ActiveCrosspoint));
                builder.AddChangeEvent(nameof(RouterOutputProxy.SelectedCrosspoint));
                builder.BuildAndAdd();
            }

            // Bind database
            routerOutputProxies = new ObservableProxyList<RouterOutputProxy, RouterOutput>(router.Outputs, (routerOutput) => new RouterOutputProxy(routerOutput));
            table.BoundCollection = routerOutputProxies;

        }

        private CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy> getColumnDescriptorBuilderForTable()
        {
            return new CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy>((CustomDataGridView<RouterOutputProxy>)table);
        }
        #endregion

        #region Proxies
        private ObservableProxyList<RouterOutputProxy, RouterOutput> routerOutputProxies;

        private class RouterOutputProxy : Model.General.INotifyPropertyChanged
        {

            private RouterOutput routerOutput;

            public RouterOutputProxy(RouterOutput routerOutput)
            {
                this.routerOutput = routerOutput;
                routerOutput.NameChanged += RouterOutput_NameChanged;
                routerOutput.CrosspointChanged += RouterOutput_CrosspointChanged;
            }

            #region Property: Name
            public string Name => routerOutput.Name;

            private void RouterOutput_NameChanged(RouterOutput output, string oldName, string newName)
            {
                PropertyChanged?.Invoke(nameof(Name));
            }
            #endregion

            #region Active crosspoint
            public RouterInput ActiveCrosspoint
            {
                get => routerOutput.Crosspoint;
                set { routerOutput.Crosspoint = value; }
            }

            private void RouterOutput_CrosspointChanged(RouterOutput output, RouterInput newInput)
            {
                PropertyChanged?.Invoke(nameof(ActiveCrosspoint));
            }
            #endregion

            #region Selected crosspoint
            private RouterInput selectedCrosspoint = null;

            public RouterInput SelectedCrosspoint
            {
                get { return selectedCrosspoint; }
                set
                {
                    RouterInput newSelectedCrosspoint = value;
                    if (newSelectedCrosspoint == ActiveCrosspoint)
                        newSelectedCrosspoint = null;
                    RouterInput oldSelectedCrosspoint = selectedCrosspoint;
                    selectedCrosspoint = newSelectedCrosspoint;
                    if (oldSelectedCrosspoint != newSelectedCrosspoint)
                        PropertyChanged?.Invoke(nameof(SelectedCrosspoint));
                }
            }
            #endregion

            public event PropertyChangedDelegate PropertyChanged;

        }

        #endregion

        private void inputsChangedHandler()
        {
            initializeTable();
        }
        private void outputsChangedHandler()
        {
            initializeTable();
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

        private bool autotake;

        private bool Autotake
        {
            get => autotake;
            set
            {
                autotake = value;
            }
        }

        private void takeButton_Click(object sender, EventArgs e)
        {
            take();
        }

        private void take()
        {
            // Do something
        }

    }

}
