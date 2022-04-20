using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.Routers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers
{

    [WindowTypeName("routers.routerlist")]
    public partial class RouterList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "router";
        protected override string SubjectPlural { get; } = "routers";

        protected override IModelTypeRegister TypeRegister { get; } = RouterTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = RouterEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => Manager = new ModelListFormBaseManager<Router>(this, RouterDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Router> table, ItemListFormBaseManager<Router>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Router> builder;

            // Column: GlobalID, ID, name
            globalIdColumnCreator(table, builderGetterMethod);
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            // Column: state
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("State");
            builder.Width(100);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((router, cell) => {
                cell.Style.BackColor = stateColorConverter.Convert(router.State);
                cell.Value = router.StateString;
            });
            builder.AddChangeEvent(nameof(Router.State));
            builder.AddChangeEvent(nameof(Router.StateString));

            // Column: inputs
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Inputs");
            builder.Width(50);
            builder.UpdaterMethod((router, cell) => { cell.Value = router.Inputs.Count; });
            builder.AddChangeEvent(nameof(Router.Inputs));

            // Column: outputs
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Outputs");
            builder.Width(50);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((router, cell) => { cell.Value = router.Outputs.Count; });
            builder.AddChangeEvent(nameof(Router.Outputs));

            // Column: crosspoints
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.Header("Crosspoints");
            builder.Width(70);
            builder.ButtonText("Crosspoints");
            builder.CellContentClickHandlerMethod((router, cell, e) => {
                new RouterControlForm(router).ShowAsChild();
            });

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly EnumConverter<RouterState, Color> stateColorConverter = new EnumConverter<RouterState, Color>(null)
        {
            { RouterState.Ok, Color.LightGreen },
            { RouterState.Warning, Color.FromArgb(255, 255, 244, 104) },
            { RouterState.Error, Color.LightPink },
            { RouterState.Unknown, Color.LightSlateGray }
        };

    }

}