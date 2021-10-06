using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.WorkspaceManager.ValueConverters
{

    [ConverterType(typeof(Color), "color")]
    class ColorConverter : IValueConverter
    {

        public string Serialize(object obj)
        {
            Color c = (Color)obj;
            return string.Format("{0},{1},{2},{3}", c.A, c.R, c.G, c.B);
        }

        public object Deserialize(string serialized)
        {
            string[] components = serialized.Split(new char[] { ',' });
            int.TryParse(components[0], out int A);
            int.TryParse(components[1], out int R);
            int.TryParse(components[2], out int G);
            int.TryParse(components[3], out int B);
            return Color.FromArgb(A, R, G, B);
        }

    }

}
