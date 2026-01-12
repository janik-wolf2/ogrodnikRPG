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
        }
        Gracz gracz = new Gracz();
        Plansza plansza1 = new Plansza(3, 3);

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

        public void idzGora()
        {
            if(gracz.getPozycjaY() == 0)
            {
                MessageBox.Show("Nie mozesz isc dalej w gore!");
                return;
            }

            znajdzPoleGracza();

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX == x && pozycjaY - 1 == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zmniejszY();
                }
            }
        }

        public void idzDol()
        {
            if (gracz.getPozycjaY() == 7)
            {
                MessageBox.Show("Nie mozesz isc dalej w dol!");
                return;
            }

            znajdzPoleGracza();

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX == x && pozycjaY + 1 == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zwiekszY();
                }
            }
        }

        public void idzLewo()
        {
            if (gracz.getPozycjaX() == 0)
            {
                MessageBox.Show("Nie mozesz isc dalej w lewo!");
                return;
            }

            znajdzPoleGracza();

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX - 1 == x && pozycjaY == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zmniejszX();
                }
            }
        }

        public void idzPrawo()
        {
            if (gracz.getPozycjaX() == 9)
            {
                MessageBox.Show("Nie mozesz isc dalej w prawo!");
                return;
            }

            znajdzPoleGracza();

            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();
            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX + 1 == x && pozycjaY == y)
                {
                    pole.Source = new BitmapImage(new Uri("/resource/ninja2.png", UriKind.Relative));
                    gracz.zwiekszX();
                }
            }

        }

        public void znajdzPoleGracza()
        {
            int pozycjaX = gracz.getPozycjaX();
            int pozycjaY = gracz.getPozycjaY();

            foreach (var pole in plansza1.getPolaPlanszy())
            {
                var (x, y) = ((int, int))pole.Tag;

                if (pozycjaX == x && pozycjaY == y)
                {
                    pole.Source = null;
                }
            }
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
