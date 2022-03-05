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
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using INotifyPropertyChanged = OpenSC.Model.General.INotifyPropertyChanged;

namespace OpenSC.GUI.Routers.Mirrors
{

    public partial class RouterMirrorEditorForm : ModelEditorFormBase, IModelEditorForm<RouterMirror>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as RouterMirror);
        public IModelEditorForm<RouterMirror> GetInstanceT(RouterMirror modelInstance) => new RouterMirrorEditorForm(modelInstance);

        public RouterMirrorEditorForm() : base() => InitializeComponent();

        public RouterMirrorEditorForm(RouterMirror routerMirror)
            : base(routerMirror)
        {
            InitializeComponent();
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<RouterMirror, RouterMirror>(this, RouterMirrorDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            RouterMirror routerMirror = (RouterMirror)EditedModel;
            if (routerMirror == null)
                return;
            routerAdropDown.SelectByValue(routerMirror.RouterA);
            routerBdropDown.SelectByValue(routerMirror.RouterB);
            synchronizationModeDropDown.SelectByValue(routerMirror.SynchronizationMode);
            loadAssociations<RouterInput, RouterInputProxy, RouterMirrorInputAssociation>(routerMirror.InputAssociations, routerInputProxies);
            loadAssociations<RouterOutput, RouterOutputProxy, RouterMirrorOutputAssociation>(routerMirror.OutputAssociations, routerOutputProxies);
        }

        protected override void validateFields()
        {
            base.validateFields();
            RouterMirror routerMirror = (RouterMirror)EditedModel;
            if (routerMirror == null)
                return;
        }

        protected override void writeFields()
        {
            base.writeFields();
            RouterMirror routerMirror = (RouterMirror)EditedModel;
            if (routerMirror == null)
                return;
            routerMirror.RouterA = routerA;
            routerMirror.RouterB = routerB;
            routerMirror.SynchronizationMode = (RouterMirrorSynchronizationMode)synchronizationModeDropDown.SelectedValue;
            saveAssociations<RouterInput, RouterInputProxy>(routerInputProxies, (() => routerMirror.ClearInputAssociations()), ((ea, eb) => routerMirror.AddInputAssociation(ea, eb)));
            saveAssociations<RouterOutput, RouterOutputProxy>(routerOutputProxies, (() => routerMirror.ClearOutputAssociations()), ((ea, eb) => routerMirror.AddOutputAssociation(ea, eb)));
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
                TProxy elementProxy = proxyCollection.FirstOrDefault(proxy => (proxy.Original == association.ItemA));
                if (elementProxy != null)
                    elementProxy.AssociatedElement = association.ItemB;
            }
        }

        private void saveAssociations<TAssociated, TProxy>(IEnumerable<TProxy> proxyCollection, Action clearAssociationsMethod, Action<TAssociated, TAssociated> addAssociationMethod)
            where TAssociated : class, Model.General.INotifyPropertyChanged
            where TProxy : RouterIOProxy<TAssociated>
        {
            clearAssociationsMethod();
            foreach (TProxy proxy in proxyCollection)
                if (proxy.AssociatedElement != null)
                    addAssociationMethod(proxy.Original, proxy.AssociatedElement);
        }

        private void initDropDowns()
        {
            // Routers
            routerAdropDown.CreateAdapterAsDataSource(RouterDatabase.Instance, null, true, "(not associated)");
            routerBdropDown.CreateAdapterAsDataSource(RouterDatabase.Instance, null, true, "(not associated)");
            routerAdropDown.SelectedIndexChanged += selectedRouterChangedHandler;
            routerBdropDown.SelectedIndexChanged += selectedRouterChangedHandler;
            routerAdropDown.ReceiveSystemObjectDrop();
            routerBdropDown.ReceiveSystemObjectDrop();
            routerAdropDown.FilterSystemObjectDropByType<Router>();
            routerBdropDown.FilterSystemObjectDropByType<Router>();
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
                routerA?.Inputs, routerB?.Inputs,
                (ri => ri.Name), (ri => ri.Index));

        private void initOutputAssociationTable() => initAssociationsTable<RouterOutput, RouterOutputProxy>(
                ref outputAssociationsTableCDGV, ref this.outputAssociationsTable,
                ref outputsTableContainerPanel, ref routerOutputProxies,
                routerA?.Outputs, routerB?.Outputs,
                (ro => ro.Name), (ro => ro.Index));

        #region Create and handle association table
        private CustomDataGridView<RouterInputProxy> inputAssociationsTableCDGV;
        private CustomDataGridView<RouterOutputProxy> outputAssociationsTableCDGV;

        private static readonly Color BACKCOLOR_ASSOCIATION_ACTIVE = Color.LightGreen;
        private static readonly Color BACKCOLOR_ASSOCIATION_INACTIVE = Color.White;
        private static readonly Color ICONCOLOR_ASSOCIATION = Color.DarkGreen;

        private void initAssociationsTable<TElement, TProxy>
            (ref CustomDataGridView<TProxy> tableCDGV, ref DataGridView tableOriginal,
            ref Panel containerPanel, ref ObservableProxyEnumerable<TProxy, TElement> proxyListRef,
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
            ObservableProxyEnumerable<TProxy, TElement> proxyList = new ObservableProxyEnumerable<TProxy, TElement>(listA, (elementA) => new TProxy() { Original = elementA });
            proxyListRef = proxyList;

            // Column: sideA name
            builder = getColumnDescriptorBuilderForTable<TProxy>(tableCDGV);
            builder.Header("");
            builder.Width(150);
            builder.DividerWidth(3);
            builder.InitializerMethod((elementProxyA, cell) => { cell.Value = elementNameGetter(elementProxyA.Original); });
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

        private void doAssociation<TProxy, TElement>(TProxy elementProxyA, TElement elementB, ObservableProxyEnumerable<TProxy, TElement> otherProxies)
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
        private ObservableProxyEnumerable<RouterInputProxy, RouterInput> routerInputProxies;
        private ObservableProxyEnumerable<RouterOutputProxy, RouterOutput> routerOutputProxies;

        private abstract class RouterIOProxy<TElement> : ObjectProxy<TElement>
            where TElement: class, INotifyPropertyChanged
        {

            public RouterIOProxy() { }

            private TElement associatedElement = null;
            public TElement AssociatedElement
            {
                get => associatedElement;
                set => this.setProperty(ref associatedElement, value, (i, ov, nv) => { }, null, (ov, nv) =>
                {
                    raisePropertyChangedAssociatedElement(ov);
                    raisePropertyChangedAssociatedElement(nv);
                });
            }

            private void raisePropertyChangedAssociatedElement(TElement element)
            {
                if (element != null)
                    RaisePropertyChanged("#" + getElementIndex(element));
            }

            protected abstract int getElementIndex(TElement element);

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
            => set11Associations<RouterInput, RouterInputProxy>(routerInputProxies, routerB.Inputs);

        private void set11OutputAssociationsButton_Click(object sender, EventArgs e)
            => set11Associations<RouterOutput, RouterOutputProxy>(routerOutputProxies, routerB.Outputs);
        #endregion

        private Router routerA => routerAdropDown.SelectedValue as Router;
        private Router routerB => routerBdropDown.SelectedValue as Router;

    }

}
