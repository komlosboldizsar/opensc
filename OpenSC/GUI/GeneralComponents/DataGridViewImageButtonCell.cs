using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI
{
    class DataGridViewImageButtonCell: DataGridViewButtonCell
    {

        public Image Image { get; set; }
        public Padding ImagePadding { get; set; }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            if (Image != null)
            {

                int left = cellBounds.Left + ImagePadding.Left;
                int top = cellBounds.Top + ImagePadding.Top;

                int maxWidth = cellBounds.Width - ImagePadding.Left - ImagePadding.Right;
                int maxHeight = cellBounds.Height - ImagePadding.Top - ImagePadding.Bottom;

                int drawWidth = maxWidth;
                int drawHeight = maxHeight;

                float originalAR = Image.Width / (float)Image.Height;
                float spaceAR = maxWidth / (float)maxHeight;

                if (spaceAR > originalAR)
                {
                    drawWidth = (int)((originalAR / spaceAR) * maxWidth);
                    left += (maxWidth - drawWidth) / 2;
                }
                else
                {
                    drawHeight = (int)((originalAR / spaceAR) * maxHeight);
                    top += (maxHeight - drawHeight) / 2;
                }

                graphics.DrawImage(Image, left, top, drawWidth, drawHeight);

            }
        }
    }
}
