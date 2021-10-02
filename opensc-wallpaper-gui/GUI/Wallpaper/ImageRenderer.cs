using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Wallpaper
{
    internal class ImageRenderer
    {

        public ImageRenderer(Image srcImage = null)
        {
            SrcImage = srcImage;
        }

        public Image SrcImage { get; set; }

        private bool monochrome;
        public bool Monochrome
        {
            get => monochrome;
            set
            {
                monochrome = value;
                updateColorMatrix();
            }
        }

        private Color tint;
        public Color Tint
        {
            get => tint;
            set
            {
                tint = value;
                updateColorMatrix();
            }
        }

        private float opacity;
        public float Opacity
        {
            get => opacity;
            set
            {
                opacity = value;
                updateColorMatrix();
            }
        }

        public ImageLayout Layout { get; set; }

        public Size DstSize { get; set; }

        public Image Render(Image srcImage = null)
        {
            srcImage ??= SrcImage;
            if (srcImage == null)
                throw new ArgumentException("A source image needs to be provided.", nameof(srcImage));
            Image dstImage = new Bitmap(DstSize.Width, DstSize.Height);
            using (Graphics graphics = Graphics.FromImage(dstImage))
            {
                // Resize
                Rectangle dstRect = new Rectangle(0, 0, dstImage.Width, dstImage.Height);
                graphics.DrawImage(srcImage, dstRect, 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            return dstImage;
        }

        private ColorMatrix colorMatrix;
        private ImageAttributes imageAttributes = new ImageAttributes();

        private void updateColorMatrix()
        {
            float[][] colorMatrixValues = new float[5][];
            for (int i = 0; i < 5; i++)
            {
                colorMatrixValues[i] = new float[5];
                colorMatrixValues[i][i] = 1.0f;
            }
            colorMatrixValues[3][3] = Opacity;
            if (Monochrome)
            {
                float[] T_SCALAR = new float[] { Tint.R / 255.0f, Tint.G / 255.0f, Tint.B / 255.0f };
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        colorMatrixValues[i][j] = Y_SCALAR[i] * T_SCALAR[j];
            }
            colorMatrix = new ColorMatrix(colorMatrixValues);
            imageAttributes.SetColorMatrix(colorMatrix);
        }

        private readonly float[] Y_SCALAR = new float[] { 0.2126f, 0.7152f, 0.0722f };

    }

}
