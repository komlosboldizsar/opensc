namespace OpenSC.GUI
{

    public partial class ItemListFormBase : ChildWindowWithTable
    {

        protected virtual string SubjectSingular { get; } = "subject";
        protected virtual string SubjectPlural { get; } = "subjects";

        protected virtual IItemListFormBaseManager Manager { get; set; } = null;

        public ItemListFormBase()
        {
            InitializeComponent();
            Manager = createManager();
            if (Manager == null)
                return;
            Manager.InitializeTable();
        }

        protected virtual IItemListFormBaseManager createManager() => null;

        protected virtual void setTexts()
        {
            string headerText = string.Format("List of {0}", SubjectPlural);
            Text = headerText;
            HeaderText = headerText;
        }

        private void ItemListFormBase_Load(object sender, System.EventArgs e)
        {
            setTexts();
        }
    }

}