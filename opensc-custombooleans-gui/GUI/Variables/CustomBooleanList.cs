using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    [WindowTypeName("booleans.custombooleanlist")]
    public partial class CustomBooleanList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "custom boolean";
        protected override string SubjectPlural { get; } = "custom booleans";

        protected override IModelTypeRegister TypeRegister { get; } = CustomBooleanTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = CustomBooleanEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => Manager = new ModelListFormBaseManager<CustomBoolean>(this, CustomBooleanDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<CustomBoolean> table, ItemListFormBaseManager<CustomBoolean>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<CustomBoolean> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: identifier
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Identifier");
            builder.Width(70);
            builder.UpdaterMethod((customBoolean, cell) => { cell.Value = customBoolean.Identifier; });
            builder.AddChangeEvent(nameof(CustomBoolean.Identifier));

            // Column: identifier
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Description");
            builder.Width(120);
            builder.UpdaterMethod((customBoolean, cell) => { cell.Value = customBoolean.Description; });
            builder.AddChangeEvent(nameof(CustomBoolean.Description));

            // Column: color
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Color");
            builder.Width(40);
            builder.UpdaterMethod((customBoolean, cell) => { cell.Style.BackColor = customBoolean.Color; });
            builder.AddChangeEvent(nameof(CustomBoolean.Color));

            // Column: state
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((customBoolean, cell) => {
                cell.Style.BackColor = customBoolean.CurrentState ? Color.LightGreen : Color.LightPink;
                cell.Value = customBoolean.CurrentState ? "true" : "false";
            });
            builder.AddChangeEvent(nameof(CustomBoolean.CurrentState));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

    }

}