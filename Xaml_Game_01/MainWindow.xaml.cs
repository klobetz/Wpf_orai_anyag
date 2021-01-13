using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Xaml_Game_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //oszály szintű változó
        public FontAwesomeIcon elozoKartya { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            //indítás gomb engedélyezése
            InditasGomb.IsEnabled = true;
            
            //igen/nem gomb letiltása
            IgenGomb.IsEnabled = false;
            NemGomb.IsEnabled = false;

            UjKartyaHuzasa();
        }


        private void IgenGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Igen gomb");
            //JobbKartya.Icon = FontAwesome.WPF.FontAwesomeIcon.Wifi;

            //jó és rossz válasz vizsgálata
            if ( elozoKartya == JobbKartya.Icon)
            {//jó válasz
                JoValasz();
            }
            else
            {//rossz válasz
                RosszValasz();
            }

            UjKartyaHuzasa();
        }


        private void NemGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Nem gomb");
            //BalKartya.Icon = FontAwesome.WPF.FontAwesomeIcon.Ban; 


            //jó és rossz válasz vizsgálata
            if (elozoKartya != JobbKartya.Icon)
            {//jó válasz
                JoValasz();
            }
            else
            {//rossz válasz
                RosszValasz();
            }

            UjKartyaHuzasa();

        }

        private void InditasGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Indítás gomb");
            
            //indítás gomb letiltása
            InditasGomb.IsEnabled = false;

            //igen/nem gomb engedélyezése
            IgenGomb.IsEnabled = true;
            NemGomb.IsEnabled = true;

            UjKartyaHuzasa();

        }



        //függvények

        private void JoValasz()
        {
            BalKartya.Icon = FontAwesomeIcon.Check;
        }

        private void RosszValasz()
        {
            BalKartya.Icon = FontAwesomeIcon.Times;
        }

        private void UjKartyaHuzasa()
        {
            //létrehozom a listát mint kártyapakli
            var kartyaPakli = new List<FontAwesome.WPF.FontAwesomeIcon>();

            //feltöltöm a lista elemeit
            kartyaPakli.Add(FontAwesome.WPF.FontAwesomeIcon.Flag);
            kartyaPakli.Add(FontAwesome.WPF.FontAwesomeIcon.Wifi);
            kartyaPakli.Add(FontAwesome.WPF.FontAwesomeIcon.Deaf);
            kartyaPakli.Add(FontAwesome.WPF.FontAwesomeIcon.Twitter);
            kartyaPakli.Add(FontAwesome.WPF.FontAwesomeIcon.Magic);
            kartyaPakli.Add(FontAwesome.WPF.FontAwesomeIcon.Edit);

            //Készítek egy véltlen szám generátort (dobókocka)
            var dobokocka = new Random();
            var dobas = dobokocka.Next(0, kartyaPakli.Count);

            //ha ellenőrizni akarom az indexeket
            //Debug.WriteLine(dobas);


            //osztály szintű változót kell létrehozni NEM A var (variable) segítségével
            elozoKartya = JobbKartya.Icon;



            //megadom hogy hol jelenjen meg
            JobbKartya.Icon = kartyaPakli[dobas];
        }
    }
}
