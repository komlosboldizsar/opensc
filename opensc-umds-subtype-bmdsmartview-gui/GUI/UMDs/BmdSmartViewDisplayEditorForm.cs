using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.GUI.Helpers;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.BmdSmartView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class BmdSmartViewDisplayEditorForm : UmdEditorFormBase, IModelEditorForm<Umd>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as Umd);
        public IModelEditorForm<Umd> GetInstanceT(Umd modelInstance) => new BmdSmartViewDisplayEditorForm(modelInstance);

        public BmdSmartViewDisplayEditorForm() : base() => InitializeComponent();

        public BmdSmartViewDisplayEditorForm(Umd umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is BmdSmartViewDisplay))
                throw new ArgumentException($"Type of UMD should be {nameof(BmdSmartViewDisplay)}.", nameof(umd));
            initTallyPriorityControls();
            initUnitDropDown();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<Umd, BmdSmartViewDisplay>(this, UmdDatabase.Instance);

        protected override void loadData()
        {
            base.loadData();
            BmdSmartViewDisplay bmdSmartViewDisplay = (BmdSmartViewDisplay)EditedModel;
            if (bmdSmartViewDisplay == null)
                return;
            unitDropDown.SelectByValue(bmdSmartViewDisplay.Unit);
            setPositionToRadioButtons(bmdSmartViewDisplay.Position);
            tallyPriorityControls.LoadData();
        }

        protected override void writeFields()
        {
            base.writeFields();
            BmdSmartViewDisplay bmdSmartViewDisplay = (BmdSmartViewDisplay)EditedModel;
            if (bmdSmartViewDisplay == null)
                return;
            bmdSmartViewDisplay.Unit = unitDropDown.SelectedValue as BmdSmartViewUnit;
            bmdSmartViewDisplay.Position = getPositionFromRadioButtons();
            tallyPriorityControls.WriteFields();
        }

        protected override void validateFields()
        {
            base.validateFields();
            BmdSmartViewDisplay bmdSmartViewDisplay = (BmdSmartViewDisplay)EditedModel;
            if (bmdSmartViewDisplay == null)
                return;
        }

        private void initUnitDropDown()
        {
            unitDropDown.CreateAdapterAsDataSource(BmdSmartViewUnitDatabase.Instance, null, true, "(not associated)");
            unitDropDown.ReceiveSystemObjectDrop().FilterByType<BmdSmartViewDisplay>();
        }

        private void setPositionToRadioButtons(BmdSmartViewDisplayPosition position)
        {
            positionSingleRadioButton.Checked = (position == BmdSmartViewDisplayPosition.Single);
            positionDualLeftRadioButton.Checked = (position == BmdSmartViewDisplayPosition.DualLeft);
            positionDualRightRadioButton.Checked = (position == BmdSmartViewDisplayPosition.DualRight);
        }

        private BmdSmartViewDisplayPosition getPositionFromRadioButtons()
        {
            if (positionSingleRadioButton.Checked)
                return BmdSmartViewDisplayPosition.Single;
            if (positionDualLeftRadioButton.Checked)
                return BmdSmartViewDisplayPosition.DualLeft;
            if (positionDualRightRadioButton.Checked)
                return BmdSmartViewDisplayPosition.DualRight;
            return BmdSmartViewDisplayPosition.Single;
        }

        #region Tally priority controls
        private TallyPriorityControls tallyPriorityControls;

        private void initTallyPriorityControls()
        {
            tallyPriorityControls = new(this);
            tallyPriorityControls.Init(((BmdSmartViewDisplay)EditedModel).TallyInfo);
            tallyPriorityControls.Assign(((BmdSmartViewDisplay)EditedModel).Tallies);
        }

        private class TallyPriorityControls
        {

            private BmdSmartViewDisplayEditorForm parentForm;
            private ControlCollection[] controlCollections = null;

            public TallyPriorityControls(BmdSmartViewDisplayEditorForm parentForm) => this.parentForm = parentForm;

            public void Init(UmdTallyInfo[] tallyInfo)
            {
                TableLayoutPanel talliesPriorityTable = parentForm.talliesPriorityTable;
                int tallyInfoLength = tallyInfo.Length;
                controlCollections = new ControlCollection[tallyInfoLength];
                TableLayoutHelpers.ColumnCloner talliesPriorityTableColumnCloner = new(talliesPriorityTable, 1);
                for (int i = 0; i < tallyInfoLength; i++)
                {
                    if (i > 0)
                        talliesPriorityTableColumnCloner.DoCloning(-2, TableLayoutHelpers.ColumnCloner.EXCLUDE_VISIBILITY);
                    ((Button)talliesPriorityTable.GetControlFromPosition(i + 1, 0)).Text = (i + 1).ToString();
                }
                TableLayoutHelpers.RowCloner talliesPriorityTableRowCloner = new(talliesPriorityTable, 0);
                for (int i = 0; i < tallyInfoLength; i++)
                {
                    if (i > 0)
                        talliesPriorityTableRowCloner.DoCloning(-1, TableLayoutHelpers.ColumnCloner.EXCLUDE_VISIBILITY);
                    Button[] tallyPriorityValueButtons = new Button[tallyInfoLength];
                    for (int j = 0; j < tallyInfoLength; j++)
                        tallyPriorityValueButtons[j] = (Button)talliesPriorityTable.GetControlFromPosition(j + 1, i);
                    ControlCollection thisTallyPriorityControls = new(this, (BmdSmartViewDisplayTallyInfo)tallyInfo[i],
                        (Label)talliesPriorityTable.GetControlFromPosition(0, i),
                        tallyPriorityValueButtons);
                    controlCollections[i] = thisTallyPriorityControls;
                }
            }

            public void Assign(IEnumerable<UmdTally> tallies)
            {
                IEnumerator<UmdTally> talliesEnumerator = tallies.GetEnumerator();
                foreach (ControlCollection controlCollection in controlCollections)
                {
                    talliesEnumerator.MoveNext();
                    controlCollection.Assign((BmdSmartViewDisplayTally)talliesEnumerator.Current);
                }
            }

            public void LoadData()
            {
                foreach (ControlCollection controlCollection in controlCollections)
                    controlCollection.LoadData();
            }

            public void WriteFields()
            {
                foreach (ControlCollection controlCollection in controlCollections)
                    controlCollection.WriteFields();
            }

            private void notifyPriorityChanged(ControlCollection source, int oldValue, int newValue)
            {
                foreach (ControlCollection controlCollection in controlCollections)
                    if ((source != controlCollection) && (controlCollection.Priority == newValue))
                        controlCollection.Priority = oldValue;
            }

            public class ControlCollection
            {

                private readonly TallyPriorityControls parent;
                private readonly BmdSmartViewDisplayTallyInfo info;
                private readonly Label nameLabel;
                private readonly Button[] valueButtons;

                public ControlCollection(TallyPriorityControls parent, BmdSmartViewDisplayTallyInfo info, Label nameLabel, Button[] valueButtons)
                {
                    this.parent = parent;
                    this.info = info;
                    this.nameLabel = nameLabel;
                    this.valueButtons = valueButtons;
                    assignTags();
                    loadInfo();
                    bindEvents();
                }

                private void assignTags()
                {
                    nameLabel.Tag = this;
                    foreach (Button valueButton in valueButtons)
                        valueButton.Tag = this;
                }

                private void loadInfo()
                {
                    nameLabel.Text = info.Name;
                }

                private void bindEvents()
                {
                    foreach (Button valueButton in valueButtons)
                        valueButton.Click += ValueButtons_Click;
                }

                private void ValueButtons_Click(object sender, EventArgs e)
                {
                    int newPriority = -1;
                    int i = 0;
                    foreach (Button valueButtons in valueButtons)
                    {
                        if (valueButtons == sender)
                        {
                            newPriority = i;
                            break;
                        }
                        i++;
                    }
                    Priority = newPriority;
                }

                private static readonly Color VALUE_BUTTON_BG_ACTIVE = Color.DarkBlue;
                private static readonly Color VALUE_BUTTON_FG_ACTIVE = Color.White;
                private static readonly Color VALUE_BUTTON_BG_INACTIVE = SystemColors.Control;
                private static readonly Color VALUE_BUTTON_FG_INACTIVE = SystemColors.ControlText;

                int priority = -1;
                bool notifyingParentAboutPriorityChange = false;

                public int Priority
                {
                    get => priority;
                    set
                    {
                        if (notifyingParentAboutPriorityChange)
                            return;
                        if (value == priority)
                            return;
                        notifyingParentAboutPriorityChange = true;
                        parent.notifyPriorityChanged(this, priority, value);
                        notifyingParentAboutPriorityChange = false;
                        priority = value;
                        updateButttons();
                    }
                }

                private void updateButttons()
                {
                    for (int i = 0; i < valueButtons.Length; i++)
                    {
                        if (i == priority)
                        {
                            valueButtons[i].BackColor = VALUE_BUTTON_BG_ACTIVE;
                            valueButtons[i].ForeColor = VALUE_BUTTON_FG_ACTIVE;
                        }
                        else
                        {
                            valueButtons[i].BackColor = VALUE_BUTTON_BG_INACTIVE;
                            valueButtons[i].ForeColor = VALUE_BUTTON_FG_INACTIVE;
                        }
                    }
                }

                public BmdSmartViewDisplayTally Tally { get; private set; }

                public void Assign(BmdSmartViewDisplayTally tally) => Tally = tally;

                public void LoadData()
                {
                    if (Tally == null)
                        return;
                    Priority = Tally.Priority;
                }

                public void WriteFields()
                {
                    if (Tally == null)
                        return;
                    Tally.Priority = Priority;
                }

            }
            #endregion

        }

    }

}
