using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.Persistence;
using OpenSC.Model.GpioInterfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI.GpioInterfaces
{

    [WindowTypeName("gpiointerfaces.gpiointerfaceslist")]
    public partial class GpioInterfaceList : ModelListFormBase
    {

        protected override string SubjectSingular { get; } = "GPIO interface";
        protected override string SubjectPlural { get; } = "GPIO interfaces";

        protected override IModelTypeRegister TypeRegister { get; } = GpioInterfaceTypeRegister.Instance;
        protected override IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = GpioInterfaceEditorFormTypeRegister.Instance;

        protected override IItemListFormBaseManager createManager()
            => Manager = new ModelListFormBaseManager<GpioInterface>(this, GpioInterfaceDatabase.Instance, baseColumnCreator);

        private void baseColumnCreator(CustomDataGridView<GpioInterface> table, ItemListFormBaseManager<GpioInterface>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
        {

            CustomDataGridViewColumnDescriptorBuilder<GpioInterface> builder;

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
            builder.UpdaterMethod((gpioInterface, cell) => {
                cell.Style.BackColor = stateColorConverter.Convert(gpioInterface.State);
                cell.Value = gpioInterface.StateString;
            });
            builder.AddChangeEvent(nameof(GpioInterface.State));
            builder.AddChangeEvent(nameof(GpioInterface.StateString));

            // Column: inputs
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Inputs");
            builder.Width(50);
            builder.UpdaterMethod((gpioInterface, cell) => { cell.Value = gpioInterface.Inputs.Count; });
            builder.AddChangeEvent(nameof(GpioInterface.Inputs));

            // Column: outputs
            builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.Header("Outputs");
            builder.Width(50);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.UpdaterMethod((gpioInterface, cell) => { cell.Value = gpioInterface.Outputs.Count; });
            builder.AddChangeEvent(nameof(GpioInterface.Outputs));

            // Column: edit, delete
            editButtonColumnCreator(table, builderGetterMethod);
            deleteButtonColumnCreator(table, builderGetterMethod);

        }

        private static readonly EnumConverter<GpioInterfaceState, Color> stateColorConverter = new EnumConverter<GpioInterfaceState, Color>(null)
        {
            { GpioInterfaceState.Ok, Color.LightGreen },
            { GpioInterfaceState.Warning, Color.FromArgb(255, 255, 244, 104) },
            { GpioInterfaceState.Error, Color.LightPink },
            { GpioInterfaceState.Unknown, Color.LightSlateGray }
        };

    }

}