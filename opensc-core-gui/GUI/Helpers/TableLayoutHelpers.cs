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
            if (destinationIndex < 0)
                destinationIndex = tableLayout.RowCount + destinationIndex;
            tableLayout.RowStyles.Insert(destinationIndex, destinationStyle);
            int newSourceIndex = sourceIndex;
            if (destinationIndex <= sourceIndex)
                newSourceIndex++;
            int columnIndex = 0; 
            while (columnIndex < tableLayout.ColumnCount)
            {
                Control originalControl = tableLayout.GetControlFromPosition(columnIndex, newSourceIndex);
                if (originalControl != null)
                {
                    int columnSpan = tableLayout.GetColumnSpan(originalControl);
                    Control clonedControl = originalControl.CloneTypeOnly();
                    tableLayout.Controls.Add(clonedControl, columnIndex, destinationIndex);
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

        public const int COLUMN_INDEX_LAST = -1;

        public static void CloneColumn(this TableLayoutPanel tableLayout, int sourceIndex, int destinationIndex = COLUMN_INDEX_LAST, string[] excludeProperties = null)
        {
            if (tableLayout.ColumnCount <= sourceIndex)
                throw new ArgumentException();
            tableLayout.ColumnCount++;
            ColumnStyle sourceStyle = tableLayout.ColumnStyles[sourceIndex];
            ColumnStyle destinationStyle = new ColumnStyle(sourceStyle.SizeType, sourceStyle.Width);
            if (destinationIndex < 0)
                destinationIndex = tableLayout.ColumnCount + destinationIndex;
            tableLayout.ColumnStyles.Insert(destinationIndex, destinationStyle);
            int newSourceIndex = sourceIndex;
            if (destinationIndex <= sourceIndex)
                newSourceIndex++;
            int rowIndex = 0;
            while (rowIndex < tableLayout.RowCount)
            {
                Control originalControl = tableLayout.GetControlFromPosition(newSourceIndex, rowIndex);
                if (originalControl != null)
                {
                    int rowSpan = tableLayout.GetRowSpan(originalControl);
                    Control clonedControl = originalControl.CloneTypeOnly();
                    tableLayout.Controls.Add(clonedControl, destinationIndex, rowIndex);
                    tableLayout.SetRowSpan(clonedControl, rowSpan);
                    clonedControl.ClonePropertiesFrom(originalControl, excludeProperties);
                    rowIndex += rowSpan;
                }
                else
                {
                    rowIndex++;
                }
            }
        }

    }

}
