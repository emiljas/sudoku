using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class SquareValuesVisualizer
    {
        public static string Visualize(Dictionary<string, List<int>> squareValues)
        {
            string visualization = "";

            var columnsWidths = (from sv in squareValues
                     group sv.Value.Count by sv.Key[1] into column
                     select new { ColumnNumber = column.Key, ColumnWidth = column.ToList().Max() })
                    .OrderBy(c => c.ColumnNumber).Select(c => c.ColumnWidth).ToList();

            int i = 0;
            foreach(var vk in squareValues.OrderBy(vk => vk.Key))
            {
                int columnNumber = i % columnsWidths.Count;
                int columnWidth = columnsWidths[columnNumber];
                visualization += string.Format("{0," + columnWidth + "}", string.Join("", vk.Value));
                visualization += (columnNumber == columnsWidths.Count - 1) ? "\n" : " ";
                i++;
            }

            return visualization;
        }
    }

}
