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
        private int obrazenia;

        public Przedmiot(string n, int obr, int x, int y)
        {
            nazwa = n;
            obrazenia = obr;
            pozycjaX = x;
            pozycjaY = y;
        }

        public string getNazwa()
        {
            return nazwa;
        }

        public int getObrazenia()
        {
            return obrazenia;
        }
    }
}
