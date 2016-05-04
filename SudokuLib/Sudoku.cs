using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLib
{
    //TODO: spróbować wyeliminować uncheckedFilledSquares, testy wydajności?
    public class Sudoku
    {
        public const int SUDOKU_VALUES_LENGTH = 81;

        private static readonly string[] rows = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        private static readonly string[] cols = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private static readonly List<string> squares;
        private static readonly List<List<string>> units;
        private static readonly Dictionary<string, List<List<string>>> squareUnits;
        private static readonly  Dictionary<string, HashSet<string>> squarePeers;

        private string sudokuStr;
        private Dictionary<string, List<int>> squareValues;

        static Sudoku()
        {
            squares = GenerateSquares();
            units = GenerateUnits();
            squareUnits = GenerateSquareUnitsDict();
            squarePeers = GenerateSquarePeersDict();
        }

        private static List<string> GenerateSquares()
        {
            var squares = new List<string>();
            foreach (var r in rows)
                foreach (var c in cols)
                    squares.Add(r + c);
            System.Diagnostics.Debug.Assert(squares.Count == SUDOKU_VALUES_LENGTH);
            return squares;
        }

        private static List<List<string>> GenerateUnits()
        {
            var units = new List<List<string>>();

            foreach(var g in squares.GroupBy(s => s[0]))
                units.Add(g.ToList());

            foreach(var g in squares.GroupBy(s => s[1]))
                units.Add(g.ToList());

            var letterGroups = new List<char[]>
            {
                new char[] { 'A', 'B', 'C' },
                new char[] { 'D', 'E', 'F' },
                new char[] { 'G', 'H', 'I' },
            };

            var digitGroups = new List<char[]>
            {
                new char[] { '1', '2', '3' },
                new char[] { '4', '5', '6' },
                new char[] { '7', '8', '9' },
            };

            foreach(char[] letterGroup in letterGroups)
            {
                foreach(char[] digitGroup in digitGroups)
                {
                    var unit = new List<string>();
                    foreach(char letter in letterGroup)
                    {
                        foreach(char digit in digitGroup)
                        {
                            unit.Add(letter.ToString() + digit.ToString());
                        }
                    }
                    units.Add(unit);
                }
            }

            return units;
        }

        private static Dictionary<string, List<List<string>>> GenerateSquareUnitsDict()
        {
            var dict = new Dictionary<string, List<List<string>>>();
            foreach(string square in squares)
                dict.Add(square, units.Where(u => u.Contains(square)).ToList());
            return dict;
        }

        private static Dictionary<string, HashSet<string>> GenerateSquarePeersDict()
        {
            var dict = new Dictionary<string, HashSet<string>>();
            foreach(string square in squares)
            {
                var units = squareUnits[square];
                var peers = new HashSet<string>();
                foreach(var unit in units)
                    foreach(string peer in unit)
                        peers.Add(peer);
                peers.Remove(square);
                dict.Add(square, peers);
            }
            return dict;
        }

        public Sudoku(string sudokuStr)
        {
            this.sudokuStr = sudokuStr;
            this.squareValues = ParseSudokuStrToSquareValuesDict();
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

        public string Solve()
        {
            ExcludeFilledValueFromPeers(this.squareValues);

            bool isSolved = this.squareValues.All(v => v.Value.Count == 1);
            if(!isSolved)
                this.squareValues = SearchForSolution();

            return StringifySquaresValues();
        }

        private ExcludeFilledValueFromPeersStatus ExcludeFilledValueFromPeers(Dictionary<string, List<int>> squareValues)
        {
            foreach (var vk in squareValues)
            {
                if (vk.Value.Count == 1)
                {
                    var status = ExcludeFilledValueFromPeers(squareValues, vk.Key);
                    if (status == ExcludeFilledValueFromPeersStatus.Conflict)
                        return ExcludeFilledValueFromPeersStatus.Conflict;
                }
            }

            return ExcludeFilledValueFromPeersStatus.Done;
        }

        private ExcludeFilledValueFromPeersStatus ExcludeFilledValueFromPeers(Dictionary<string, List<int>> squareValues, string square)
        {
            var values = squareValues[square];

            int value = values[0];
            var peers = squarePeers[square];
            foreach (var peer in peers)
            {
                var peerValues = squareValues[peer];
                bool wasRemoved = peerValues.Remove(value);
                if (peerValues.Count == 0)
                    return ExcludeFilledValueFromPeersStatus.Conflict;

                if (wasRemoved && peerValues.Count == 1)
                {
                    var status = ExcludeFilledValueFromPeers(squareValues, peer);
                    if (status == ExcludeFilledValueFromPeersStatus.Conflict)
                        return ExcludeFilledValueFromPeersStatus.Conflict;
                }

            }

            return ExcludeFilledValueFromPeersStatus.Done;
        }

        private Dictionary<string, List<int>> SearchForSolution()
        {
            return SearchForSolution(this.squareValues);
        }

        private Dictionary<string, List<int>> SearchForSolution(Dictionary<string, List<int>> squaresValues)
        {
            var unfilledSquareField = squaresValues.MinBy(kv => kv.Value.Count + (kv.Value.Count == 1 ? 10 : 0));
            if (unfilledSquareField.Value.Count == 1)
                return squaresValues;

            foreach (int value in unfilledSquareField.Value)
            {
                string square = unfilledSquareField.Key;

                var squareValuesToSearch = DictionaryUtils.Clone(squaresValues);
                squareValuesToSearch[square] = new List<int> { value };

                var status = ExcludeFilledValueFromPeers(squareValuesToSearch, square);

                if (status == ExcludeFilledValueFromPeersStatus.Conflict)
                    continue;

                var solution = SearchForSolution(squareValuesToSearch);
                if (solution != null)
                    return solution;
            }

            return null;
        }

        private string StringifySquaresValues()
        {
            string ret = "";
            foreach (var values in this.squareValues)
                ret += values.Value[0].ToString();
            return ret;
        }
    }
}
