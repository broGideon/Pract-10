using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_10
{
    internal class SelectedProduct : Product
    {
        public int ViborColvo;
        public SelectedProduct (int ID, string Name, double Cost, int Colvo, int ViborColvo)
        {
            this.ID = ID;
            this.Name = Name;
            this.Cost = Cost;
            this.Colvo = Colvo;
            this.ViborColvo = ViborColvo;
        }
    }
}
