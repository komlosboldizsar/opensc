using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenSC.Model.Macros;
using OpenSC.Model;

namespace OpenSC.GUI.Macros
{
    public partial class MacroPanelElementButton : UserControl
    {

        public MacroPanelElement Element { get; internal set; }

        private Macro macro;

        public Macro Macro
        {
            get => macro;
            set
            {
                if (macro != null)
                    macro.NameChanged -= Macro_NameChanged;
                macro = value;
                if (macro != null)
                    macro.NameChanged += Macro_NameChanged;
            }
        }

        private void Macro_NameChanged(IModel text, string oldName, string newName)
        {
            if (!ShowLabel)
                button.Text = newName;
        }

        private string label;

        public string Label
        {
            get => label;
            set
            {
                label = value;
                if (ShowLabel)
                    button.Text = value;
            }
        }

        private bool showLabel;
        public bool ShowLabel
        {
            get => showLabel;
            set
            {
                showLabel = value;
                button.Text = (value ? label : macro?.Name);
            }
        }
        public Color ElementForeColor
        {
            get => button.ForeColor;
            set
            {
                button.ForeColor = value;
            }
        }

        public Color ElementBackColor
        {
            get => button.BackColor;
            set
            {
                button.BackColor = value;
            }
        }

        public MacroPanelElementButton(MacroPanelElement element)
        {
            this.Element = element;
            InitializeComponent();
        }

        private void MacroPanelElementButton_Load(object sender, EventArgs e)
        {
            RestoreFromModel();
        }

        public void RestoreFromModel()
        {

            if (Element == null)
                return;
            Macro = Element.Macro;
            Label = Element.Label;
            ShowLabel = Element.ShowLabel;
            button.ForeColor = Element.ForeColor;
            button.BackColor = Element.BackColor;
            Location = new Point(Element.PositionX, Element.PositionY);
            Width = Element.SizeW;
            Height = Element.SizeH;
        }

        public void SaveToModel()
        {
            if (Element == null)
                return;
            Element.Macro = Macro;
            Element.ShowLabel = ShowLabel;
            Element.Label = Label;
            Element.ForeColor = button.ForeColor;
            Element.BackColor = button.BackColor;
            Element.PositionX = Location.X;
            Element.PositionY = Location.Y;
            Element.SizeW = Width;
            Element.SizeH = Height;
            Element.Updated();
        }

        public delegate void ButtonClickHandlerDelegate(MacroPanelElementButton sender);
        public event ButtonClickHandlerDelegate ButtonClick;

        public delegate void ButtonRightClickHandlerDelegate(MacroPanelElementButton sender);
        public event ButtonRightClickHandlerDelegate ButtonRightClick;

        public void RunMacro()
        {
            Macro?.Run();
        }

        private void button_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(this);
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
            => OnMouseDown(e);

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
                ButtonRightClick?.Invoke(this);
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
            => OnMouseMove(e);

    }
}
