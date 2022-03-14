using OpenSC.GUI.GeneralComponents.DropDowns;
using OpenSC.Model;
using OpenSC.Model.SerialPorts;
using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{

    public partial class McCurdyUmd1EditorForm : UmdEditorFormBase, IModelEditorForm<UMD>
    {

        public IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as UMD);
        public IModelEditorForm<UMD> GetInstanceT(UMD modelInstance) => new McCurdyUmd1EditorForm(modelInstance);

        public McCurdyUmd1EditorForm() : base() => InitializeComponent();

        public McCurdyUmd1EditorForm(UMD umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is McCurdyUMD1))
                throw new ArgumentException($"Type of UMD should be {nameof(McCurdyUMD1)}.", nameof(umd));
            fillControlArrays();
            initDropDowns();
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<UMD, McCurdyUMD1>(this, UmdDatabase.Instance);

        private const int MAX_COLUMN_COUNT = 3;

        private ComboBox[] columnDynamicTextSourceDropDowns = new ComboBox[MAX_COLUMN_COUNT];
        private ComboBox[] columnAlignmentDropDowns = new ComboBox[MAX_COLUMN_COUNT];
        private NumericUpDown[] columnWidthNumericFields = new NumericUpDown[MAX_COLUMN_COUNT];
        private Label[] columnWidthLabels = new Label[MAX_COLUMN_COUNT];
        private Label[] columnDynamicTextLabels = new Label[MAX_COLUMN_COUNT];
        private Panel[] columnDynamicDataPanels = new Panel[MAX_COLUMN_COUNT];
        private RadioButton[] columnCountRadioButtons = new RadioButton[MAX_COLUMN_COUNT];

        protected override void loadData()
        {
            base.loadData();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            portDropDown.SelectByValue(mcCurdyUmd1.Port);
            addressNumericField.Value = mcCurdyUmd1.Address;
            columnCount = convertColumnCountEnumToInt(mcCurdyUmd1.ColumnCount);
            useSeparatorBarCheckBox.Checked = mcCurdyUmd1.UseSeparators;
            for (int i = 0; i < MAX_COLUMN_COUNT; i++)
            {
                columnDynamicTextSourceDropDowns[i].SelectByValue(mcCurdyUmd1.GetDynamicTextSource(i));
                columnAlignmentDropDowns[i].SelectByValue(mcCurdyUmd1.TextAlignment[i]);
                if(i < MAX_COLUMN_COUNT - 1)
                    columnWidthNumericFields[i].Value = mcCurdyUmd1.ColumnWidths[i];
            }
            updateColumnWidths();
        }

        protected override void writeFields()
        {
            base.writeFields();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
            mcCurdyUmd1.Port = portDropDown.SelectedValue as SerialPort;
            mcCurdyUmd1.Address = (int)addressNumericField.Value;
            mcCurdyUmd1.ColumnCount = convertIntToColumnCountEnum(columnCount);
            mcCurdyUmd1.UseSeparators = useSeparatorBarCheckBox.Checked;
            TextAlignment[] alignments = new TextAlignment[MAX_COLUMN_COUNT];
            int[] widths = new int[MAX_COLUMN_COUNT - 1];
            for (int i = 0; i < 3; i++)
            {
                mcCurdyUmd1.SetDynamicTextSource(i, columnDynamicTextSourceDropDowns[i].SelectedValue as DynamicText);
                alignments[i] = (TextAlignment)columnAlignmentDropDowns[i].SelectedValue;
                if (i < MAX_COLUMN_COUNT - 1)
                    widths[i] = (int)columnWidthNumericFields[i].Value;
            }
            mcCurdyUmd1.TextAlignment = alignments;
            mcCurdyUmd1.ColumnWidths = widths;
        }

        protected override void validateFields()
        {
            base.validateFields();
            McCurdyUMD1 mcCurdyUmd1 = (McCurdyUMD1)EditedModel;
            if (mcCurdyUmd1 == null)
                return;
        }

        private int _columnCount;

        private int columnCount
        {
            get => _columnCount;
            set
            {
                if (value == _columnCount)
                    return;
                _columnCount = value;
                for (int i = 0; i < MAX_COLUMN_COUNT; i++)
                {
                    columnWidthNumericFields[i].Visible
                        = columnWidthLabels[i].Visible
                        = columnDynamicDataPanels[i].Visible
                        = columnDynamicTextLabels[i].Visible
                        = (i < value);
                    columnWidthNumericFields[i].Enabled = (i < (value - 1));
                    columnCountRadioButtons[i].Checked = (value == (i + 1));
                }
                updateColumnWidths();
                useSeparatorBarLabel.Visible = useSeparatorBarCheckBox.Visible = (value > 1);
            }
        }

        private void columnCountRadioButtonCheckedStateChange(object sender, EventArgs e)
        {
            RadioButton typedSender = sender as RadioButton;
            if(typedSender.Checked)
                columnCount = int.Parse(typedSender.Tag?.ToString());
        }

        private int convertColumnCountEnumToInt(ColumnCount columnCount)
            => columnCount switch
            {
                ColumnCount.One => 1,
                ColumnCount.Two => 2,
                ColumnCount.Three => 3,
                _ => throw new ArgumentException("Column count must be between 1 and 3!", nameof(columnCount))
            };

        private ColumnCount convertIntToColumnCountEnum(int columnCount)
            => columnCount switch
            {
                1 => ColumnCount.One,
                2 => ColumnCount.Two,
                3 => ColumnCount.Three,
                _ => throw new ArgumentException("Column count must be between 1 and 3!", nameof(columnCount))
            };


        private void initDropDowns()
        {
            // Ports
            portDropDown.CreateAdapterAsDataSource(SerialPortDatabase.Instance, port => port.Name, true, "(not connected)");
            portDropDown.ReceiveSystemObjectDrop().FilterByType<SerialPort>();
            // Dynamic text sources
            IComboBoxAdapterFactory dynamicTextAdapterFactory = new ComboBoxAdapterFactory<DynamicText>(DynamicTextDatabase.Instance, dt => dt.Name, true, "(empty)");
            for (int i = 0; i < MAX_COLUMN_COUNT; i++)
                columnDynamicTextSourceDropDowns[i].GetAdapterFromFactoryAsDataSource(dynamicTextAdapterFactory);
            // Dynamic text alignments
            Dictionary<TextAlignment, string> textAlignmentTranslations = new Dictionary<TextAlignment, string>()
            {
                { TextAlignment.Left, "left" },
                { TextAlignment.Center, "center" },
                { TextAlignment.Right, "right" }
            };
            IComboBoxAdapterFactory textAlignmentsAdapterFactory = new EnumComboBoxAdapterFactory<TextAlignment>(textAlignmentTranslations);
            for (int i = 0; i < MAX_COLUMN_COUNT; i++)
                columnAlignmentDropDowns[i].GetAdapterFromFactoryAsDataSource(textAlignmentsAdapterFactory);
        }

        private void fillControlArrays()
        {

            columnDynamicTextSourceDropDowns[0] = column1DynamicTextSourceDropDown;
            columnDynamicTextSourceDropDowns[1] = column2DynamicTextSourceDropDown;
            columnDynamicTextSourceDropDowns[2] = column3DynamicTextSourceDropDown;

            columnAlignmentDropDowns[0] = column1AlignmentDropDown;
            columnAlignmentDropDowns[1] = column2AlignmentDropDown;
            columnAlignmentDropDowns[2] = column3AlignmentDropDown;

            columnWidthNumericFields[0] = column1WidthNumericField;
            columnWidthNumericFields[1] = column2WidthNumericField;
            columnWidthNumericFields[2] = column3WidthNumericField;

            columnWidthLabels[0] = column1WidthLabel;
            columnWidthLabels[1] = column2WidthLabel;
            columnWidthLabels[2] = column3WidthLabel;

            columnDynamicTextLabels[0] = column1DynamicTextLabel;
            columnDynamicTextLabels[1] = column2DynamicTextLabel;
            columnDynamicTextLabels[2] = column3DynamicTextLabel;

            columnDynamicDataPanels[0] = column1DynamicDataPanel;
            columnDynamicDataPanels[1] = column2DynamicDataPanel;
            columnDynamicDataPanels[2] = column3DynamicDataPanel;

            columnCountRadioButtons[0] = columnCountOneRadioButton;
            columnCountRadioButtons[1] = columnCountTwoRadioButton;
            columnCountRadioButtons[2] = columnCountThreeRadioButton;

        }

        private void columnWidthChangedHandler(object sender, EventArgs e)
        {
            if (sender != columnWidthNumericFields[columnCount - 1])
                updateColumnWidths();
        }

        private void updateColumnWidths()
        {
            int totalColumnWidth = ((McCurdyUMD1)EditedModel).TotalWidth;
            if (useSeparatorBarCheckBox.Checked)
                totalColumnWidth -= 11 * (columnCount - 1);
            for (int i = 0; i < columnCount - 1; i++)
                totalColumnWidth -= (int)columnWidthNumericFields[i].Value;
            if (totalColumnWidth < 0)
                totalColumnWidth = 0;
            columnWidthNumericFields[columnCount - 1].Value = totalColumnWidth;
        }

        private void useSeparatorBarCheckBox_CheckedChanged(object sender, EventArgs e) => updateColumnWidths();

    }

}
