using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Wrog
    {
        private int hp;
        private int pozycjaX;
        private int pozycjaY;

        public Wrog(int startHp, int startX, int startY)
        {
            hp = startHp;
            pozycjaX = startX;
            pozycjaY = startY;
        }

        public void zmniejszHp(int ilosc)
        {
            hp -= ilosc;
        }

        public int getPozycjaX()
        {
            return pozycjaX;
        }

        public int getPozycjaY()
        {
            return pozycjaY;
        }

        public void setPozycjaX(int x)
        {
            pozycjaX = x;
        }
        public void setPozycjaY(int y)
        {
            pozycjaY = y;
        }

    }
}
