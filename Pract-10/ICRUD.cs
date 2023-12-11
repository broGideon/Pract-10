using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_10
{
    internal interface ICRUD
    {
        public void Create(string file);
        public void Read(string file, int pos);
        public void Update(int pos, string file);
        public void Delete(int pos, string file);
        public void Filter(string file);
    }
}