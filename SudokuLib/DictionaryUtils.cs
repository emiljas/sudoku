using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLib
{
    public static class DictionaryUtils
    {
        public static Dictionary<string, List<int>> Clone(Dictionary<string, List<int>> dict)
        {
            return FromBinary<Dictionary<string, List<int>>>(ToBinary(dict));
            //var dictClone = new Dictionary<string, List<int>>(dict.Count);
            //foreach (var vk in dict)
            //    dictClone.Add(vk.Key, new List<int>(vk.Value));
            //return dictClone;
        }

        public static Byte[] ToBinary(object obj)
        {
            MemoryStream ms = null;
            Byte[] byteArray = null;
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                ms = new MemoryStream();
                serializer.Serialize(ms, obj);
                byteArray = ms.ToArray();
            }
            catch (Exception unexpected)
            {
                Trace.Fail(unexpected.Message);
                throw;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return byteArray;
        }

        public static T FromBinary<T>(Byte[] buffer) where T: class
        {
            MemoryStream ms = null;
            T deserializedObject = null;

            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                ms = new MemoryStream();
                ms.Write(buffer, 0, buffer.Length);
                ms.Position = 0;
                deserializedObject = serializer.Deserialize(ms) as T;
            }
            finally
            {
                if (ms != null)
                    ms.Close();
            }
            return deserializedObject;
        }
    }
}
