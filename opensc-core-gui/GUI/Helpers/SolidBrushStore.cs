using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.GUI.Helpers
{

    public static class SolidBrushStore
    {

        public static Brush Get(Color color)
        {
            if (storedBrushes.TryGetValue(color, out Brush storedBrush))
                return storedBrush;
            Brush newStoredBrush = new SolidBrush(color);
            storedBrushes.Add(color, newStoredBrush);
            return newStoredBrush;
        }

        private static Dictionary<Color, Brush> storedBrushes = new();

        static SolidBrushStore()
        {
            PropertyInfo[] properties = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public);
            Type brushType = typeof(Brush);
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == brushType)
                {
                    Brush brush = property.GetValue(null) as Brush;
                    if (brush != null)
                        storedBrushes.Add(((SolidBrush)brush).Color, brush);
                }
            }
        }

    }

}
