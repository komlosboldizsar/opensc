using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model.Macros;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Macros
{

    [WindowTypeName("macros.macropanelslist")]
    public partial class MacroPanelList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "macro panel";
        protected override string SubjectPlural { get; } = "macro panels";

        protected override IModelEditorForm ModelEditorForm { get; } = new MacroPanelEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<MacroPanel>(this, MacroPanelDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<MacroPanel> table, ItemListFormBaseManager<MacroPanel>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<MacroPanel> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: element count
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Elements");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((macroPanel, cell) => { cell.Value = macroPanel.Elements.Count; });
            // TODO: builder.AddChangeEvent(nameof(MacroPanel.Elements));

            // Column: open button
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Open");
            builder.Width(70);
            builder.ButtonText("Open");
            builder.CellContentClickHandlerMethod((macroPanel, cell, e) => {
                var window = new MacroPanelForm(macroPanel);
                window.ShowAsChild();
            });

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}