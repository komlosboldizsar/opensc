using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Streams
{

    // @source https://stackoverflow.com/a/15988692
    public class VideoIdPasteTextBox : TextBox
    {

        private const int WM_PASTE = 0x0302;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                Clipboard.GetText();
                return;
            }
            base.WndProc(ref m);
        }

    }
}
