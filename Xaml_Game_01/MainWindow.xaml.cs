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
using System.Windows.Media.Animation;
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
        public List<FontAwesomeIcon> kartyaPakli { get; private set; }
        public Random dobokocka { get; private set; }
        public int pontszam { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            
            pontszam = 0;

            //indítás gomb engedélyezése
            InditasGomb.IsEnabled = true;
            
            //igen/nem gomb letiltása
            IgenGomb.IsEnabled = false;
            NemGomb.IsEnabled = false;

            //létrehozom a listát mint kártyapakli
            kartyaPakli = new List<FontAwesomeIcon>();

            //feltöltöm a lista elemeit
            kartyaPakli.Add(FontAwesomeIcon.Flag);
            kartyaPakli.Add(FontAwesomeIcon.Wifi);
            kartyaPakli.Add(FontAwesomeIcon.Deaf);
            kartyaPakli.Add(FontAwesomeIcon.Twitter);
            kartyaPakli.Add(FontAwesomeIcon.Magic);
            kartyaPakli.Add(FontAwesomeIcon.Edit);

            //Készítek egy véltlen szám generátort (dobókocka)
            dobokocka = new Random();

            UjKartyaHuzasa();
        }


        private void IgenGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Igen gomb");
            //JobbKartya.Icon = FontAwesome.WPF.FontAwesomeIcon.Wifi;
            IgenValasz();
        }

        

        private void NemGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Nem gomb");
            //BalKartya.Icon = FontAwesome.WPF.FontAwesomeIcon.Ban; 
            NemValasz();

        }


        private void InditasGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Indítás gomb");
            Inditas();

        }

        



        //függvények

        private void IgenValasz()
        {
            //jó és rossz válasz vizsgálata
            if (elozoKartya == JobbKartya.Icon)
            {//jó válasz
                JoValasz();
            }
            else
            {//rossz válasz
                RosszValasz();
            }

            UjKartyaHuzasa();
        }

        private void NemValasz()
        {
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

        private void Inditas()
        {
            //indítás gomb letiltása
            InditasGomb.IsEnabled = false;

            //igen/nem gomb engedélyezése
            IgenGomb.IsEnabled = true;
            NemGomb.IsEnabled = true;

            UjKartyaHuzasa();
        }

        private void JoValasz()
        {
            BalKartya.Icon = FontAwesomeIcon.Check;
            BalKartya.Foreground = Brushes.Green;
            BalKartyaAnimacio();
            Pontszamitas(true);
        }       

        private void RosszValasz()
        {
            BalKartya.Icon = FontAwesomeIcon.Times;
            BalKartya.Foreground = Brushes.Red;
            BalKartyaAnimacio();
            Pontszamitas(false);
        }

        private void Pontszamitas(bool IgazHamisValasz)
        {
            //pontszámítás kezdete
            //pontszam += 1; //létrehozok egy osztály szintű változót

            if (IgazHamisValasz)
            {
                pontszam += 1;
            }
            else
            {
                pontszam -= 1;
            }

            LabelPont.Content = pontszam; //megoldom a kiíratást a képernyőre
        }

        private void BalKartyaAnimacio()
        {
            //animáció: egy adott értéket megváltoztatok az dő függvényében
            //feladat: a bal kártya opacity tul. megváltoztatása
            //100% - 0% rövid idő alatt

            //100% = 1-el
            //0% = 0-val
            //időt a c#-ban a TimeSpan

            var animacio = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(1200));
            BalKartya.BeginAnimation(OpacityProperty, animacio);
        }

        private void UjKartyaHuzasa()
        {           
            var dobas = dobokocka.Next(0, kartyaPakli.Count);

            //ha ellenőrizni akarom az indexeket
            //Debug.WriteLine(dobas);

            //osztály szintű változót kell létrehozni NEM A var (variable) segítségével
            elozoKartya = JobbKartya.Icon;

            //jobb kárty eltűnik
            var animacioEltunik = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            JobbKartya.BeginAnimation(OpacityProperty, animacioEltunik);

            //megadom hogy hol jelenjen meg
            JobbKartya.Icon = kartyaPakli[dobas];

            //jobb kárty megjeleni
            var animacioMegjelenik = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
            JobbKartya.BeginAnimation(OpacityProperty, animacioMegjelenik);
        }

        /// <summary>
        /// a leütés vizsgálatához sz xaml-ben létre kell hozni a KeyDown="Window_KeyDown"
        /// </summary>
        /// <param name="sender"> ez a paraméter mindig a .NET konverzióban az az obijektum ami az eseményt dobja
        ///                         ebben az esetben megkapjuk az egész window-t</param>
        /// <param name="e">ez a 2. paraméter ebben van a leütés </param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.Key);

            if (e.Key == Key.Up && InditasGomb.IsEnabled == true)
            {
                Inditas();
            }

            if (e.Key == Key.Left && InditasGomb.IsEnabled == false)
            {
                IgenValasz();
            }

            if (e.Key == Key.Right && InditasGomb.IsEnabled == false) 
            {
                NemValasz();
            }

        }
    }
}
