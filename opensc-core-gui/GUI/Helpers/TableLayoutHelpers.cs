using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSC.GUI.Helpers
{

    public static class TableLayoutHelpers
    {

        public const int ROW_INDEX_LAST = -1;

        public static void CloneRow(this TableLayoutPanel tableLayout, int sourceIndex, int destinationIndex = ROW_INDEX_LAST, string[] excludeProperties = null)
        {
            if (tableLayout.RowCount <= sourceIndex)
                throw new ArgumentException();
            tableLayout.RowCount++; 
            RowStyle sourceStyle = tableLayout.RowStyles[sourceIndex];
            RowStyle destinationStyle = new RowStyle(sourceStyle.SizeType, sourceStyle.Height);
            if (destinationIndex != ROW_INDEX_LAST)
                tableLayout.RowStyles.Insert(destinationIndex, destinationStyle);
            else
                tableLayout.RowStyles.Add(destinationStyle);
            int newSourceIndex = sourceIndex;
            if ((destinationIndex != ROW_INDEX_LAST) && (destinationIndex <= sourceIndex))
                newSourceIndex++;
            int newDestinationIndex = (destinationIndex == ROW_INDEX_LAST) ? (tableLayout.RowCount - 1) : destinationIndex;
            for (int columnIndex = 0; columnIndex < tableLayout.ColumnCount; columnIndex++)
            {
                Control clonedControl = tableLayout.GetControlFromPosition(columnIndex, newSourceIndex).Clone(excludeProperties);
                tableLayout.Controls.Add(clonedControl, columnIndex, newDestinationIndex);
            }
        }

    }

}
