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
    /// Logika interakcji dla klasy pomoc.xaml
    /// </summary>
    public partial class pomoc : Page
    {
        public pomoc()
        {
            InitializeComponent();
        }

        public void showMenuGlowne(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("strony/menuGlowne.xaml", UriKind.Relative));
        }

        public void showSterowanie(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("strony/sterowanie.xaml", UriKind.Relative));
        }
    }
}
