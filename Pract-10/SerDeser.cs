using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_10
{
    internal static class SerDeser
    {
        public static void Serialize<T>(T obj, string file)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(path + "\\" + file, json);
        }
        public static List<T> Deserialize<T>(string file)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string json = File.ReadAllText(path + "\\" + file);
                List<T> obj = JsonConvert.DeserializeObject<List<T>>(json);
                if (obj != null) return obj;
                else return new List<T>();
            }
            catch
            {
                return new List<T>();
            }

        }
    }
}
