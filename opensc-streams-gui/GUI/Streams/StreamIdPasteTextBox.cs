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
    public class StreamIdPasteTextBox : TextBox
    {

        private const int WM_PASTE = 0x0302;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                string clipboardText = Clipboard.GetText();
                if (PastedTextConverter != null)
                {
                    try
                    {
                        string convertedText = PastedTextConverter(clipboardText);
                        Text = convertedText;
                    }
                    catch (PastedTextConversionException ex)
                    {
                        MessageBox.Show(ex.Message, "Pasted text parse error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Text = clipboardText;
                    }
                }
                else
                {
                    Text = clipboardText;
                }
                Select(Text.Length, 0);
                return;
            }
            base.WndProc(ref m);
        }

        public delegate string PastedTextConverterDelegate(string clipboardText);
        public PastedTextConverterDelegate PastedTextConverter;

        public class PastedTextConversionException : Exception
        {
            public PastedTextConversionException(string message) : base(message) { }
        }

    }
}
