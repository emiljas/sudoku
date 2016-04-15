using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public class Sudoku
    {
        public const int SUDOKU_VALUES_LENGTH = 81;

        private string[] rows = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        private string[] cols = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private List<string> squares;
        private string sudokuStr;

        public Sudoku(string sudokuStr)
        {
            GenerateSquares();
        }

        public string Solve()
        {
            var squareValues = ParseSudokuStrToSquareValuesDict();
            return "";
        }

        private Dictionary<string, List<int>> ParseSudokuStrToSquareValuesDict()
        {
            var dict = new Dictionary<string, List<int>>();

            List<int> sudokuValues = ParseSudokuValues();
            for (int i = 0; i < SUDOKU_VALUES_LENGTH; i++)
            {
                string square = squares[i];
                int value = sudokuValues[i];
                if (value == 0)
                    dict.Add(square, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                else
                    dict.Add(square, new List<int> { value });
            }

            return dict;
        }

        private void GenerateSquares()
        {
            squares = new List<string>();
            foreach (var r in rows)
                foreach (var c in cols)
                    squares.Add(r + c);
            System.Diagnostics.Debug.Assert(squares.Count == 81);
        }

        private List<int> ParseSudokuValues()
        {
            var sudokuValues = new List<int>();
            for(int i = 0; i < sudokuStr.Length; i++)
            {
                char c = sudokuStr[i];
                if (Char.IsNumber(c))
                {
                    int value = int.Parse(c.ToString());
                    sudokuValues.Add(value);
                }
                else if(c == '.')
                {
                    sudokuValues.Add(0);
                }
            }

            if(sudokuValues.Count != SUDOKU_VALUES_LENGTH)
                throw new Exception("Invalid sudoku string");

            return sudokuValues;
        }
    }
}
