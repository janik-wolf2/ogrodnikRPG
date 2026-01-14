using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ogrodnikRPG.klasy;

namespace ogrodnikRPG.strony
{
    /// <summary>
    /// Logika interakcji dla klasy planszaGry.xaml
    /// </summary>
    public partial class planszaGry : Page
    {
        public planszaGry()
        {
            InitializeComponent();
            generujPlansze(10, 8);
            stworzPostac();
            stworzWroga("chwast");
            stworzWroga("stonka");
            stworzWroga("krecik");
            stworzKwiatka("kwiatek1");
            stworzKwiatka("kwiatek2");
            stworzKwiatka("kwiatek2");
        }
        Gracz gracz = new Gracz();
        Plansza plansza1 = new Plansza(3, 3);
        Random rnd = new Random();
        Wrog wrogChwast = new Wrog(20, 5, "prawo", 0, 0);
        Wrog wrogStonka = new Wrog(50, 10, "prawo", 0, 0);
        Wrog wrogKrecik = new Wrog(60, 25, "prawo", 0, 0);

        public void generujPlansze(int szerokosc, int wysokosc)
        {
            for (int r = 0; r < wysokosc; r++)
            {
                siatka.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }

            for (int r = 0; r < szerokosc; r++)
            {
                siatka.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }

            List<Image> polaPlanszy = new List<Image>();
            for (int r = 0; r < wysokosc; r++)
            {
                for (int c = 0; c < szerokosc; c++)
                {
                    Image img = new Image()
                    {
                        Tag = (c, r),
                        Stretch = Stretch.Fill
                    };

                    Border border = new Border()
                    {
                        BorderBrush = Brushes.White,
                        BorderThickness = new Thickness(2),
                        Child = img
                    };


                    Grid.SetRow(border, r);
                    Grid.SetColumn(border, c);

                    siatka.Children.Add(border);
                    polaPlanszy.Add(img);
                    plansza1.setPolaPlanszy(polaPlanszy);
                }
            }
        }

        public void stworzPostac()
        {
            List<Image> polaPlanszy = plansza1.getPolaPlanszy();
            polaPlanszy[0].Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
        }

        public void stworzWroga(string typ) //wybieranie losowego wolnego pola i tworzenie na nim wroga
        {
            List<Image> polaPlanszy = plansza1.getPolaPlanszy();
            List<Image> wolnePola = new List<Image>();

            foreach(var pole in polaPlanszy)
            {
                if (pole.Source == null)
                {
                    wolnePola.Add(pole);
                }
            }

            Image losoweWolnePole = wolnePola[rnd.Next(wolnePola.Count)];
            var (x, y) = ((int, int))losoweWolnePole.Tag;

            switch (typ)
            {
                case "chwast":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/chwast.png", UriKind.Relative));
                    wrogChwast.setPozycjaX(x);
                    wrogChwast.setPozycjaY(y);
                    break;
                case "stonka":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/stonka.png", UriKind.Relative));
                    wrogStonka.setPozycjaX(x);
                    wrogStonka.setPozycjaY(y);
                    break;
                case "krecik":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/krecik.png", UriKind.Relative));
                    wrogKrecik.setPozycjaX(x);
                    wrogKrecik.setPozycjaY(y);
                    break;
            }
        }

        public void stworzKwiatka(string typ) //wybieranie losowego wolnego pola i tworzenie na nim kwiatka
        {
            List<Image> polaPlanszy = plansza1.getPolaPlanszy();
            List<Image> wolnePola = new List<Image>();

            foreach (var pole in polaPlanszy)
            {
                if (pole.Source == null)
                {
                    wolnePola.Add(pole);
                }
            }

            Image losoweWolnePole = wolnePola[rnd.Next(wolnePola.Count)];

            switch (typ)
            {
                case "kwiatek1":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek.png", UriKind.Relative));
                    break;
                case "kwiatek2":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek2.png", UriKind.Relative));
                    break;
                case "krecik":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/krecik.png", UriKind.Relative));
                    break;
            }
        }

        public void idzGora()
        {
            if (sprawdzaniePrzeszkody(0, -1) == true)
            {
                return;
            }

            if (gracz.getPozycjaY() == 0)
            {
                return;
            }

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            usunZdjecie(pozycjaX, pozycjaY);

            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX == x && pozycjaY - 1 == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zmniejszY();
                }
            }

            ruchyPrzeciwnikow();
        }

        public void idzDol()
        {
            if(sprawdzaniePrzeszkody(0, 1) == true)
            {
                return;
            }

            if (gracz.getPozycjaY() == 7)
            {
                return;
            }

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            usunZdjecie(pozycjaX, pozycjaY);

            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX == x && pozycjaY + 1 == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zwiekszY();
                }
            }

            ruchyPrzeciwnikow();
        }

        public void idzLewo()
        {
            if (sprawdzaniePrzeszkody(-1, 0) == true)
            {
                return;
            }

            if (gracz.getPozycjaX() == 0)
            {
                return;
            }

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            usunZdjecie(pozycjaX, pozycjaY);

            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX - 1 == x && pozycjaY == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zmniejszX();
                }
            }

            ruchyPrzeciwnikow();
        }

        public void idzPrawo()
        {
            if (sprawdzaniePrzeszkody(1, 0) == true)
            {
                return;
            }

            if (gracz.getPozycjaX() == 9)
            {
                return;
            }

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            usunZdjecie(pozycjaX, pozycjaY);

            
            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX + 1 == x && pozycjaY == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zwiekszX();
                }
            }

            ruchyPrzeciwnikow();
        }

        public void stonkaPoruszanie()
        {
            int pozycjaX = wrogStonka.getPozycjaX();
            int pozycjaY = wrogStonka.getPozycjaY();
            string kierunek = wrogStonka.getKierunek();

            if ((sprawdzaniePrzeszkody(1, 0) == true || pozycjaX == 9) && kierunek == "prawo") //zmiana kierunku w lewo
            {
                wrogStonka.zmianaKierunku("lewo");
            }

            if ((sprawdzaniePrzeszkody(-1, 0) == true || pozycjaX == 0) && kierunek == "lewo") //zmiana kierunku w prawo
            {
                wrogStonka.zmianaKierunku("prawo");
            }

            usunZdjecie(pozycjaX, pozycjaY);

            if (wrogStonka.getKierunek() == "prawo")
            {
                foreach (var pole in plansza1.getPolaPlanszy())
                {
                    var (x, y) = ((int, int))pole.Tag;

                    if (pozycjaX + 1 == x && pozycjaY == y)
                    {
                        pole.Source = new BitmapImage(new Uri("/resource/stonka.png", UriKind.Relative));
                        wrogStonka.zwiekszX();
                    }
                }
            }
            else
            {
                foreach (var pole in plansza1.getPolaPlanszy())
                {
                    var (x, y) = ((int, int))pole.Tag;

                    if (pozycjaX - 1 == x && pozycjaY == y)
                    {
                        pole.Source = new BitmapImage(new Uri("/resource/stonka.png", UriKind.Relative));
                        wrogStonka.zmniejszX();
                    }
                }
            }
        }


        public void usunZdjecie(int pozycjaX, int pozycjaY) //znajdowanie pola na ktorym jest gracz i usuwanie jego obrazka
        {
            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX == x && pozycjaY == y)
                {
                    pole.Source = null;
                }
            }
        }

        public bool sprawdzaniePrzeszkody(int przesuniecieX, int przesuniecieY)
        {
            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();

            foreach (var pole in plansza1.getPolaPlanszy()) //sprawdzanie czy w poblizu nie stoi wrog albo kwiatek
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX + przesuniecieX == x && pozycjaY + przesuniecieY == y) //czy pole sie zgadza
                {
                    if(pole.Source != null && sprawdzanieKwiatka(pole) == false)
                    {
                        return true;
                    }
                    else if(sprawdzanieKwiatka(pole) == true) //czy tam jest kwiatek, jesli tak to mozna wejsc na pole
                    {
                        gracz.zebranoKwiatek();
                        wskaznikKwiatkow.Content = "Kwiatki: " + gracz.getZebraneKwiatki().ToString() + "/3";
                        return false;
                    }
                }
            }
            return false;
        }

        public bool sprawdzanieKwiatka(Image pole)
        {
            if (pole.Source is BitmapImage bmp && bmp.UriSource != null)
            {
                string uri = bmp.UriSource.ToString();
                
                return uri.Contains("kwiatek.png") || uri.Contains("kwiatek2.png");
            }
            return false;
        }

        public void ruchyPrzeciwnikow()
        {
            gracz.zwiekszTure();
            wskaznikTury.Content = "Tura: " + gracz.getTura().ToString();
            stonkaPoruszanie();
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    idzGora(); break;

                case Key.S:
                    idzDol(); break;

                case Key.A:
                    idzLewo(); break;

                case Key.D:
                    idzPrawo(); break;

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }


    }
}
