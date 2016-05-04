using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public static class LinqExtensions
    {
        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    return default(T);

                var min = iterator.Current;
                var minKey = selector(min);
                IComparer<TKey> comparer = Comparer<TKey>.Default;

                while (iterator.MoveNext())
                {
                    var current = iterator.Current;
                    var currentKey = selector(current);
                    if (comparer.Compare(currentKey, minKey) < 0)
                    {
                        min = current;
                        minKey = currentKey;
                    }
                }

                return min;
            }
        }
    }
}
