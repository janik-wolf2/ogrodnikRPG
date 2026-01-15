using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class ObiektGry
    {
        protected int pozycjaX;
        protected int pozycjaY;

        public void setPozycjaX(int x)
        {
            pozycjaX = x;
        }

        public void setPozycjaY(int y)
        {
            pozycjaY = y;
        }

        public int getPozycjaX()
        {
            return pozycjaX;
        }

        public int getPozycjaY()
        {
            return pozycjaY;
        }

        public void zmniejszX()
        {
            pozycjaX--;
        }
        public void zwiekszX()
        {
            pozycjaX++;
        }
        public void zmniejszY()
        {
            pozycjaY--;
        }
        public void zwiekszY()
        {
            pozycjaY++;
        }
    }
 
}
