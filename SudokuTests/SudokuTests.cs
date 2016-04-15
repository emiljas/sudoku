using SudokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SudokuTests
{
    public class SudokuTests
    {
        [Fact]
        public void Test()
        {
            string simpleSudoku = "..3.2.6..9..3.5..1..18.64....81.29..7.......8..67.82....26.95..8..2.3..9..5.1.3..";
            var sudoku = new Sudoku(simpleSudoku);
            Assert.Equal(@"483921657967345821251876493548132976729564138136798245372689514814253769695417382", sudoku.Solve());
        }
    }
}
