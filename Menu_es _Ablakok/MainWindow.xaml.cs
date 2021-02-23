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

namespace Menu_es__Ablakok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonFooldal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAblak1_Click(object sender, RoutedEventArgs e)
        {
            Tartalom.Content = new Ablak1_page();

        }

        private void ButtonAblak2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonUjablak_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonTallozas_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
