﻿using OpenSC.Model.UMDs;
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

        public new IModelEditorForm GetInstance(object modelInstance) => GetInstanceT(modelInstance as UMD);
        public new IModelEditorForm<UMD> GetInstanceT(UMD modelInstance) => new McCurdyUmd1TEditorForm(modelInstance);

        public McCurdyUmd1TEditorForm() : base() => InitializeComponent();

        public McCurdyUmd1TEditorForm(UMD umd) : base(umd)
        {
            InitializeComponent();
            if ((umd != null) && !(umd is McCurdyUMD1T))
                throw new ArgumentException($"Type of UMD should be {nameof(McCurdyUMD1T)}.", nameof(umd));
        }

        protected override IModelEditorFormDataManager createManager()
            => new ModelEditorFormDataManager<UMD, McCurdyUMD1T>(this, UmdDatabase.Instance);

    }

}