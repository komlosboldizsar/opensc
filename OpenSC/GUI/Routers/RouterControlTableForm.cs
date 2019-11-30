using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.General;
using OpenSC.Model.Routers;
using OpenSC.Model.Signals;
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

        private List<Router> _routersTempRef = new List<Router>();

        private List<Router> _routers = new List<Router>();

        private List<Router> routers
        {
            get { return _routers; }
            set
            {

                foreach (Router router in _routers)
                {
                    router.Inputs.ItemsChanged -= inputsChangedHandler;
                    router.Outputs.ItemsChanged -= outputsChangedHandler;
                }
                Text = HeaderText = "Router crosspoints: ?";

                _routers.Clear();
                if (value != null)
                    _routers.AddRange(value);

                foreach (Router router in _routers)
                {
                    router.Inputs.ItemsChanged += inputsChangedHandler;
                    router.Outputs.ItemsChanged += outputsChangedHandler;
                }

                Text = HeaderText =
                    singleMode
                    ? string.Format("Router crosspoints: {0}", _routers[0].Name)
                    : "Router crosspoints";

                initializeTable();

            }
        }

        private bool singleMode => (_routers.Count == 1);
        public RouterControlTableForm()
        {
            InitializeComponent();
        }

        public RouterControlTableForm(Router router)
        {
            InitializeComponent();
            this._routersTempRef.Clear();
            this._routersTempRef.Add(router);
        }

        public RouterControlTableForm(IEnumerable<Router> routers)
        {
            InitializeComponent();
            this._routersTempRef.Clear();
            this._routersTempRef.AddRange(routers);
        }

        private void RouterControlForm_Load(object sender, EventArgs e)
        {
            routers = _routersTempRef;
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

            bool _singleMode = singleMode;

            // Column: output name
            builder = getColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("");
            builder.Width(150);
            builder.DividerWidth(3);
            builder.UpdaterMethod((routerOutputProxy, cell) => {
                if (_singleMode)
                    cell.Value = routerOutputProxy.Name;
                else
                    cell.Value = string.Format("[{0}] {1}", routerOutputProxy.RouterName, routerOutputProxy.Name);
            });
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

            List<IRouterOutputAssignable> assignables = getAllAssignables();
            foreach (IRouterOutputAssignable assignable in assignables)
            {
                builder = getColumnDescriptorBuilderForTable();
                builder.Type(DataGridViewColumnType.SmallIcon);
                builder.Header(assignable.Name);
                builder.Width(30);
                builder.IconColor(Color.Red);
                builder.IconType(DataGridViewSmallIconCell.IconTypes.Circle);
                builder.IconShown(false);
                builder.CellDoubleClickHandlerMethod((routerOutputProxy, cell, e) => {
                    if (autotake)
                        doAssign(routerOutputProxy.Output, assignable);
                    else
                        routerOutputProxy.SelectedToAssign = assignable;
                });
                builder.UpdaterMethod((routerOutputProxy, cell) => {

                    DataGridViewSmallIconCell typedCell = ((DataGridViewSmallIconCell)cell);

                    bool active = _singleMode
                        ? (routerOutputProxy.ActiveAssigned == assignable)
                        : (routerOutputProxy.ActiveAssigned?.SourceSignal == assignable.SourceSignal);

                    bool selected = _singleMode
                        ? (routerOutputProxy.SelectedToAssign == assignable)
                        : ((routerOutputProxy.SelectedToAssign != null) && (routerOutputProxy.SelectedToAssign?.SourceSignal == assignable.SourceSignal));

                    if (active)
                    {
                        cell.Style.BackColor = BACKCOLOR_CROSSPOINT_ACTIVE;
                        typedCell.IconShown = true;
                        typedCell.IconColor = ICONCOLOR_CROSSPOINT_ACTIVE;
                    }
                    else if (selected)
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
                builder.AddChangeEvent(nameof(RouterOutputProxy.ActiveAssigned));
                builder.AddChangeEvent(nameof(RouterOutputProxy.SelectedToAssign));
                builder.BuildAndAdd();
            }

            // Bind database
            ObservableList<RouterOutput> allOutputs = getAllOutputs();
            routerOutputProxies = new ObservableProxyList<RouterOutputProxy, RouterOutput>(allOutputs, (routerOutput) => new RouterOutputProxy(routerOutput));
            table.BoundCollection = routerOutputProxies;

        }

        private CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy> getColumnDescriptorBuilderForTable()
        {
            return new CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy>((CustomDataGridView<RouterOutputProxy>)table);
        }

        private List<IRouterOutputAssignable> getAllAssignables()
        {
            if (!singleMode)
                return RouterOutputAssignableExternalSignal.GetAll();
            List<IRouterOutputAssignable> inputs = new List<IRouterOutputAssignable>();
            inputs.AddRange(routers[0].Inputs);
            return inputs;
        }

        private ObservableList<RouterOutput> getAllOutputs()
        {
            ObservableList<RouterOutput> outputs = new ObservableList<RouterOutput>();
            foreach (Router router in routers)
                outputs.AddRange(router.Outputs);
            return outputs;
        }
        #endregion

        #region Proxies
        private ObservableProxyList<RouterOutputProxy, RouterOutput> routerOutputProxies;

        private class RouterOutputProxy : Model.General.INotifyPropertyChanged
        {

            public RouterOutput Output { get; private set; }

            public RouterOutputProxy(RouterOutput routerOutput)
            {
                this.Output = routerOutput;
                routerOutput.NameChanged += RouterOutput_NameChanged;
                routerOutput.Router.NameChanged += Router_NameChanged;
                routerOutput.CrosspointChanged += RouterOutput_CrosspointChanged;
            }

            #region Property: Name
            public string Name => Output.Name;

            private void RouterOutput_NameChanged(RouterOutput output, string oldName, string newName)
            {
                PropertyChanged?.Invoke(nameof(Name));
            }
            #endregion

            #region Property: RouterName
            public string RouterName => Output.Router.Name;

            private void Router_NameChanged(Router output, string oldName, string newName)
            {
                PropertyChanged?.Invoke(nameof(RouterName));
            }
            #endregion

            #region Active assignable
            public IRouterOutputAssignable ActiveAssigned
                => Output.Crosspoint;

            private void RouterOutput_CrosspointChanged(RouterOutput output, RouterInput newInput)
            {
                PropertyChanged?.Invoke(nameof(ActiveAssigned));
            }
            #endregion

            #region Selected assignable
            private IRouterOutputAssignable selectedToAssign = null;

            public IRouterOutputAssignable SelectedToAssign
            {
                get { return selectedToAssign; }
                set
                {
                    IRouterOutputAssignable newSelectedAssignable = value;
                    if (newSelectedAssignable == ActiveAssigned)
                        newSelectedAssignable = null;
                    IRouterOutputAssignable oldSelectedCrosspoint = selectedToAssign;
                    selectedToAssign = newSelectedAssignable;
                    if (oldSelectedCrosspoint != newSelectedAssignable)
                        PropertyChanged?.Invoke(nameof(SelectedToAssign));
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
        private const string PERSISTENCE_KEY_ROUTER_IDS = "router_ids";

        protected override void restoreBeforeOpen(Dictionary<string, object> keyValuePairs)
        {
            base.restoreBeforeOpen(keyValuePairs);
            string[] routerIdsStr = keyValuePairs[PERSISTENCE_KEY_ROUTER_IDS].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            _routersTempRef.Clear();
            foreach (string routerIdStr in routerIdsStr) {
                if (!int.TryParse(routerIdStr, out int routerId))
                    continue;
                Router router = RouterDatabase.Instance.GetTById(routerId);
                if (router != null)
                    _routersTempRef.Add(router);
            }
        }

        public override Dictionary<string, object> GetKeyValuePairs()
        {
            var dict = base.GetKeyValuePairs();
            List<string> routerIdsStr = new List<string>();
            routers.ForEach(r => routerIdsStr.Add(r.ID.ToString()));
            dict.Add(PERSISTENCE_KEY_ROUTER_IDS, string.Join(",", routerIdsStr));
            return dict;
        }
        #endregion

        #region Take

        private bool autotake = false;

        private bool Autotake
        {
            get => autotake;
            set
            {
                autotake = value;
                autotakeButton.ForeColor = autotake ? AUTOTAKE_ACTIVE_FOREGROUND : AUTOTAKE_INCTIVE_FOREGROUND;
                autotakeButton.BackColor = autotake ? AUTOTAKE_ACTIVE_BACKGROUND : AUTOTAKE_INCTIVE_BACKGROUND;
                autotakeButton.FlatAppearance.BorderColor = autotake ? AUTOTAKE_ACTIVE_BORDER : AUTOTAKE_INCTIVE_BORDER;
            }
        }

        private static Color AUTOTAKE_ACTIVE_FOREGROUND = Color.FromArgb(255, 224, 192);
        private static Color AUTOTAKE_ACTIVE_BACKGROUND = Color.FromArgb(192, 164, 0);
        private static Color AUTOTAKE_ACTIVE_BORDER = Color.FromArgb(192, 164, 0);

        private static Color AUTOTAKE_INCTIVE_FOREGROUND = Color.FromArgb(192, 164, 0);
        private static Color AUTOTAKE_INCTIVE_BACKGROUND = Color.FromArgb(255, 224, 192);
        private static Color AUTOTAKE_INCTIVE_BORDER = Color.FromArgb(192, 164, 0);

        private void autotakeButton_Click(object sender, EventArgs e)
        {
            Autotake = !Autotake;
        }

        private void takeButton_Click(object sender, EventArgs e)
        {
            take();
        }

        private void take()
        {
            foreach (RouterOutputProxy routerOutputProxy in routerOutputProxies)
            {
                if (routerOutputProxy.SelectedToAssign != null)
                {
                    doAssign(routerOutputProxy.Output, routerOutputProxy.SelectedToAssign);
                    routerOutputProxy.SelectedToAssign = null;
                }
            }
        }

        private void doAssign(RouterOutput output, IRouterOutputAssignable assignable)
        {
            if (singleMode)
            {
                RouterInput input = assignable as RouterInput;
                if (input == null)
                    return;
                if (!output.Router.Inputs.Contains(input))
                    return;
                output.Crosspoint = input;
                return;
            }
            MessageBox.Show("Not implemented yet!", "Auto path search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        #endregion

    }

}
