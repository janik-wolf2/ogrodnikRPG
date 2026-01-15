using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Gracz
    {
        private int hp = 100;
        private int tura = 0;
        private int zebraneKwiatki = 0;
        private int przedmioty = 0;
        private int pozycjaX = 0;
        private int pozycjaY = 0;

        public Gracz()
        {

        }

        public void zwiekszTure()
        {
            tura++;
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

        public int getTura()
        {
            return tura;
        }
        public int getPozycjaX()
        {
            return pozycjaX;
        }

        public int getPozycjaY()
        {
            return pozycjaY;
        }

        public void zebranoKwiatek()
        {
            zebraneKwiatki++;
        }

        public int getZebraneKwiatki()
        {
            return zebraneKwiatki;
        }
    }
}
