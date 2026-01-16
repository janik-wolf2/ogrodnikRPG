using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Przedmiot : ObiektGry
    {
        private string nazwa;

        public Przedmiot(string n, int x, int y)
        {
            nazwa = n;
            pozycjaX = x;
            pozycjaY = y;
        }

        public string getNazwa()
        {
            return nazwa;
        }
    }
}
