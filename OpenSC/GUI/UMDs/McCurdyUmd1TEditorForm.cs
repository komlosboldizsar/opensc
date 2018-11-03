using OpenSC.Model.UMDs;
using OpenSC.Model.UMDs.McCurdy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.UMDs
{
    public partial class McCurdyUmd1TEditorForm : McCurdyUmd1EditorForm
    {

        public McCurdyUmd1TEditorForm() : base()
        {
            InitializeComponent();
        }

        public McCurdyUmd1TEditorForm(UMD umd) : base(umd)
        {

            InitializeComponent();

            if (umd == null)
                this.umd = new McCurdyUMD1T();
            else if (!(umd is McCurdyUMD1T))
                throw new ArgumentException();

        }

        public override IModelEditorForm<UMD> GetInstance(UMD modelInstance)
        {
            return new McCurdyUmd1TEditorForm(modelInstance);
        }

    }
}
