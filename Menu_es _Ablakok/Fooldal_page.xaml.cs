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

        private void TallozasGomb_Click(object sender, RoutedEventArgs e)
        {
            Fajlnyitas();
        }

        private void MemtesGomb_Click(object sender, RoutedEventArgs e)
        {
            FajlMentes();
        }

        //fájl kimentése
        private void FajlMentes()
        {
            var fajlmentes = new SaveFileDialog() { Filter = "Text fájlok (.txt)|*.txt" };

            if (fajlmentes.ShowDialog() == true)
            {//mentés folyamata
                using (var fs = new FileStream(fajlmentes.FileName,FileMode.Create))
                {
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(TextBoxMegjelenes.Text);
                    }
                }
            }
            else
            {//figyelmeztetés
                MessageBox.Show("Nem lett mentve az állományt!");
            }
        }


        //fájl beolvasás
        public void Fajlnyitas()
        {
            var fajlmegnyitas = new OpenFileDialog() { Filter = "Text fájlok (.txt)|*.txt" };
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
