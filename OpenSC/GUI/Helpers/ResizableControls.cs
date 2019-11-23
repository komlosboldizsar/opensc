using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{

    // original by: @source https://www.codeproject.com/Tips/178587/Draggable-WinForms-Controls-2
    public static class ResizableControls
    {

        private static int RESIZE_START_ACCURACY = 2;

        private static Dictionary<Control, ResizingMode> resizables = new Dictionary<Control, ResizingMode>();

        private static System.Drawing.Size mouseOffset;

        private static bool mouseDown;
        public static void Resizable(this Control control, bool Enable)
        {
            if (Enable)
            {
                if (resizables.ContainsKey(control))
                    return;
                resizables.Add(control, ResizingMode.NoResizing);
                control.MouseDown += new MouseEventHandler(control_MouseDown);
                control.MouseUp += new MouseEventHandler(control_MouseUp);
                control.MouseMove += new MouseEventHandler(control_MouseMove);

            }
            else
            {
                if (!resizables.ContainsKey(control))
                    return;
                control.MouseDown -= control_MouseDown;
                control.MouseUp -= control_MouseUp;
                control.MouseMove -= control_MouseMove;
                resizables.Remove(control);
            }
        }
        static void control_MouseDown(object sender, MouseEventArgs e)
        {
            Control typedSender = (Control)sender;
            bool _atRight = atRight(typedSender, e);
            bool _atBottom = atBottom(typedSender, e);
            if (_atRight || _atBottom)
            mouseOffset = new System.Drawing.Size(e.Location);
            if (_atRight && _atBottom)
                resizables[typedSender] = ResizingMode.ResizingBoth;
            else if (_atRight)
                resizables[typedSender] = ResizingMode.ResizingWidth;
            else if (_atBottom)
                resizables[typedSender] = ResizingMode.ResizingHeight;
            else
                resizables[typedSender] = ResizingMode.NoResizing;
        }
        static void control_MouseUp(object sender, MouseEventArgs e)
        {
            resizables[(Control)sender] = ResizingMode.NoResizing;
        }
        static void control_MouseMove(object sender, MouseEventArgs e)
        {

            Control typedSender = (Control)sender;

            // Cursor
            bool _atRight = atRight(typedSender, e);
            bool _atBottom = atBottom(typedSender, e);
            if (_atRight && _atBottom)
                typedSender.Cursor = Cursors.SizeNWSE;
            else if (_atRight)
                typedSender.Cursor = Cursors.SizeWE;
            else if (_atBottom)
                typedSender.Cursor = Cursors.SizeNS;
            else
                typedSender.Cursor = Cursors.Default;

            // Resizing
            ResizingMode resizingMode = resizables[(Control)sender];
            if (resizingMode != ResizingMode.NoResizing)
            {
                System.Drawing.Point newLocationOffset = e.Location - mouseOffset;
                if ((resizingMode == ResizingMode.ResizingBoth) || (resizingMode == ResizingMode.ResizingWidth))
                    ((Control)sender).Width += newLocationOffset.X;
                if ((resizingMode == ResizingMode.ResizingBoth) || (resizingMode == ResizingMode.ResizingHeight))
                    ((Control)sender).Height += newLocationOffset.Y;
                mouseOffset = new System.Drawing.Size(e.Location);
            }
        }

        static bool atRight(Control control, MouseEventArgs e)
            => (Math.Abs(control.Width - 1 - e.Location.X) <= RESIZE_START_ACCURACY);

        static bool atBottom(Control control, MouseEventArgs e)
            => (Math.Abs(control.Height - 1 - e.Location.Y) <= RESIZE_START_ACCURACY);

        public enum ResizingMode
        {
            NoResizing,
            ResizingWidth,
            ResizingHeight,
            ResizingBoth
        }

    }

}
