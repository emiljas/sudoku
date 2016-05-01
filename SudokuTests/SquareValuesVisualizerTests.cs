using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SudokuTests
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

            foreach(var vk in squareValues.OrderBy(vk => vk.Key))
            {
                vk.
            }

            return visualization;
        }
    }

    public class SquareValuesVisualizerTests
    {
        [Fact]
        public void Visualize()
        {
            var squareValues = new Dictionary<string, List<int>>();
            squareValues["A1"] = new List<int> { 6 };
            squareValues["A2"] = new List<int> { 1, 2 };
            squareValues["A3"] = new List<int> { 3 };
            squareValues["B1"] = new List<int> { 1 };
            squareValues["B2"] = new List<int> { 2 };
            squareValues["B3"] = new List<int> { 3 };

            string visualization = SquareValuesVisualizer.Visualize(squareValues);

            Assert.Equal("6 12 3\n1  2 3\n", visualization);
        }
    }
}
