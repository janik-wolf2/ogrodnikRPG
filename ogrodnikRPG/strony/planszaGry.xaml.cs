using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using System.Xml.Serialization;
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
            stworzKwiatka("kwiatek3");
            stworzPrzedmiot("lopata");
            stworzPrzedmiot("malaLopatka");
        }
        Gracz gracz = new Gracz();
        Plansza plansza1 = new Plansza(3, 3);
        Random rnd = new Random();

        List<Wrog> wrogowie = new List<Wrog>();
        List<Kwiatek> kwiatki = new List<Kwiatek>();
        List<Przedmiot> przedmioty = new List<Przedmiot>();
        bool blokadaKlawiatury = false; //blokada podczas animacji

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
                        Stretch = Stretch.Fill,
                        
                    };
                    img.MouseLeftButtonDown += czyMoznaAtakowac;

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
            Image losoweWolnePole = znajdzWolnePole();
            var (x, y) = ((int, int))losoweWolnePole.Tag;

            switch (typ)
            {
                case "chwast":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/chwast.png", UriKind.Relative));
                    wrogowie.Add(new Wrog("chwast", 20, 10, x, y));
                    break;
                case "stonka":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/stonka.png", UriKind.Relative));
                    wrogowie.Add(new Stonka("stonka", 50, 15, "prawo", x, y));
                    break;
                case "krecik":
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/krecik.png", UriKind.Relative));
                    wrogowie.Add(new Krecik("krecik", 60, 25, x, y));
                    break;
            }
        }

        public void stworzKwiatka(string typ) //wybieranie losowego wolnego pola i tworzenie na nim kwiatka
        {
            Image losoweWolnePole = znajdzWolnePole();
            var (x, y) = ((int, int))losoweWolnePole.Tag;

            switch (typ)
            {
                case "kwiatek1":
                    kwiatki.Add(new Kwiatek(1, x, y));
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek.png", UriKind.Relative));
                    break;
                case "kwiatek2":
                    kwiatki.Add(new Kwiatek(2, x, y));
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek2.png", UriKind.Relative));
                    break;
                case "kwiatek3":
                    kwiatki.Add(new Kwiatek(3, x, y));
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/kwiatek3.png", UriKind.Relative));
                    break;
            }
        }

        public void stworzPrzedmiot(string typ)
        {
            Image losoweWolnePole = znajdzWolnePole();
            var (x, y) = ((int, int))losoweWolnePole.Tag;

            switch (typ)
            {
                case "lopata":
                    przedmioty.Add(new Przedmiot("lopata", 20, x, y));
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/przedmioty/lopata.png", UriKind.Relative));
                    break;
                case "malaLopatka":
                    przedmioty.Add(new Przedmiot("malaLopatka", 10, x, y));
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/przedmioty/malaLopatka.png", UriKind.Relative));
                    break;
                case "sekator":
                    przedmioty.Add(new Przedmiot("sekator", 15, x, y));
                    losoweWolnePole.Source = new BitmapImage(new Uri("/resource/przedmioty/sekator.png", UriKind.Relative));
                    break;
            }
        }

        public Image znajdzWolnePole()
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
            return losoweWolnePole;
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
                                podniesPrzedmiot(pole);
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
                                podniesPrzedmiot(pole);
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
                                podniesPrzedmiot(pole);
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
                                podniesPrzedmiot(pole);
                            }

                            pole.Source = new BitmapImage(new Uri(sciezkaZdjecia, UriKind.Relative));
                            obiekt.zwiekszX();
                        }
                    }
                break;
            }

        }
        internal void stonkaPoruszanie(Stonka wrogStonka)
        {
            int pozycjaX = wrogStonka.getPozycjaX();
            int pozycjaY = wrogStonka.getPozycjaY();
            string kierunek = wrogStonka.getKierunek();

            if ((sprawdzaniePrzeszkody(wrogStonka, 1, 0) == true || pozycjaX == 9) && kierunek == "prawo") //zmiana kierunku w lewo
            {
                wrogStonka.zmianaKierunku("lewo");
            }

            if ((sprawdzaniePrzeszkody(wrogStonka, -1, 0) == true || pozycjaX == 0) && kierunek == "lewo") //zmiana kierunku w prawo
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
            sprawdzObrazenia(gracz, wrogStonka);
        }

        internal void krecikPoruszanie(Wrog wrogKrecik)
        {
            int krecikX = wrogKrecik.getPozycjaX();
            int krecikY = wrogKrecik.getPozycjaY();

            int graczX = gracz.getPozycjaX();
            int graczY = gracz.getPozycjaY();

            if (graczX == krecikX && graczY < krecikY && sprawdzaniePrzeszkody(wrogKrecik, 0, -1) == false)  //sprwadzanie czy gracz jest w kolumnie lub rzedzie krecika
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
            sprawdzObrazenia(gracz, wrogKrecik);
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
                    if(pole.Source != null && sprawdzanieKwiatka(pole) == false && sprawdzaniePrzedmiotu(pole) == false)
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
                    else if (sprawdzaniePrzedmiotu(pole) == true && obiekt is Gracz && czyPelnyEkwipunek() == false) //czy tam jest przedmiot i czy sie zmiesci w ekwipunku
                    {
                        return false;
                    }
                    else if (sprawdzaniePrzedmiotu(pole) == true && obiekt is Wrog) //wrog nie moze wejsc na pole z przedmiotem
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public bool sprawdzanieKwiatka(Image pole)
        {
            var(x, y) = ((int, int))pole.Tag;

            foreach (var kwiatek in kwiatki)
            {
                if (kwiatek.getPozycjaX() == x && kwiatek.getPozycjaY() == y)
                {
                    return true;
                }
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
                    usunKwiatka(1);
                }
                else if(uri.Contains("kwiatek2.png"))
                {
                    gracz.zebranoKwiatek();
                    wskaznikKwiatkow.Content = "Kwiatki: " + gracz.getZebraneKwiatki().ToString() + "/3";
                    usunKwiatka(2);
                }
                else if(uri.Contains("kwiatek3.png"))
                {
                    gracz.zebranoKwiatek();
                    wskaznikKwiatkow.Content = "Kwiatki: " + gracz.getZebraneKwiatki().ToString() + "/3";
                    usunKwiatka(3);
                }
            }
        }

        public void usunKwiatka(int typ)
        {
            foreach (var kwiatek in kwiatki)
            {
                if (kwiatek.getTyp() == typ)
                {
                    kwiatek.setPozycjaX(-1);
                    kwiatek.setPozycjaY(-1);
                }
            }
        }

        public bool czyPelnyEkwipunek()
        {
            if(gracz.getEkwipunek().Count == 6)
            {
                return true;
            }

            return false;
        }

        public bool sprawdzaniePrzedmiotu(Image pole)
        {
            var (x, y) = ((int, int))pole.Tag;

            foreach (var przedmiot in przedmioty)
            {
                if (przedmiot.getPozycjaX() == x && przedmiot.getPozycjaY() == y)
                {
                    return true;
                }
            }

            return false;
        }

        public void podniesPrzedmiot(Image pole)
        {
            var(x, y) = ((int, int))pole.Tag;

            foreach(var przedmiot in przedmioty)
            {
                if(przedmiot.getPozycjaX() == x && przedmiot.getPozycjaY() == y)
                {
                    //MessageBox.Show("Podniosłeś przedmiot: " + przedmiot.getNazwa());
                    gracz.dodajDoEkwipunku(przedmiot);
                    usunPrzedmiot(przedmiot.getNazwa());
                }
            }
        }

        public void usunPrzedmiot(string nazwa)
        {
            foreach (var przedmiot in przedmioty)
            {
                if (przedmiot.getNazwa() == nazwa)
                {
                    przedmiot.setPozycjaX(-1);
                    przedmiot.setPozycjaY(-1);
                }
            }
        }

        public void aktualizujPrzedmioty() //funkcja pobiera ekwipunek i wypelnia image przedmiotami, jesli przedmiot jest aktywny to jest on na zielonym tle
        {
            List<Przedmiot> ekwipunek = gracz.getEkwipunek();
            int aktywnyPrzedmiot = gracz.getAktywnyPrzedmiot();

            for (int i = 0; i < ekwipunek.Count; i++)
            {
                string nazwaPrzedmiotu = ekwipunek[i].getNazwa();
                switch (i) 
                {
                    case 0:
                        if(aktywnyPrzedmiot == 0)
                        {
                            przedmiot0.Source = new BitmapImage(new Uri("/resource/aktywnePrzedmioty/" + nazwaPrzedmiotu + "Aktywny.png", UriKind.Relative));
                        }
                        else
                        {
                            przedmiot0.Source = new BitmapImage(new Uri("/resource/przedmioty/" + nazwaPrzedmiotu + ".png", UriKind.Relative));
                        }
                        break;

                    case 1:
                        if (aktywnyPrzedmiot == 1)
                        {
                            przedmiot1.Source = new BitmapImage(new Uri("/resource/aktywnePrzedmioty/" + nazwaPrzedmiotu + "Aktywny.png", UriKind.Relative));
                        }
                        else
                        {
                            przedmiot1.Source = new BitmapImage(new Uri("/resource/przedmioty/" + nazwaPrzedmiotu + ".png", UriKind.Relative));
                        }
                        break;

                    case 2:
                        if (aktywnyPrzedmiot == 2)
                        {
                            przedmiot2.Source = new BitmapImage(new Uri("/resource/aktywnePrzedmioty/" + nazwaPrzedmiotu + "Aktywny.png", UriKind.Relative));
                        }
                        else
                        {
                            przedmiot2.Source = new BitmapImage(new Uri("/resource/przedmioty/" + nazwaPrzedmiotu + ".png", UriKind.Relative));
                        }
                        break;

                    case 3:
                        if (aktywnyPrzedmiot == 3)
                        {
                            przedmiot3.Source = new BitmapImage(new Uri("/resource/aktywnePrzedmioty/" + nazwaPrzedmiotu + "Aktywny.png", UriKind.Relative));
                        }
                        else
                        {
                            przedmiot3.Source = new BitmapImage(new Uri("/resource/przedmioty/" + nazwaPrzedmiotu + ".png", UriKind.Relative));
                        }
                        break;

                    case 4:
                        if (aktywnyPrzedmiot == 4)
                        {
                            przedmiot4.Source = new BitmapImage(new Uri("/resource/aktywnePrzedmioty/" + nazwaPrzedmiotu + "Aktywny.png", UriKind.Relative));
                        }
                        else
                        {
                            przedmiot4.Source = new BitmapImage(new Uri("/resource/przedmioty/" + nazwaPrzedmiotu + ".png", UriKind.Relative));
                        }
                        break;

                    case 5:
                        if (aktywnyPrzedmiot == 5)
                        {
                            przedmiot5.Source = new BitmapImage(new Uri("/resource/aktywnePrzedmioty/" + nazwaPrzedmiotu + "Aktywny.png", UriKind.Relative));
                        }
                        else
                        {
                            przedmiot5.Source = new BitmapImage(new Uri("/resource/przedmioty/" + nazwaPrzedmiotu + ".png", UriKind.Relative));
                        }
                        break;
                }

            }
        }


        internal async Task sprawdzObrazenia(Gracz gracz, Wrog wrog) //funckja sprawdza czy na okolo gracz jest wrog, jesli tak to odejmuje hp
        {
            int HpGracza = gracz.getHp();
            int graczX = gracz.getPozycjaX();
            int graczY = gracz.getPozycjaY();

            int wrogObraznienia = wrog.getObrazenia();
            int wrogX = wrog.getPozycjaX();
            int wrogY = wrog.getPozycjaY();

            Image zdjecieZgraczem = null;

            foreach (var pole in plansza1.getPolaPlanszy()) //szukanie pola na ktorym jest gracz zeby bylo wiadomo gdzie zrobic animacje obrazen
            {
                var (x, y) = ((int, int))pole.Tag;

                if (graczX == x && graczY == y)
                {
                    zdjecieZgraczem = pole;
                }
            }

            for (int i = graczX - 1; i < graczX + 2; i++)
            {
                for (int j = graczY - 1; j < graczY + 2; j++)
                {
                    if (i == wrogX && j == wrogY)
                    {
                        gracz.zmniejszHp(wrogObraznienia);

                        
                        blokadaKlawiatury = true;
                        zdjecieZgraczem.Source = new BitmapImage(new Uri("/resource/ninja2Obrazenia.png", UriKind.Relative));
                        await Task.Delay(500);
                        zdjecieZgraczem.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                        blokadaKlawiatury = false;
                    }
                }
            }

            gracz.zmniejszHp(-100); //god mode xd

            if (HpGracza < 100 && HpGracza > 75)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp4.png", UriKind.Relative));
            }
            else if(HpGracza == 75)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp75.png", UriKind.Relative));
            }
            else if(HpGracza < 75 && HpGracza > 50)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp3.png", UriKind.Relative));
            }
            else if(HpGracza == 50)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp50.png", UriKind.Relative));
            }
            else if(HpGracza < 50 && HpGracza > 25)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp2.png", UriKind.Relative));
            }
            else if(HpGracza == 25)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp25.png", UriKind.Relative));
            }
            else if(HpGracza < 25 && HpGracza > 0)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp1.png", UriKind.Relative));
            }
            else if(HpGracza <= 0)
            {
                wskaznikHp.Source = new BitmapImage(new Uri("/resource/hp0.png", UriKind.Relative));
                MessageBox.Show("Przegrałeś!");
                NavigationService.Navigate(new Uri("strony/menuGlowne.xaml", UriKind.Relative));
            }
        }

        public async Task atakuj(Image pole, int x, int y)
        {
            List<Przedmiot> ekwipunek = gracz.getEkwipunek();

            int aktywnyPrzedmiot = gracz.getAktywnyPrzedmiot();
            int zadawaneObrazenia = ekwipunek[aktywnyPrzedmiot].getObrazenia();
            string nazwaPrzedmiotu = ekwipunek[aktywnyPrzedmiot].getNazwa();

            foreach (var wrog in wrogowie)
            {
                if(wrog.getPozycjaX() == x && wrog.getPozycjaY() == y) //znajdujemy wroga na ktorego kliknelismy
                {
                    wrog.zmniejszHp(zadawaneObrazenia);

                    MessageBox.Show("zadano tyle obrazen:" + zadawaneObrazenia);
                    pole.Source = new BitmapImage(new Uri("/resource/" + wrog.getNazwa() + "Obrazenia.png", UriKind.Relative));
                    await Task.Delay(500);
                    pole.Source = new BitmapImage(new Uri("/resource/" + wrog.getNazwa() + ".png", UriKind.Relative));
                }
            }
            ruchyPrzeciwnikow();
        }

        public async void czyMoznaAtakowac(object sender, MouseButtonEventArgs e)
        {
            List<Przedmiot> ekwipunek = gracz.getEkwipunek();
            bool czyPusty = !ekwipunek.Any();
            if (czyPusty)
            {
                return;
            }

            int graczX = gracz.getPozycjaX();
            int graczY = gracz.getPozycjaY();

            Image pole = (Image)sender;
            var (x, y) = ((int x, int y))pole.Tag;

            bool wykrytoWroga = false;
            
            foreach(var wrog in wrogowie)
            {
                if(wrog.getPozycjaX() == x && wrog.getPozycjaY() == y)
                {
                    wykrytoWroga = true;
                }
            }

            if (wykrytoWroga == false)
            {
                return;
            }

            for (int i = graczX - 2; i < graczX + 3; i++)
            {
                for (int j = graczY - 2; j < graczY + 3; j++)
                {
                    if (i == x && j == y)
                    {
                        await atakuj(pole, i, j);
                    }
                }
            }
        }

        public void zmienKursor()
        {
            int graczX = gracz.getPozycjaX();
            int graczY = gracz.getPozycjaY();
            

            foreach(var wrog in wrogowie)
            {
                int wrogX = wrog.getPozycjaX();
                int wrogY = wrog.getPozycjaY();

                for (int i = graczX - 2; i < graczX + 3; i++)
                {
                    for (int j = graczY - 2; j < graczY + 3; j++)
                    {
                        if (i == wrogX && j == wrogY)
                        {
                            
                        }
                    }
                }
            }

            
        }


        public async void ruchyPrzeciwnikow()
        {
            gracz.zwiekszTure();
            wskaznikTury.Content = "Tura: " + gracz.getTura().ToString();
            aktualizujPrzedmioty();

            foreach(var wrog in wrogowie) //poruszanie sie wrogow chwast sie nie rusza 
            {
                if(wrog is Stonka stonka)
                {
                    stonkaPoruszanie(stonka);
                }
                else if(wrog is Krecik krecik)
                {
                    krecikPoruszanie(krecik);
                }
                else
                {
                    await sprawdzObrazenia(gracz, wrog);
                }
            }
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (blokadaKlawiatury) return;

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

                case Key.D1:
                    gracz.setAktywnyPrzedmiot(0);
                    aktualizujPrzedmioty(); break;

                case Key.D2:
                    gracz.setAktywnyPrzedmiot(1);
                    aktualizujPrzedmioty(); break;

                case Key.D3:
                    gracz.setAktywnyPrzedmiot(2);
                    aktualizujPrzedmioty(); break;

                case Key.D4:
                    gracz.setAktywnyPrzedmiot(3);
                    aktualizujPrzedmioty(); break;

                case Key.D5:
                    gracz.setAktywnyPrzedmiot(4);
                    aktualizujPrzedmioty(); break;

                case Key.D6:
                    gracz.setAktywnyPrzedmiot(5);
                    aktualizujPrzedmioty(); break;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }
    }
}
