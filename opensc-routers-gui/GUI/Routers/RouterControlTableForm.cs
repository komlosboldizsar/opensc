using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
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
using INotifyPropertyChanged = OpenSC.Model.General.INotifyPropertyChanged;

namespace OpenSC.GUI.Routers
{

    [WindowTypeName("routers.routercontroltable")]
    public partial class RouterControlTableForm : ChildWindowWithTitle
    {

        private List<Router> _routersTempRef = new List<Router>();

        private List<Router> _routers = new List<Router>();

        private List<Router> routers
        {
            get => _routers;
            set
            {

                foreach (Router router in _routers)
                {
                    router.Inputs.ItemsAdded += inputsChangedHandler;
                    router.Inputs.ItemsRemoved += inputsChangedHandler;
                    router.Outputs.ItemsAdded += outputsChangedHandler;
                    router.Outputs.ItemsRemoved += outputsChangedHandler;
                }
                Text = HeaderText = "Router crosspoints: ?";

                _routers.Clear();
                if (value != null)
                    _routers.AddRange(value);

                foreach (Router router in _routers)
                {
                    router.Inputs.ItemsAdded -= inputsChangedHandler;
                    router.Inputs.ItemsRemoved -= inputsChangedHandler;
                    router.Outputs.ItemsAdded -= outputsChangedHandler;
                    router.Outputs.ItemsRemoved -= outputsChangedHandler;
                }

                Text = HeaderText =
                    singleMode
                    ? string.Format("Router crosspoints: {0}", _routers[0].Name)
                    : "Router crosspoints";

                initializeTable();

            }
        }

        private bool singleMode => (_routers.Count == 1);

        public RouterControlTableForm() => InitializeComponent();

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

        private void RouterControlForm_Load(object sender, EventArgs e) => routers = _routersTempRef;

        #region Table

        private static readonly Color BACKCOLOR_CROSSPOINT_ACTIVE = Color.LightPink;
        private static readonly Color ICONCOLOR_CROSSPOINT_ACTIVE = Color.Red;

        private static readonly Color BACKCOLOR_CROSSPOINT_SELECTED = Color.LightBlue;
        private static readonly Color ICONCOLOR_CROSSPOINT_SELECTED = Color.Blue;

        private static readonly Color BACKCOLOR_CROSSPOINT_EMPTY = Color.White;
        private static readonly Color ICONCOLOR_CROSSPOINT_EMPTY = Color.Black;

        private static readonly Color BACKCOLOR_LOCK_NOTSUPPORTED = Color.DarkGray;
        private static readonly Color BACKCOLOR_LOCK_INACTIVE = Color.White;
        private static readonly Color BACKCOLOR_LOCK_ACTIVE = Color.LightBlue;
        private static readonly Color TEXTCOLOR_LOCK_ACTIVE = Color.DarkBlue;

        private static readonly Color BACKCOLOR_PROTECT_NOTSUPPORTED = Color.DarkGray;
        private static readonly Color BACKCOLOR_PROTECT_INACTIVE = Color.White;
        private static readonly Color BACKCOLOR_PROTECT_ACTIVE = Color.LightGreen;
        private static readonly Color TEXTCOLOR_PROTECT_ACTIVE = Color.DarkGreen;

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

            DataGridViewCellStyle lockProtectColumnCellStyle = crosspointsTable.DefaultCellStyle.Clone();
            lockProtectColumnCellStyle.Font = new Font(crosspointsTable.DefaultCellStyle.Font, FontStyle.Bold);
            lockProtectColumnCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewCellStyle lockColumnCellStyle = lockProtectColumnCellStyle.Clone();
            lockColumnCellStyle.ForeColor = TEXTCOLOR_LOCK_ACTIVE;

            DataGridViewCellStyle protectColumnCellStyle = lockProtectColumnCellStyle.Clone();
            protectColumnCellStyle.ForeColor = TEXTCOLOR_PROTECT_ACTIVE;

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
            builder.UpdaterMethod((routerOutputProxy, cell) => {
                RouterOutput ro = routerOutputProxy.Original;
                if (!ro.Lock.Supported)
                    return;
                cell.Value = lockStateToStringConverter.Convert(ro.Lock.State);
                cell.Style.BackColor = (ro.Lock.State == RouterOutputLockState.Clear) ? BACKCOLOR_LOCK_INACTIVE : BACKCOLOR_LOCK_ACTIVE;
            });
            builder.InitializerMethod((routerOutputProxy, cell) => {
                RouterOutput ro = routerOutputProxy.Original;
                if (!ro.Lock.Supported)
                    cell.Style.BackColor = BACKCOLOR_LOCK_NOTSUPPORTED;
            });
            builder.CellStyle(lockColumnCellStyle);
            builder.AddMultilevelChangeEvent(nameof(RouterOutput.Lock), nameof(RouterOutputLock.State));
            builder.CellDoubleClickHandlerMethod((routerOutputProxy, cell, e) => lockOperation(routerOutputProxy.Original.Lock));
            builder.BuildAndAdd();

            // Column: protect
            builder = getColumnDescriptorBuilderForTable();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Protect");
            builder.Width(30);
            builder.DividerWidth(3);
            builder.UpdaterMethod((routerOutputProxy, cell) => {
                RouterOutput ro = routerOutputProxy.Original;
                if (!ro.Protect.Supported)
                    return;
                cell.Value = lockStateToStringConverter.Convert(ro.Protect.State);
                cell.Style.BackColor = (ro.Protect.State == RouterOutputLockState.Clear) ? BACKCOLOR_PROTECT_INACTIVE : BACKCOLOR_PROTECT_ACTIVE;
            });
            builder.InitializerMethod((routerOutputProxy, cell) => {
                RouterOutput ro = routerOutputProxy.Original;
                if (!ro.Protect.Supported)
                    cell.Style.BackColor = BACKCOLOR_PROTECT_NOTSUPPORTED;
            });
            builder.CellStyle(protectColumnCellStyle);
            builder.AddMultilevelChangeEvent(nameof(RouterOutput.Protect));
            builder.CellDoubleClickHandlerMethod((routerOutputProxy, cell, e) => lockOperation(routerOutputProxy.Original.Protect));
            builder.BuildAndAdd();

            List<ISignalSource> assignables = getAllAssignables();
            foreach (ISignalSource assignable in assignables)
            {
                builder = getColumnDescriptorBuilderForTable();
                builder.Type(DataGridViewColumnType.SmallIcon);
                if (_singleMode)
                    builder.Header((assignable as RouterInput)?.Name);
                else
                    builder.Header(assignable.RegisteredSourceSignalName);
                builder.Width(30);
                builder.IconColor(Color.Red);
                builder.IconType(DataGridViewSmallIconCell.IconTypes.Circle);
                builder.IconShown(false);
                builder.CellDoubleClickHandlerMethod((routerOutputProxy, cell, e) => {
                    if (autotake)
                        doAssign(routerOutputProxy.Original, assignable);
                    else
                        routerOutputProxy.SelectedToAssign = assignable;
                });
                builder.UpdaterMethod((routerOutputProxy, cell) => {

                    DataGridViewSmallIconCell typedCell = ((DataGridViewSmallIconCell)cell);

                    bool active = _singleMode
                        ? (routerOutputProxy.CurrentSource == assignable)
                        : (routerOutputProxy.CurrentSource?.RegisteredSourceSignal == assignable.RegisteredSourceSignal);

                    bool selected = _singleMode
                        ? (routerOutputProxy.SelectedToAssign == assignable)
                        : ((routerOutputProxy.SelectedToAssign != null) && (routerOutputProxy.SelectedToAssign?.RegisteredSourceSignal == assignable.RegisteredSourceSignal));

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
                builder.AddChangeEvent(nameof(RouterOutputProxy.CurrentSource));
                builder.AddChangeEvent(nameof(RouterOutputProxy.SelectedToAssign));
                builder.BuildAndAdd();
            }

            // Bind database
            ObservableList<RouterOutput> allOutputs = getAllOutputs();
            routerOutputProxies = new ObservableProxyEnumerable<RouterOutputProxy, RouterOutput>(allOutputs, (routerOutput) => new RouterOutputProxy(routerOutput));
            table.BoundCollection = routerOutputProxies;

        }

        private CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy> getColumnDescriptorBuilderForTable()
        {
            return new CustomDataGridViewColumnDescriptorBuilder<RouterOutputProxy>((CustomDataGridView<RouterOutputProxy>)table);
        }

        private List<ISignalSource> getAllAssignables()
        {
            if (!singleMode)
                return SignalRegister.Instance.ToList<ISignalSource>();
            List<ISignalSource> inputs = new List<ISignalSource>();
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

        private static readonly EnumToStringConverter<RouterOutputLockState> lockStateToStringConverter = new EnumToStringConverter<RouterOutputLockState>()
        {
            { RouterOutputLockState.Clear, "" },
            { RouterOutputLockState.Locked, "X" },
            { RouterOutputLockState.LockedLocal, "L" },
            { RouterOutputLockState.LockedRemote, "R" },
        };

        private void lockOperation(RouterOutputLock @lock)
        {
            try
            {
                switch (@lock.State)
                {
                    case RouterOutputLockState.Clear:
                        @lock.Do();
                        break;
                    case RouterOutputLockState.Locked:
                    case RouterOutputLockState.LockedLocal:
                        @lock.Undo();
                        break;
                    case RouterOutputLockState.LockedRemote:
                        RoutersGuiUtilities.ForceUndoWithPrompt(@lock);
                        break;
                }
            }
            catch (RouterOutputLockOperationException ex)
            {
                RoutersGuiUtilities.ShowLockOperationFailedAlert(ex);
            }
        }

        #region Proxies
        private ObservableProxyEnumerable<RouterOutputProxy, RouterOutput> routerOutputProxies;

        private class RouterOutputProxy : ObjectProxy<RouterOutput>
        {

            private static readonly Dictionary<string, string> ROUTER_OUTPUT_PROXY_PROPERTY_BINDINGS = new Dictionary<string, string>()
            {
                { nameof(RouterOutput.Name), nameof(RouterOutputProxy.Name) }
            };

            public RouterOutputProxy(RouterOutput routerOutput)
                : base(routerOutput, ROUTER_OUTPUT_PROXY_PROPERTY_BINDINGS)
            {
                routerOutput.Router.NameChanged += Router_NameChanged;
                routerOutput.CurrentSourceChanged += RouterOutput_CurrentSourceChanged; ;
            }

            public string Name => Original.Name;

            public string RouterName => Original.Router.Name;
            private void Router_NameChanged(IModel item, string oldValue, string newValue) => RaisePropertyChanged(nameof(RouterName));

            public ISignalSource CurrentSource => Original.CurrentSource;
            private void RouterOutput_CurrentSourceChanged(ISignalDestination signalDestination, ISignalSource newSource) => RaisePropertyChanged(nameof(CurrentSource));
           
            #region Selected assignable
            private ISignalSource selectedToAssign = null;

            public ISignalSource SelectedToAssign
            {
                get => selectedToAssign;
                set
                {
                    ISignalSource newSelectedAssignable = value;
                    if (newSelectedAssignable == CurrentSource)
                        newSelectedAssignable = null;
                    ISignalSource oldSelectedCrosspoint = selectedToAssign;
                    selectedToAssign = newSelectedAssignable;
                    if (oldSelectedCrosspoint != newSelectedAssignable)
                        RaisePropertyChanged(nameof(SelectedToAssign));
                }
            }
            #endregion

        }

        #endregion

        private void inputsChangedHandler(IEnumerable<IObservableEnumerable<RouterInput>.ItemWithPosition> affectedItemsWithPositions) => initializeTable();
        private void outputsChangedHandler(IEnumerable<IObservableEnumerable<RouterOutput>.ItemWithPosition> affectedItemsWithPositions) => initializeTable();

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

        private void autotakeButton_Click(object sender, EventArgs e) => Autotake = !Autotake;
        private void takeButton_Click(object sender, EventArgs e) => take();

        private void take()
        {
            foreach (RouterOutputProxy routerOutputProxy in routerOutputProxies)
            {
                if (routerOutputProxy.SelectedToAssign != null)
                {
                    doAssign(routerOutputProxy.Original, routerOutputProxy.SelectedToAssign);
                    routerOutputProxy.SelectedToAssign = null;
                }
            }
        }

        private void doAssign(RouterOutput output, ISignalSource assignable)
        {

            if (singleMode)
            {
                RouterInput input = assignable as RouterInput;
                if (input == null)
                    return;
                if (!output.Router.Inputs.Contains(input))
                    return;
                output.RequestCrosspointUpdate(input);
                return;
            }

            AutoPathSearcher aps = new AutoPathSearcher(assignable.RegisteredSourceSignal, output);
            if (aps.Possible != true)
            {
                MessageBox.Show("Assignment cannot be done, no path found.", "Automatic path search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            aps.TakeCrosspointsAndReserve();

        }
        #endregion

    }

}
