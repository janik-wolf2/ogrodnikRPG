using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ogrodnikRPG.klasy
{
    internal class Gracz : ObiektGry
    {
        private int hp = 100;
        private int tura = 0;
        private int zebraneKwiatki = 0;
        private int przedmioty = 0;

        public Gracz()
        {

        }

        public void zwiekszTure()
        {
            tura++;
        }

        public int getTura()
        {
            return tura;
        }

        public void zebranoKwiatek()
        {
            zebraneKwiatki++;
        }

        public int getZebraneKwiatki()
        {
            return zebraneKwiatki;
        }

        public int getHp()
        {
            return hp;
        }

        public void zmniejszHp(int ilosc)
        {
            hp -= ilosc;
        }
    }
}
