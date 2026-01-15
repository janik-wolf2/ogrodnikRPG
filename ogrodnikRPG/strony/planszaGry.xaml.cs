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
        Wrog wrogChwast = new Wrog(20, 5, 0, 0);
        Stonka wrogStonka = new Stonka(50, 10, "prawo", 0, 0);
        Wrog wrogKrecik = new Wrog(60, 25, 0, 0);
        Kwiatek kwiatek1 = new Kwiatek();
        Kwiatek kwiatek2 = new Kwiatek();
        Kwiatek kwiatek3 = new Kwiatek();

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
            var (x, y) = ((int, int))losoweWolnePole.Tag;

            switch (typ)
            {
                case "kwiatek1":
                    kwiatek1.setPozycjaX(x);
                    kwiatek1.setPozycjaY(y);
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek.png", UriKind.Relative));
                    break;
                case "kwiatek2":
                    kwiatek2.setPozycjaX(x);
                    kwiatek2.setPozycjaY(y);
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek2.png", UriKind.Relative));
                    break;
                case "kwiatek3":
                    kwiatek3.setPozycjaX(x);
                    kwiatek3.setPozycjaY(y);
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek3.png", UriKind.Relative));
                    break;
            }
        }

        public void idzGora()
        {
            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();

            if (sprawdzaniePrzeszkody(gracz, 0, -1) == true || pozycjaY == 0)
            {
                return;
            }


            przesunZdjecie(gracz, pozycjaX, pozycjaY, "gora", "/resource/ninja2.png");

            ruchyPrzeciwnikow();
        }

        public void idzDol()
        {
            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();

            if (sprawdzaniePrzeszkody(gracz, 0, 1) == true || pozycjaY == 7)
            {
                return;
            }

            przesunZdjecie(gracz, pozycjaX, pozycjaY, "dol", "/resource/ninja2.png");

            ruchyPrzeciwnikow();
        }

        public void idzLewo()
        {
            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();

            if (sprawdzaniePrzeszkody(gracz, -1, 0) == true || pozycjaX == 0)
            {
                return;
            }

            przesunZdjecie(gracz, pozycjaX, pozycjaY, "lewo", "/resource/ninja2.png");

            ruchyPrzeciwnikow();
        }

        public void idzPrawo()
        {
            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();

            if (sprawdzaniePrzeszkody(gracz, 1, 0) == true || pozycjaX == 9)
            {
                return;
            }
       
            przesunZdjecie(gracz, pozycjaX, pozycjaY, "prawo", "/resource/ninja2.png");

            ruchyPrzeciwnikow();
        }

        public void stonkaPoruszanie()
        {
            int pozycjaX = wrogStonka.getPozycjaX();
            int pozycjaY = wrogStonka.getPozycjaY();
            string kierunek = wrogStonka.getKierunek();

            if ((sprawdzaniePrzeszkody(wrogStonka, 1, 0) == true || pozycjaX == 9) && kierunek == "prawo") //zmiana kierunku w lewo
            {
                wrogStonka.zmianaKierunku("lewo");
            }

            if ((sprawdzaniePrzeszkody(wrogStonka, - 1, 0) == true || pozycjaX == 0) && kierunek == "lewo") //zmiana kierunku w prawo
            {
                wrogStonka.zmianaKierunku("prawo");
            }


            if (wrogStonka.getKierunek() == "prawo")
            {
                przesunZdjecie(wrogStonka, pozycjaX, pozycjaY, "prawo", "/resource/stonka.png");
            }
            else
            {
                przesunZdjecie(wrogStonka, pozycjaX, pozycjaY, "lewo", "/resource/stonka.png");
            }
            sprawdzPrzegrana(gracz, wrogStonka);
        }

        internal void przesunZdjecie(ObiektGry obiekt, int pozycjaX, int pozycjaY, string kierunek, string sciezkaZdjecia)
        {
            switch (kierunek)
            {
                case "gora":
                    usunZdjecie(pozycjaX, pozycjaY);

                    foreach (var pole in plansza1.getPolaPlanszy())
                    {
                        var (x, y) = ((int, int))pole.Tag;

                        if (pozycjaX == x && pozycjaY - 1 == y)
                        {
                            if(obiekt is Gracz)
                            {
                                zbierzKwiatka(pole);
                            }
                            
                            pole.Source = new BitmapImage(new Uri(sciezkaZdjecia, UriKind.Relative));
                            obiekt.zmniejszY();
                        }
                    }
                break;

                case "dol":
                    usunZdjecie(pozycjaX, pozycjaY);

                    foreach (var pole in plansza1.getPolaPlanszy())
                    {
                        var (x, y) = ((int, int))pole.Tag;

                        if (pozycjaX == x && pozycjaY + 1 == y)
                        {
                            if(obiekt is Gracz)
                            {
                                zbierzKwiatka(pole);
                            }
                            
                            pole.Source = new BitmapImage(new Uri(sciezkaZdjecia, UriKind.Relative));
                            obiekt.zwiekszY();
                        }
                    }
                break;

                case "lewo":
                    usunZdjecie(pozycjaX, pozycjaY);

                    foreach (var pole in plansza1.getPolaPlanszy())
                    {
                        var (x, y) = ((int, int))pole.Tag;

                        if (pozycjaX - 1 == x && pozycjaY == y)
                        {
                            if (obiekt is Gracz)
                            {
                                zbierzKwiatka(pole);
                            }

                            pole.Source = new BitmapImage(new Uri(sciezkaZdjecia, UriKind.Relative));
                            obiekt.zmniejszX();
                        }
                    }
                break;

                case "prawo":
                    usunZdjecie(pozycjaX, pozycjaY);

                    foreach (var pole in plansza1.getPolaPlanszy())
                    {
                        var (x, y) = ((int, int))pole.Tag;

                        if (pozycjaX + 1 == x && pozycjaY == y)
                        {
                            if (obiekt is Gracz)
                            {
                                zbierzKwiatka(pole);
                            }

                            pole.Source = new BitmapImage(new Uri(sciezkaZdjecia, UriKind.Relative));
                            obiekt.zwiekszX();
                        }
                    }
                break;
            }

        }

        public void krecikPoruszanie()
        {
            int krecikX = wrogKrecik.getPozycjaX();
            int krecikY = wrogKrecik.getPozycjaY();

            int graczX = gracz.getPozycjaX();
            int graczY = gracz.getPozycjaY();

            if (graczX == krecikX && graczY < krecikY && sprawdzaniePrzeszkody(wrogKrecik, 0, -1) == false) 
            {
                przesunZdjecie(wrogKrecik, krecikX, krecikY, "gora", "/resource/krecik.png");
            }
            else if (graczX == krecikX && graczY > krecikY && sprawdzaniePrzeszkody(wrogKrecik, 0, 1) == false) 
            {
                przesunZdjecie(wrogKrecik, krecikX, krecikY, "dol", "/resource/krecik.png");
            }
            else if (graczY == krecikY && graczX < krecikX && sprawdzaniePrzeszkody(wrogKrecik, -1, 0) == false) 
            {
                przesunZdjecie(wrogKrecik, krecikX, krecikY, "lewo", "/resource/krecik.png");
            }
            else if (graczY == krecikY && graczX > krecikX && sprawdzaniePrzeszkody(wrogKrecik, 1, 0) == false) 
            {
                przesunZdjecie(wrogKrecik, krecikX, krecikY, "prawo", "/resource/krecik.png");
            }
            sprawdzPrzegrana(gracz, wrogKrecik);
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

        internal bool sprawdzaniePrzeszkody(ObiektGry obiekt, int przesuniecieX, int przesuniecieY)
        {
            int pozycjaX = obiekt.getPozycjaX();
            int pozycjaY = obiekt.getPozycjaY();

            foreach (var pole in plansza1.getPolaPlanszy()) //sprawdzanie czy w poblizu nie stoi wrog albo kwiatek
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX + przesuniecieX == x && pozycjaY + przesuniecieY == y) //czy pole sie zgadza
                {
                    if(pole.Source != null && sprawdzanieKwiatka(pole) == false)
                    {
                        return true;
                    }
                    else if(sprawdzanieKwiatka(pole) == true && obiekt is Gracz) //czy tam jest kwiatek, jesli tak to mozna wejsc na pole
                    {
                        return false;
                    }
                    else if (sprawdzanieKwiatka(pole) == true && obiekt is Wrog) //wrog nie moze wejsc na pole z kwiatkiem
                    {
                        return true;
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
                
                return uri.Contains("kwiatek.png") || uri.Contains("kwiatek2.png") || uri.Contains("kwiatek3.png");
            }
            return false;
        }

        public void zbierzKwiatka(Image pole) //ta funckja sprawdza czy pole na ktore wchodzisz jest kwiatkiem, jesli tak to go zbiera
        {
            if (pole.Source is BitmapImage bmp && bmp.UriSource != null)
            {
                string uri = bmp.UriSource.ToString();
                
                if(uri.Contains("kwiatek.png"))
                {
                    gracz.zebranoKwiatek();
                    wskaznikKwiatkow.Content = "Kwiatki: " + gracz.getZebraneKwiatki().ToString() + "/3";
                    kwiatek1.setPozycjaX(-1);
                    kwiatek1.setPozycjaY(-1);
                }
                else if(uri.Contains("kwiatek2.png"))
                {
                    gracz.zebranoKwiatek();
                    wskaznikKwiatkow.Content = "Kwiatki: " + gracz.getZebraneKwiatki().ToString() + "/3";
                    kwiatek2.setPozycjaX(-1);
                    kwiatek2.setPozycjaY(-1);
                }
                else if(uri.Contains("kwiatek3.png"))
                {
                    gracz.zebranoKwiatek();
                    wskaznikKwiatkow.Content = "Kwiatki: " + gracz.getZebraneKwiatki().ToString() + "/3";
                    kwiatek3.setPozycjaX(-1);
                    kwiatek3.setPozycjaY(-1);
                }
            }
        }

        internal void sprawdzPrzegrana(Gracz gracz, Wrog wrog)
        {
            if(gracz.getHp() <= 0)
            {
                MessageBox.Show("Przegrałeś!");
                NavigationService.Navigate(new Uri("strony/menuGlowne.xaml", UriKind.Relative));
            }

            if(gracz.getPozycjaX() == wrog.getPozycjaX() && gracz.getPozycjaY() == wrog.getPozycjaY())
            {
                MessageBox.Show("Przegrałeś!");
                NavigationService.Navigate(new Uri("strony/menuGlowne.xaml", UriKind.Relative));
            }
        }


        public void ruchyPrzeciwnikow()
        {
            gracz.zwiekszTure();
            wskaznikTury.Content = "Tura: " + gracz.getTura().ToString();
            stonkaPoruszanie();
            krecikPoruszanie();
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
