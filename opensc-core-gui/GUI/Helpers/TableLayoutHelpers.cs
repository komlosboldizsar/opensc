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
            int columnIndex = 0; 
            while (columnIndex < tableLayout.ColumnCount)
            {
                Control originalControl = tableLayout.GetControlFromPosition(columnIndex, newSourceIndex);
                if (originalControl != null)
                {
                    int columnSpan = tableLayout.GetColumnSpan(originalControl);
                    Control clonedControl = originalControl.CloneTypeOnly();
                    tableLayout.Controls.Add(clonedControl, columnIndex, newDestinationIndex);
                    tableLayout.SetColumnSpan(clonedControl, columnSpan);
                    clonedControl.ClonePropertiesFrom(originalControl, excludeProperties);
                    columnIndex += columnSpan;
                }
                else
                {
                    columnIndex++;
                }
            }
        }

    }

}
