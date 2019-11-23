using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Macros
{
    public class MacroPanelElement
    {

        public Macro Macro { get; set; }

        // "Temp foreign key"
        private int _macroId;

        public string Label { get; set; }

        public bool ShowLabel { get; set; }

        public Color BackColor { get; set; }

        public Color ForeColor { get; set; }

        public int PositionX { get; set; }
        
        public int PositionY { get; set; }

        public int SizeW { get; set; }

        public int SizeH { get; set; }

        public MacroPanelElement()
        { }

        public MacroPanelElement(int macroId)
        {
            this._macroId = macroId;
        }

        public void Restored()
        {
            Macro = MacroDatabase.Instance.GetTById(_macroId);
        }

    }
}
