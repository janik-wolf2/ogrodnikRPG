using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Krecik : Wrog
    {
        public Krecik(string nazwa, int startHp, int startObr, int startX, int startY)
            : base(nazwa, startHp, startObr, startX, startY)
        {
            hp = startHp;
            obrazenia = startObr;
            pozycjaX = startX;
            pozycjaY = startY;
        }
    }
}
