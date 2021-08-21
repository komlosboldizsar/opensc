using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.Model.General;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Mirrors;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.Mirrors
{

    public partial class RouterMirrorEditorForm : ModelEditorFormBase
    {

        private const string TITLE_NEW = "New router mirror";
        private const string TITLE_EDIT = "Edit router mirror: (#{0}) {1}";

        private const string HEADER_TEXT_NEW = "New router mirror";
        private const string HEADER_TEXT_EDIT = "Edit router mirror";

        protected RouterMirror routerMirror;

        private bool addingNew = false;

        protected bool AddingNew
        {
            get { return addingNew; }
            set
            {
                addingNew = value;
                Text = string.Format((value ? TITLE_NEW : TITLE_EDIT), routerMirror?.ID, routerMirror?.Name);
                HeaderText = string.Format((value ? HEADER_TEXT_NEW : HEADER_TEXT_EDIT), routerMirror?.ID, routerMirror?.Name);
            }
        }

        public RouterMirrorEditorForm() => InitializeComponent();

        public RouterMirrorEditorForm(RouterMirror routerMirror)
        {
            InitializeComponent();
            AddingNew = (routerMirror == null);
            this.routerMirror = (routerMirror != null) ? routerMirror : new RouterMirror();
            initDropDowns();
        }

        protected override void loadData()
        {
            if (routerMirror == null)
                return;
            idNumericField.Value = (addingNew ? RouterMirrorDatabase.Instance.NextValidId() : routerMirror.ID);
            nameTextBox.Text = routerMirror.Name;
            routerAdropDown.SelectByValue(routerMirror.RouterA);
            routerBdropDown.SelectByValue(routerMirror.RouterB);
            synchronizationModeDropDown.SelectByValue(routerMirror.SynchronizationMode);
            loadAssociations<RouterInput, RouterInputProxy, RouterMirrorInputAssociation>(routerMirror.InputAssociations, routerInputProxies);
            loadAssociations<RouterOutput, RouterOutputProxy, RouterMirrorOutputAssociation>(routerMirror.OutputAssociations, routerOutputProxies);
        }

        private void loadAssociations<TAssociated, TProxy, TAssociation>(IEnumerable<TAssociation> associationCollection, IEnumerable<TProxy> proxyCollection)
            where TAssociated : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TAssociated>
            where TAssociation : RouterMirrorAssociation<TAssociated>
        {
            if (proxyCollection == null)
                return;
            foreach (TAssociation association in associationCollection)
            {
                TProxy elementProxy = proxyCollection.FirstOrDefault(proxy => (proxy.ThisElement == association.ItemA));
                if (elementProxy != null)
                    elementProxy.AssociatedElement = association.ItemB;
            }
        }

        protected sealed override bool saveData()
        {

            try
            {
                validateFields();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Data validation error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            routerMirror.StartUpdate();
            writeFields();
            routerMirror.EndUpdate();

            if (AddingNew)
                RouterMirrorDatabase.Instance.Add(routerMirror);
            AddingNew = false;

            return true;

        }

        protected virtual void validateFields()
        {
            if (routerMirror == null)
                return;
            routerMirror.ValidateId((int)idNumericField.Value);
            // TODO: validate name
        }

        protected virtual void writeFields()
        {
            if (routerMirror == null)
                return;
            routerMirror.ID = (int)idNumericField.Value;
            routerMirror.Name = nameTextBox.Text;
            routerMirror.RouterA = RouterA;
            routerMirror.RouterB = RouterB;
            routerMirror.SynchronizationMode = (RouterMirrorSynchronizationMode)synchronizationModeDropDown.SelectedValue;
            saveAssociations<RouterInput, RouterInputProxy>(routerInputProxies, (() => routerMirror.ClearInputAssociations()), ((ea, eb) => routerMirror.AddInputAssociation(ea, eb)));
            saveAssociations<RouterOutput, RouterOutputProxy>(routerOutputProxies, (() => routerMirror.ClearOutputAssociations()), ((ea, eb) => routerMirror.AddOutputAssociation(ea, eb)));
        }

        private void saveAssociations<TAssociated, TProxy>(
            IEnumerable<TProxy> proxyCollection,
            Action clearAssociationsMethod,
            Action<TAssociated, TAssociated> addAssociationMethod)
            where TAssociated : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TAssociated>
        {
            clearAssociationsMethod();
            foreach (TProxy proxy in proxyCollection)
                if (proxy.AssociatedElement != null)
                    addAssociationMethod(proxy.ThisElement, proxy.AssociatedElement);
        }

        private void initDropDowns()
        {
            // Routers
            routerAdropDown.CreateAdapterAsDataSource(RouterDatabase.Instance, router => router.Name, true, "(not associated)");
            routerBdropDown.CreateAdapterAsDataSource(RouterDatabase.Instance, router => router.Name, true, "(not associated)");
            routerAdropDown.SelectedIndexChanged += selectedRouterChangedHandler;
            routerBdropDown.SelectedIndexChanged += selectedRouterChangedHandler;
            // Synchronization mode
            EnumComboBoxAdapter<RouterMirrorSynchronizationMode> synchronizationBaseDropDownAdapter = new EnumComboBoxAdapter<RouterMirrorSynchronizationMode>(routerSynchronizationModeTranslations);
            synchronizationModeDropDown.SetAdapterAsDataSource(synchronizationBaseDropDownAdapter);
        }

        private Dictionary<RouterMirrorSynchronizationMode, string> routerSynchronizationModeTranslations = new Dictionary<RouterMirrorSynchronizationMode, string>() {
            { RouterMirrorSynchronizationMode.Never, "Never" },
            { RouterMirrorSynchronizationMode.FromSideA, "Always from router A" },
            { RouterMirrorSynchronizationMode.FromSideB, "Always from router B" },
            { RouterMirrorSynchronizationMode.FromFirstConnected, "From first connected" },
            { RouterMirrorSynchronizationMode.FromLastConnected, "From last connected" },
        };

        private void selectedRouterChangedHandler(object sender, EventArgs e)
        {
            initInputAssociationTable();
            initOutputAssociationTable();
        }

        private void initInputAssociationTable() => initAssociationsTable<RouterInput, RouterInputProxy>(
                ref inputAssociationsTableCDGV, ref this.inputAssociationsTable,
                ref inputsTableContainerPanel, ref routerInputProxies,
                RouterA?.Inputs, RouterB?.Inputs,
                (ri => ri.Name), (ri => ri.Index));

        private void initOutputAssociationTable() => initAssociationsTable<RouterOutput, RouterOutputProxy>(
                ref outputAssociationsTableCDGV, ref this.outputAssociationsTable,
                ref outputsTableContainerPanel, ref routerOutputProxies,
                RouterA?.Outputs, RouterB?.Outputs,
                (ro => ro.Name), (ro => ro.Index));

        #region Create and handle association table
        private CustomDataGridView<RouterInputProxy> inputAssociationsTableCDGV;
        private CustomDataGridView<RouterOutputProxy> outputAssociationsTableCDGV;

        private static readonly Color BACKCOLOR_ASSOCIATION_ACTIVE = Color.LightGreen;
        private static readonly Color BACKCOLOR_ASSOCIATION_INACTIVE = Color.White;
        private static readonly Color ICONCOLOR_ASSOCIATION = Color.DarkGreen;

        private void initAssociationsTable<TElement, TProxy>
            (ref CustomDataGridView<TProxy> tableCDGV, ref DataGridView tableOriginal,
            ref Panel containerPanel, ref ObservableProxyList<TProxy, TElement> proxyListRef,
            ObservableList<TElement> listA, ObservableList<TElement> listB,
            Func<TElement, string> elementNameGetter, Func<TElement, int> elementIndexGetter) 
            where TElement : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TElement>, new()
        {

            tableCDGV = createTable<TProxy>(containerPanel, ref tableOriginal);
            tableCDGV.VerticalHeader = true;
            CustomDataGridViewColumnDescriptorBuilder<TProxy> builder;

            if ((listA == null) || (listB == null))
                return;

            // Create database
            ObservableProxyList<TProxy, TElement> proxyList = new ObservableProxyList<TProxy, TElement>(listA, (elementA) => new TProxy() { ThisElement = elementA });
            proxyListRef = proxyList;

            // Column: sideA name
            builder = getColumnDescriptorBuilderForTable<TProxy>(tableCDGV);
            builder.Header("");
            builder.Width(150);
            builder.DividerWidth(3);
            builder.InitializerMethod((elementProxyA, cell) => { cell.Value = elementNameGetter(elementProxyA.ThisElement); });
            builder.BuildAndAdd();

            // Columns for associations
            foreach (TElement elementB in listB) {
                builder = getColumnDescriptorBuilderForTable<TProxy>(tableCDGV);
                builder.Type(DataGridViewColumnType.SmallIcon);
                builder.Header(elementNameGetter(elementB));
                builder.Width(30);
                builder.IconColor(ICONCOLOR_ASSOCIATION);
                builder.IconType(DataGridViewSmallIconCell.IconTypes.Circle);
                builder.IconShown(false);
                builder.UpdaterMethod((elementProxyA, cell) =>
                {
                    DataGridViewSmallIconCell typedCell = ((DataGridViewSmallIconCell)cell);
                    bool active = (elementProxyA.AssociatedElement == elementB);
                    if (active)
                    {
                        cell.Style.BackColor = BACKCOLOR_ASSOCIATION_ACTIVE;
                        typedCell.IconShown = true;
                    }
                    else
                    {
                        cell.Style.BackColor = BACKCOLOR_ASSOCIATION_INACTIVE;
                        typedCell.IconShown = false;
                    }
                });
                builder.AddChangeEvent("#" + elementIndexGetter(elementB));
                builder.CellDoubleClickHandlerMethod((elementProxyA, cell, e) => { doAssociation(elementProxyA, elementB, proxyList); });
                builder.BuildAndAdd();
            }

            // Bind database
            tableCDGV.BoundCollection = proxyList;

        }

        private void doAssociation<TProxy, TElement>(TProxy elementProxyA, TElement elementB, ObservableProxyList<TProxy, TElement> otherProxies)
            where TElement : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TElement>, new()
        {
            foreach (TProxy elementProxy in otherProxies)
                if (elementProxy.AssociatedElement == elementB)
                    elementProxy.AssociatedElement = null;
            elementProxyA.AssociatedElement = elementB;
        }
        #endregion

        #region Proxies
        private ObservableProxyList<RouterInputProxy, RouterInput> routerInputProxies;
        private ObservableProxyList<RouterOutputProxy, RouterOutput> routerOutputProxies;

        private abstract class RouterIOProxy<TElement> : Model.General.INotifyPropertyChanged
            where TElement: class, Model.General.INotifyPropertyChanged
        {

            private TElement thisElement = null;

            public TElement ThisElement
            {
                get => thisElement;
                set
                {
                    if (thisElement != null) // Settable only once
                        return;
                    thisElement = value;
                    thisElement.PropertyChanged += handlePropertyChanged;
                }
            }

            private TElement associatedElement = null;

            public TElement AssociatedElement
            {
                get => associatedElement;
                set
                {
                    if (value == associatedElement)
                        return;
                    TElement oldValue = associatedElement;
                    associatedElement = value;
                    raisePropertyChangedAssociatedElement(oldValue);
                    raisePropertyChangedAssociatedElement(value);
                }
            }

            private void raisePropertyChangedAssociatedElement(TElement element)
            {
                if (element != null)
                    PropertyChanged?.Invoke("#" + getElementIndex(element));
            }

            protected abstract int getElementIndex(TElement element);

            public RouterIOProxy()
            { }

            public event PropertyChangedDelegate PropertyChanged;
            private void handlePropertyChanged(string propertyName)
                => PropertyChanged?.Invoke(propertyName);

        }

        private class RouterInputProxy : RouterIOProxy<RouterInput>
        {
            protected override int getElementIndex(RouterInput routerInput) => routerInput.Index;
        }

        private class RouterOutputProxy : RouterIOProxy<RouterOutput>
        {
            protected override int getElementIndex(RouterOutput routerOutput) => routerOutput.Index;
        }
        #endregion

        #region Creating table
        public CustomDataGridView<T> createTable<T>(Control container, ref DataGridView originalTableMember)
            where T : class
        {
            var customTable = new CustomDataGridView<T>();
            container.Controls.Clear();
            container.Controls.Add(customTable);
            customTable.Dock = DockStyle.Fill;
            originalTableMember = customTable;
            return customTable;
        }

        private CustomDataGridViewColumnDescriptorBuilder<T> getColumnDescriptorBuilderForTable<T>(CustomDataGridView<T> table)
            where T : class
            => new CustomDataGridViewColumnDescriptorBuilder<T>(table);
        #endregion

        #region Clear associations buttons
        private void clearAssociations<TElement, TProxy>(IEnumerable<TProxy> proxies)
            where TElement : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TElement>
        {
            foreach (TProxy proxy in proxies)
                proxy.AssociatedElement = null;
        }

        private void clearInputAssociationsButton_Click(object sender, EventArgs e)
            => clearAssociations<RouterInput, RouterInputProxy>(routerInputProxies);

        private void clearOutputAssociations_Click(object sender, EventArgs e)
             => clearAssociations<RouterOutput, RouterOutputProxy>(routerOutputProxies);
        #endregion

        #region Set 1:1 associations buttons
        private void set11Associations<TElement, TProxy>(IEnumerable<TProxy> proxies, IEnumerable<TElement> bSideElements)
            where TElement : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TElement>
        {
            if ((proxies == null) || (bSideElements == null))
                return;
            using (IEnumerator<TElement> bSideElementsEnumerator = bSideElements.GetEnumerator())
            {
                TElement bSideElement;
                foreach (TProxy proxy in proxies)
                {
                    bSideElement = bSideElementsEnumerator.MoveNext() ? bSideElementsEnumerator.Current : null;
                    proxy.AssociatedElement = bSideElement;
                }
            }
        }

        private void set11InputAssociationsButton_Click(object sender, EventArgs e)
            => set11Associations<RouterInput, RouterInputProxy>(routerInputProxies, RouterB.Inputs);

        private void set11OutputAssociationsButton_Click(object sender, EventArgs e)
            => set11Associations<RouterOutput, RouterOutputProxy>(routerOutputProxies, RouterB.Outputs);
        #endregion

        private Router RouterA => routerAdropDown.SelectedValue as Router;
        private Router RouterB => routerBdropDown.SelectedValue as Router;

    }

}
