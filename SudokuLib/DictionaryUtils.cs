using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public static class DictionaryUtils
    {
        public static Dictionary<string, List<int>> Clone(Dictionary<string, List<int>> dict)
        {
            var dictClone = new Dictionary<string, List<int>>(dict);
            foreach(var vk in dict)
            {
                dictClone[vk.Key] = new List<int>(vk.Value);
            }
            return dictClone;
        }
    }
}
