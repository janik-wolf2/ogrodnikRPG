using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Krecik : Wrog
    {
        public Krecik(int startHp, int startObr, int startX, int startY)
            : base(startHp, startObr, startX, startY)
        {
            hp = startHp;
            obrazenia = startObr;
            pozycjaX = startX;
            pozycjaY = startY;
        }
    }
}
