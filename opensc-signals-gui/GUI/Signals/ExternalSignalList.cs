using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Signals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Signals
{

    [WindowTypeName("signals.externalsignallist")]
    public partial class ExternalSignalList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "external signal";
        protected override string SubjectPlural { get; } = "external signals";

        protected override IModelEditorForm ModelEditorForm { get; } = new ExternalSignalEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<ExternalSignal>(this, ExternalSignalDatabases.Signals, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<ExternalSignal> table, ItemListFormBaseManager<ExternalSignal>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<ExternalSignal> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: category
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Category");
            builder.Width(150);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((signal, cell) => {
                cell.Value = (signal.Category != null) ? string.Format("(#{0}) {1}", signal.Category.ID, signal.Category.Name) : "(not associated)";
                cell.Style.BackColor = (signal.Category != null) ? signal.Category.Color : table.DefaultCellStyle.BackColor;
            });
            builder.ExternalUpdateEventSubscriberMethod((signal, updateInvoker) =>
            {
                PropertyChangedTwoValuesDelegate<IModel, int> signalCategoryIdChangedHandler = (_category, oldCategory, newCategory) => updateInvoker();
                PropertyChangedTwoValuesDelegate<IModel, string> signalCategoryNameChangedHandler = (_category, oldName, newName) => updateInvoker();
                PropertyChangedTwoValuesDelegate<ExternalSignalCategory, Color> signalCategoryColorChangedHandler = (_category, oldColor, newColor) => updateInvoker();
                if (signal.Category != null)
                {
                    signal.Category.IdChanged += signalCategoryIdChangedHandler;
                    signal.Category.NameChanged += signalCategoryNameChangedHandler;
                    signal.Category.ColorChanged += signalCategoryColorChangedHandler;
                }
                signal.CategoryChanged += (_signal, oldCategory, newCategory) => {
                    if (oldCategory != null)
                    {
                        oldCategory.IdChanged -= signalCategoryIdChangedHandler;
                        oldCategory.NameChanged -= signalCategoryNameChangedHandler;
                        oldCategory.ColorChanged -= signalCategoryColorChangedHandler;
                    }
                    if (newCategory != null)
                    {
                        newCategory.IdChanged += signalCategoryIdChangedHandler;
                        newCategory.NameChanged += signalCategoryNameChangedHandler;
                        newCategory.ColorChanged += signalCategoryColorChangedHandler;
                    }
                };
            });
            builder.AddChangeEvent(nameof(ExternalSignal.Category));

            // Column: red tally
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("T(R)");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.RedTally.State ? Color.Red : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.RedTally), nameof(IBidirectionalSignalTally.State));

            // Column: yellow tally
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("T(Y)");
            builder.Width(50);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.YellowTally.State ? Color.Gold : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.YellowTally), nameof(IBidirectionalSignalTally.State));

            // Column: green tally
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("T(G)");
            builder.Width(50);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((signal, cell) => { cell.Style.BackColor = (signal.GreenTally.State ? Color.ForestGreen : Color.LightGray); });
            builder.AddMultilevelChangeEvent(nameof(ExternalSignal.GreenTally), nameof(IBidirectionalSignalTally.State));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}