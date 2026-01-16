using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Wrog : ObiektGry
    {
        protected int hp;
        protected int obrazenia;

        public Wrog(int startHp, int startObr, int startX, int startY)
        {
            hp = startHp;
            obrazenia = startObr;
            pozycjaX = startX;
            pozycjaY = startY;
        }

        public int getObrazenia()
        {
            return obrazenia;
        }

        public void zmniejszHp(int ilosc)
        {
            hp -= ilosc;
        }




    }
}
