using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.GeneralComponents
{
    public partial class RichTextBoxWithBar : UserControl
    {

        public RichTextBox TextBox
        {
            get => this.richTextBox1;
        }

        public int BarWidth
        {
            get => panel1.Width;
            set
            {
                panel1.Width = value;
                drawPoints();
            }
        }

        private int circleSize;
        public int CircleSize
        {
            get => circleSize;
            set
            {
                circleSize = value;
                drawPoints();
            }
        }

        public RichTextBoxWithBar()
        {
            InitializeComponent();
        }

        private Dictionary<int, PointData> pointData = new Dictionary<int, PointData>();

        public void SetPointColor(int line, Color color, string tooltip = "")
        {
            if (line < 0)
                throw new ArgumentException("Line number must be a non-negative integer!", nameof(line));
            pointData[line] = new PointData(color, tooltip);
            drawPoints();
        }

        public void RemovePoint(int line)
        {
            if (!pointData.ContainsKey(line))
                return;
            pointData.Remove(line);
            drawPoints();
        }
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            drawPoints();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            drawPoints();
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            drawPoints();
        }

        private void RichTextBoxWithBar_Load(object sender, EventArgs e)
        {
            drawPoints();
        }

        private void drawPoints()
        {
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Point basePosition = TextBox.GetPositionFromCharIndex(0);
            float lineHeight = 0;
            if (TextBox.Lines.Length > 1)
            {
                int secondLineFirstCharIndex = TextBox.GetFirstCharIndexFromLine(1);
                Point secondPosition = TextBox.GetPositionFromCharIndex(secondLineFirstCharIndex);
                lineHeight = secondPosition.Y - basePosition.Y;
            }
            else
            {
                lineHeight = TextBox.Font.Height;
            }

            float drawSize = CircleSize;
            if ((lineHeight > 0) && (CircleSize >= lineHeight))
                drawSize = lineHeight - 2;

            float yBase = basePosition.Y + (lineHeight / 2) + 2;
            float xCenter = BarWidth / 2.0f;
            float x = xCenter - (drawSize / 2.0f);
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                float yCenter = yBase + i * lineHeight;
                float y = yCenter - (drawSize / 2.0f);
                if (pointData.TryGetValue(i, out PointData pd))
                    e.Graphics.FillEllipse(new SolidBrush(pd.Color), x, y, drawSize, drawSize);
            }


        }

        private class PointData
        {
            public Color Color { get; private set; }
            public string Tooltip { get; private set; }
            public PointData(Color color, string tooltip)
            {
                this.Color = color;
                this.Tooltip = tooltip;
            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            int activeTooltip = -1;

            Point basePosition = TextBox.GetPositionFromCharIndex(0);
            float lineHeight = 0;
            if (TextBox.Lines.Length > 1)
            {
                int secondLineFirstCharIndex = TextBox.GetFirstCharIndexFromLine(1);
                Point secondPosition = TextBox.GetPositionFromCharIndex(secondLineFirstCharIndex);
                lineHeight = secondPosition.Y - basePosition.Y;
            }

            float drawSize = (CircleSize >= lineHeight) ? (lineHeight - 1) : CircleSize;
            float yBase = basePosition.Y + lineHeight / 2 + 2;
            float xCenter = BarWidth / 2.0f;
            float x = xCenter - (drawSize / 2.0f);
            for (int i = 0; i < richTextBox1.Lines.Length; i++)
            {
                float yCenter = yBase + i * lineHeight;
                float y = yCenter - (drawSize / 2.0f);
                if (Math.Sqrt(Math.Pow((e.X - xCenter), 2) + Math.Pow((e.Y - yCenter), 2)) <= drawSize)
                    activeTooltip = i;
            }

            if ((activeTooltip >= 0) && pointData.TryGetValue(activeTooltip, out PointData pd) && !string.IsNullOrEmpty(pd.Tooltip))
                toolTip1.Show(pd.Tooltip, panel1, e.Location + new Size(10, 10));
            else
                toolTip1.Hide(ParentForm);

        }

    }
}
