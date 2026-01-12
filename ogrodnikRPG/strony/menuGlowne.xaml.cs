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

namespace ogrodnikRPG.strony
{
    /// <summary>
    /// Logika interakcji dla klasy menuGlowne.xaml
    /// </summary>
    public partial class menuGlowne : Page
    {
        public menuGlowne()
        {
            InitializeComponent();
        }

        public void showJakGrac(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("strony/pomoc.xaml", UriKind.Relative));
        }

        public void showPrzeciwnicy(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("strony/przeciwnicy.xaml", UriKind.Relative));
        }

        public void wyjdzZGry(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void showPlanszaGry(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("strony/planszaGry.xaml", UriKind.Relative));
        }
    }
}
