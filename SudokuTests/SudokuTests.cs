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
        public void SudokuSolvedByEliminatingValuesFromPeers()
        {
            string simpleSudoku =
                "..3.2.6.." +
                "9..3.5..1" +
                "..18.64.." +
                "..81.29.." +
                "7.......8" +
                "..67.82.." +
                "..26.95.." +
                "8..2.3..9" +
                "..5.1.3..";

            string simpleSudokuSolution =
                "483921657" +
                "967345821" +
                "251876493" +
                "548132976" +
                "729564138" +
                "136798245" +
                "372689514" +
                "814253769" +
                "695417382";

            var sudoku = new Sudoku(simpleSudoku);
            Assert.Equal(simpleSudokuSolution, sudoku.Solve());
        }

        //[Fact]
        //public void SudokuSolvedBySearch()
        //{
        //    string difficultSudoku =
        //        "4.....8.5" +
        //        ".3......." +
        //        "...7....." +
        //        ".2.....6." +
        //        "....8.4.." +
        //        "....1...." +
        //        "...6.3.7." +
        //        "5..2....." +
        //        "1.4......";

        //    string difficultSudokuSolution =
        //        "417369825" +
        //        "632158947" +
        //        "958724316" +
        //        "825437169" +
        //        "791586432" +
        //        "346912758" +
        //        "289643571" +
        //        "573291684" +
        //        "164875293";

        //    var sudoku = new Sudoku(difficultSudoku);
        //    Assert.Equal(difficultSudokuSolution, sudoku.Solve());
        //}
    }
}
