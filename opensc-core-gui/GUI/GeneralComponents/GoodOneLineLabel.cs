using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents
{
    public class GoodOneLineLabel : Label
    {
        // @source https://stackoverflow.com/a/68867239
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle drawRectangle = new Rectangle(ClientRectangle.Location, ClientRectangle.Size);
            drawRectangle.X += Padding.Left;
            drawRectangle.Width -= Padding.Left + Padding.Right;
            drawRectangle.Y += Padding.Top;
            drawRectangle.Height -= Padding.Top + Padding.Bottom;
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis;
            flags |= TextAlign switch
            {
                ContentAlignment.TopLeft => TextFormatFlags.Top | TextFormatFlags.Left,
                ContentAlignment.TopCenter => TextFormatFlags.Top | TextFormatFlags.HorizontalCenter,
                ContentAlignment.TopRight => TextFormatFlags.Top | TextFormatFlags.Right,
                ContentAlignment.MiddleLeft => TextFormatFlags.VerticalCenter | TextFormatFlags.Left,
                ContentAlignment.MiddleCenter => TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                ContentAlignment.MiddleRight => TextFormatFlags.VerticalCenter | TextFormatFlags.Right,
                ContentAlignment.BottomLeft => TextFormatFlags.Bottom | TextFormatFlags.Left,
                ContentAlignment.BottomCenter => TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter,
                ContentAlignment.BottomRight => TextFormatFlags.Bottom | TextFormatFlags.Right,
                _ => TextFormatFlags.Top | TextFormatFlags.Left,
            };
            TextRenderer.DrawText(e.Graphics, Text, Font, drawRectangle, ForeColor, flags);
        }
    }
}
