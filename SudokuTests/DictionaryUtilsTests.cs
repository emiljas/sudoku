using SudokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SudokuTests
{
    public class DictionaryUtilsTests
    {
        [Fact]
        public void Clone()
        {
            var dict = new Dictionary<string, List<int>>();
            dict.Add("1", new List<int> { 1 });

            var dictClone = DictionaryUtils.Clone(dict);

            dict["1"].Remove(1);

            Assert.Equal(1, dictClone["1"].Count);
        }
    }
}
