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
        private int obrazenia;
        private string kierunek;
        private int pozycjaX;
        private int pozycjaY;

        public Wrog(int startHp, int startObr, string kier, int startX, int startY)
        {
            hp = startHp;
            obrazenia = startObr;
            kierunek = kier;
            pozycjaX = startX;
            pozycjaY = startY;
        }

        public void zmniejszHp(int ilosc)
        {
            hp -= ilosc;
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

        public int getPozycjaX()
        {
            return pozycjaX;
        }

        public int getPozycjaY()
        {
            return pozycjaY;
        }

        public string getKierunek()
        {
            return kierunek;
        }

        public void setPozycjaX(int x)
        {
            pozycjaX = x;
        }
        public void setPozycjaY(int y)
        {
            pozycjaY = y;
        }

        public void zmianaKierunku(string nowyKierunek)
        {
            kierunek = nowyKierunek;
        }


    }
}
