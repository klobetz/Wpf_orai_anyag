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
using System.Windows.Shapes;

namespace Menu_es__Ablakok
{
    /// <summary>
    /// Interaction logic for Ujablak_window.xaml
    /// </summary>
    public partial class Ujablak_window : Window
    {
        public Ujablak_window()
        {
            InitializeComponent();
        }

        private void ButtonBezar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Close(); //így is jó nem fontos a (this)

        }
    }
}
