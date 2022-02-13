using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Routers;
using OpenSC.Model.Routers.Salvos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.Routers.Salvos
{

    [WindowTypeName("routers.salvolist")]
    public partial class SalvoList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "salvo";
        protected override string SubjectPlural { get; } = "salvos";

        protected override IModelEditorForm ModelEditorForm { get; } = new SalvoEditorForm();

        protected override IItemListFormBaseManager createManager()
            => new ModelListFormBaseManager<Salvo>(this, SalvoDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<Salvo> table, ItemListFormBaseManager<Salvo>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<Salvo> builder;

            // Column: ID, name
            idColumnCreator(table, builderGetterMethod);
            nameColumnCreator(table, builderGetterMethod);

            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.DisableButton);
            builder.Header("Take");
            builder.Width(70);
            builder.ButtonText("Take");
            builder.CellContentClickHandlerMethod((salvo, cell, e) => salvo.Take());

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        //private static readonly Color TEXT_COLOR_INVALID_CROSSPOINT = Color.Red;
        //private static readonly Color TEXT_COLOR_VALID_CROSSPOINT = Color.Black;

    }

}