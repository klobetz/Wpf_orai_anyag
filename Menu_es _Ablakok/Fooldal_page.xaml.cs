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
    /// Interaction logic for Fooldal_page.xaml
    /// </summary>
    public partial class Fooldal_page : Page
    {
        public Fooldal_page()
        {
            InitializeComponent();
        }
        public void Fajlnyitas()
        {
            var fajlmegnyitas = new OpenFileDialog();
            //var falkivalasztas = fajlmegnyitas.ShowDialog();

            if (fajlmegnyitas.ShowDialog() == true)
            {
                using (var fs = new FileStream(fajlmegnyitas.FileName, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        TextBoxMegjelenes.Text = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva fájl");
            }
        }
    }
}
