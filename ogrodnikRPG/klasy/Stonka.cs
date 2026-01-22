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

        public Stonka(string nazwa, int startHp, int startObr, string kier, int startX, int startY)
        : base(nazwa, startHp, startObr, startX, startY)
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
