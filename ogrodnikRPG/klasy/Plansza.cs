using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ogrodnikRPG.klasy
{
    internal class Plansza
    {
        private List<Image> polaPlanszy = new List<Image>();
        private int liczbaKwiatkow;
        private int liczbaWrogow;

        public Plansza(int kwiatki, int wrogowie)
        {
            liczbaKwiatkow = kwiatki;
            liczbaWrogow = wrogowie;
        }

        public void setPolaPlanszy(List<Image> pola)
        {
            polaPlanszy = pola;
        }

        public List<Image> getPolaPlanszy()
        {
            return polaPlanszy;
        }
    }
}
