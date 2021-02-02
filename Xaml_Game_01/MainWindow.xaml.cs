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
        public List<FontAwesomeIcon> kartyaPakli { get; private set; }
        public Random dobokocka { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

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
            NeValasz();

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

        private void NeValasz()
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
        }

        private void RosszValasz()
        {
            BalKartya.Icon = FontAwesomeIcon.Times;
        }

        private void UjKartyaHuzasa()
        {
            
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
