using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{

    // @source https://www.codeproject.com/Tips/178587/Draggable-WinForms-Controls-2
    public static class DraggableControls
    {

        private static Dictionary<Control, bool> draggables = new Dictionary<Control, bool>();

        private static Size mouseOffset;

        private static Cursor originalCursor;
        public static void Draggable(this Control control, bool Enable)
        {
            if (Enable)
            {
                if (draggables.ContainsKey(control))
                    return;
                draggables.Add(control, false);
                control.MouseDown += new MouseEventHandler(control_MouseDown);
                control.MouseUp += new MouseEventHandler(control_MouseUp);
                control.MouseMove += new MouseEventHandler(control_MouseMove);
            }
            else
            {
                if (!draggables.ContainsKey(control))
                    return;
                control.MouseDown -= control_MouseDown;
                control.MouseUp -= control_MouseUp;
                control.MouseMove -= control_MouseMove;
                draggables.Remove(control);
            }
        }
        static void control_MouseDown(object sender, MouseEventArgs e)
        {
            mouseOffset = new Size(e.Location);
            Control typedSender = (Control)sender;
            draggables[typedSender] = true;
            originalCursor = typedSender.Cursor;
            typedSender.Cursor = Cursors.SizeAll;
        }
        static void control_MouseUp(object sender, MouseEventArgs e)
        {
            Control typedSender = (Control)sender;
            draggables[typedSender] = false;
            typedSender.Cursor = originalCursor;
        }
        static void control_MouseMove(object sender, MouseEventArgs e)
        {
            Control typedSender = (Control)sender;
            if (draggables[typedSender] == true)
            {
                Point newLocationOffset = e.Location - mouseOffset;
                typedSender.Left += newLocationOffset.X;
                typedSender.Top += newLocationOffset.Y;
            }
        }

    }

}
