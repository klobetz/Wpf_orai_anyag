using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            TallozasPipaBe(false);

        }

        private void ButtonFooldal_Click(object sender, RoutedEventArgs e)
        {
            Tartalom.Content = new Fooldal_page();
        }

        private void ButtonAblak1_Click(object sender, RoutedEventArgs e)
        {
            Tartalom.Content = new Ablak1_page();

        }

        private void ButtonAblak2_Click(object sender, RoutedEventArgs e)
        {
            Tartalom.Content = new Ablak2_page();
        }

        private void ButtonUjablak_Click(object sender, RoutedEventArgs e)
        {
            var ujablak = new Ujablak_window();
            ujablak.ShowDialog();
        }

        private void ButtonTallozas_Click(object sender, RoutedEventArgs e)
        {
            //if (Tartalom.Content is Fooldal_page)
            //{
            //    var fooldal = (Fooldal_page)Tartalom.Content;
            //    fooldal.Fajlnyitas();
            //}
            //else
            //{
            //    MessageBox.Show("nem ott álsz! Menj a főoldalra");
            //}

            var tallozas = new Fooldal_page();
            tallozas.Fajlnyitas();
            Tartalom.Content = tallozas;
            
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Tartalom.Content = new Fooldal_page();
        }

        
        private void TallozasAktiv_Checked(object sender, RoutedEventArgs e)
        {
            TallozasPipaBe(true);
        }

       

        private void TallozasAktiv_Unchecked(object sender, RoutedEventArgs e)
        {
            TallozasPipaBe(false);
        }

        private void TallozasPipaBe(bool habevanpipalva)
        {
            if (habevanpipalva)
            {
                ButtonTallozas.IsEnabled = true;
            }
            else
            {
                ButtonTallozas.IsEnabled = false;
            }
            
        }

    }
}
