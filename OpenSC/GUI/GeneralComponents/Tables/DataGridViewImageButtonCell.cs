using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents.Tables
{
    class DataGridViewImageButtonCell: DataGridViewDisableButtonCell
    {

        public Image Image { get; set; }
        public Padding ImagePadding { get; set; }

        public override object Clone()
        {
            DataGridViewImageButtonCell cell = (DataGridViewImageButtonCell)base.Clone();
            cell.Image = this.Image;
            cell.ImagePadding = this.ImagePadding;
            return cell;
        }

        private const float CELL_OWN_PADDING = 2;

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            if (Image != null)
            {

                float left = cellBounds.Left + ImagePadding.Left + CELL_OWN_PADDING;
                float top = cellBounds.Top + ImagePadding.Top + CELL_OWN_PADDING;

                float canvasWidth = cellBounds.Width - 2 * CELL_OWN_PADDING - ImagePadding.Left - ImagePadding.Right;
                float canvasHeight = cellBounds.Height - 2 * CELL_OWN_PADDING - ImagePadding.Top - ImagePadding.Bottom;

                float drawWidth = canvasWidth;
                float drawHeight = canvasHeight;

                float imageAR = Image.Width / Image.Height;
                float canvasAR = canvasWidth / canvasHeight;

                if (imageAR > canvasAR) // Image is wider than the canvas
                {
                    drawHeight = Image.Height * (canvasWidth /Image.Width);
                    top += (canvasHeight - drawHeight) / 2;
                }
                else
                {
                    drawWidth = (Image.Width * (canvasHeight / Image.Height));
                    left += (canvasWidth - drawWidth) / 2;
                }
               
                graphics.DrawImage(Image, left, top, drawWidth, drawHeight);

            }
        }
    }
}
