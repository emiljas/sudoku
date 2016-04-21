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

        private readonly string[] rows = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        private readonly string[] cols = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private string sudokuStr;
        private List<string> squares;
        private Dictionary<string, List<int>> squareValues;
        private List<List<string>> units;
        private Dictionary<string, List<List<string>>> squareUnits;
        private Dictionary<string, List<string>> squarePeers;

        public Sudoku(string sudokuStr)
        {
            this.sudokuStr = sudokuStr;
            this.squares = GenerateSquares();
            this.squareValues = ParseSudokuStrToSquareValuesDict();
            this.units = GenerateUnits();
            this.squareUnits = GenerateSquareUnitsDict();
            this.squarePeers = GenerateSquarePeersDict();
        }

        private List<string> GenerateSquares()
        {
            var squares = new List<string>();
            foreach (var r in rows)
                foreach (var c in cols)
                    squares.Add(r + c);
            System.Diagnostics.Debug.Assert(squares.Count == SUDOKU_VALUES_LENGTH);
            return squares;
        }

        private Dictionary<string, List<int>> ParseSudokuStrToSquareValuesDict()
        {
            var squareValues = new Dictionary<string, List<int>>();

            List<int> sudokuValues = ParseSudokuValues();
            for (int i = 0; i < SUDOKU_VALUES_LENGTH; i++)
            {
                string square = squares[i];
                int value = sudokuValues[i];
                if (value == 0)
                    squareValues.Add(square, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                else
                    squareValues.Add(square, new List<int> { value });
            }

            return squareValues;
        }

        private List<int> ParseSudokuValues()
        {
            var sudokuValues = new List<int>();
            for(int i = 0; i < this.sudokuStr.Length; i++)
            {
                char c = this.sudokuStr[i];
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

        private List<List<string>> GenerateUnits()
        {
            var units = new List<List<string>>();

            foreach(var g in squares.GroupBy(s => s[0]))
                units.Add(g.ToList());

            foreach(var g in squares.GroupBy(s => s[1]))
                units.Add(g.ToList());

            new List<char[]>
            {
                new char[] { 'A', 'B', 'C' },
                new char[] { 'D', 'E', 'F' },
                new char[] { 'G', 'H', 'I' },
            }
        }

        private Dictionary<string, List<List<string>>> GenerateSquareUnitsDict()
        {
        }

        private Dictionary<string, List<string>> GenerateSquarePeersDict()
        {
            var squarePeers = new Dictionary<string, List<string>>();
            foreach(var square in this.squares)
            {
                var peers = new List<string>();

                foreach (var c in this.cols)
                {
                    string peer = square[0] + c;
                    if(peer != square)
                        peers.Add(peer);
                }

                foreach (var r in this.rows)
                {
                    string peer = r + square[1];
                    if(peer != square)
                        peers.Add(peer);
                }

                squarePeers.Add(square, peers);
            }
            //A2 - A1 A2 A3..
            return squarePeers;
        }

        public string Solve()
        {
            return "";
        }
    }
}
