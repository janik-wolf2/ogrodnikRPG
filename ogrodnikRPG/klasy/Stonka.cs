using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Stonka : Wrog
    {
        private string kierunek;

        public Stonka(int startHp, int startObr, string kier, int startX, int startY)
        : base(startHp, startObr, startX, startY)
        {
            kierunek = kier;
        }

        public string getKierunek()
        {
            return kierunek;
        }

        public void zmianaKierunku(string nowyKierunek)
        {
            kierunek = nowyKierunek;
        }

    }
}
