using OpenSC.GUI.GeneralComponents;
using OpenSC.GUI.GeneralComponents.Tables;
using OpenSC.GUI.Helpers.Converters;
using OpenSC.GUI.WorkspaceManager;
using OpenSC.Model;
using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpenSC.GUI
{

    public partial class ModelListFormBase : ItemListFormBase
    {

        protected virtual IModelTypeRegister TypeRegister { get; } = null;
        protected virtual IModelEditorFormTypeRegister EditorFormTypeRegister { get; } = null;
        protected virtual IModelEditorForm ModelEditorForm { get; } = null;

        public ModelListFormBase()
        {
            InitializeComponent();
            setTexts();
            if ((Manager != null) && !(Manager is IModelListFormBaseManager))
                throw new Exception();
            initAddItemButton();
        }

        private void initAddItemButton()
        {
            if ((TypeRegister != null) && (EditorFormTypeRegister != null))
            {
                loadAddableSutypes();
            }
            else if (ModelEditorForm != null)
            {
                addItemButton.SplitWidth = 0;
                addItemButton.Click += addItemClick;
                return;
            }
        }

        private void loadAddableSutypes()
        {
            foreach (Type type in TypeRegister.RegisteredTypes)
            {
                string label = type.GetTypeLabel();
                ToolStripMenuItem contextMenuItem = new ToolStripMenuItem(label)
                {
                    Tag = type
                };
                contextMenuItem.Click += addSubtypeItemClick;
                addableItemTypesMenu.Items.Add(contextMenuItem);
            }
        }

        private void addItemClick(object sender, EventArgs e)
        {
            IModelEditorForm editorForm = ModelEditorForm?.GetInstance(null);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }

        private void addSubtypeItemClick(object sender, EventArgs e)
        {
            Type newItemType = (sender as ToolStripMenuItem)?.Tag as Type;
            IModelEditorForm editorForm = EditorFormTypeRegister.GetFormForType(newItemType);
            (editorForm as ChildWindowBase)?.ShowAsChild();
        }

        protected const string COLUMN_ID_ID = "base@id";

        protected CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> idColumnCreator<TModelBasetype>(CustomDataGridView<TModelBasetype> table, ItemListFormBaseManager<TModelBasetype>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
            where TModelBasetype : class, IModel, INotifyPropertyChanged
        {
            CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.ID(COLUMN_ID_ID);
            builder.Header("ID");
            builder.Width(30);
            builder.UpdaterMethod((item, cell) => { cell.Value = string.Format("#{0}", item.ID); });
            builder.AddChangeEvent(nameof(IModel.ID));
            builder.AllowSystemObjectDrag();
            return builder;
        }

        protected const string COLUMN_ID_NAME = "base@name";

        protected CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> nameColumnCreator<TModelBasetype>(CustomDataGridView<TModelBasetype> table, ItemListFormBaseManager<TModelBasetype>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
            where TModelBasetype : class, IModel, INotifyPropertyChanged
        {
            CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.TextBox);
            builder.ID(COLUMN_ID_NAME);
            builder.Header("Name");
            builder.Width(150);
            builder.CellStyle(BOLD_TEXT_CELL_STYLE);
            builder.UpdaterMethod((item, cell) => { cell.Value = item.Name; });
            builder.AddChangeEvent(nameof(IModel.Name));
            builder.AllowSystemObjectDrag();
            return builder;
        }

        protected const string COLUMN_ID_EDIT = "base@edit";

        protected CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> editButtonColumnCreator<TModelBasetype>(CustomDataGridView<TModelBasetype> table, ItemListFormBaseManager<TModelBasetype>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
            where TModelBasetype : class, IModel, INotifyPropertyChanged
        {
            CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.ID(COLUMN_ID_EDIT);
            builder.Header("Edit");
            builder.Width(70);
            builder.ButtonText("Edit");
            builder.CellContentClickHandlerMethod((item, cell, e) => {
                ChildWindowBase editWindow = null;
                if (EditorFormTypeRegister != null)
                {
                    ModelEditorFormTypeRegister<TModelBasetype> typeRegisterCasted = EditorFormTypeRegister as ModelEditorFormTypeRegister<TModelBasetype>;
                    editWindow = typeRegisterCasted?.GetFormForModel(item) as ChildWindowBase;
                }
                else if (ModelEditorForm != null)
                {
                    editWindow = ModelEditorForm.GetInstance(item) as ChildWindowBase;
                }
                editWindow?.ShowAsChild();
            });
            return builder;
        }

        protected const string COLUMN_ID_DELETE = "base@delete";

        protected CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> deleteButtonColumnCreator<TModelBasetype>(CustomDataGridView<TModelBasetype> table, ItemListFormBaseManager<TModelBasetype>.ColumnDescriptorBuilderGetterDelegate builderGetterMethod)
            where TModelBasetype : class, IModel, INotifyPropertyChanged
        {
            CustomDataGridViewColumnDescriptorBuilder<TModelBasetype> builder = builderGetterMethod();
            builder.Type(DataGridViewColumnType.Button);
            builder.ID(COLUMN_ID_DELETE);
            builder.Header("Delete");
            builder.Width(70);
            builder.DividerWidth(DEFAULT_DIVIDER_WIDTH);
            builder.ButtonText("Delete");
            builder.CellContentClickHandlerMethod((item, cell, e) => {
                string msgBoxText = string.Format("Do you really want to delete this {0}?\n{1}", SubjectSingular, item);
                var confirm = MessageBox.Show(msgBoxText, "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                    ((IModelListFormBaseManager)Manager).DeleteItem(item);
            });
            return builder;
        }

        protected override void setTexts()
        {
            base.setTexts();
            addItemButton.Text = string.Format("Add new {0}", SubjectSingular);
        }

    }

}