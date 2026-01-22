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
        private List<Przedmiot> ekwipunek = new List<Przedmiot>();
        private int aktywnyPrzedmiot = 0;

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

        public List<Przedmiot> getEkwipunek()
        {
            return ekwipunek;
        }

        public void dodajDoEkwipunku(Przedmiot p)
        {
            ekwipunek.Add(p);
        }

        public int getAktywnyPrzedmiot()
        {
            return aktywnyPrzedmiot;
        }

        public void setAktywnyPrzedmiot(int x)
        {
            aktywnyPrzedmiot = x;
        }
    }
}
