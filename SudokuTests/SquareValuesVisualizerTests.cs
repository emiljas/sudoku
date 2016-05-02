using SudokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SudokuTests
{
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
