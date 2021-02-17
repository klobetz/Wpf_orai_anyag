using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
        public DispatcherTimer ingaora { get; private set; }
        public int pontszam { get; private set; }
        public TimeSpan jatekido { get; private set; }
        public Stopwatch stopperora { get; }
        public DispatcherTimer aktualisido { get; private set; }
        public List<long> listaRekcioIdohoz { get; private set; }
        public Action<object, EventArgs> pillIdo { get; private set; }
        public List<int> listaTop5Eredmeny { get; private set; }
        public string toplistaFajlen { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            //csak az alkalmazás indításaokr kell lefutnia

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

            //ingaóra létrehozása: feladata a hosszú távú időmérés
            //lesz eyg eseményem ami ilyenkor megtörténi és nekem erre fel kell tudnom íratkozni

            ingaora = new DispatcherTimer           //új obijektum ot hozok létre
                (TimeSpan.FromSeconds(1)            //az esemény másodpercenként
                , DispatcherPriority.Normal          //az üzenet szétszórása annak a naormál beállítása
                , Orautes                            //eseményvezérló amit az ingaóra meghív
                , Application.Current.Dispatcher     //mi az ami ezeket az eseményeket szétszórja beépített...
                );

            //az ingaóra megállítása mert különben egyből eindul a program indulásakor
            ingaora.Stop();

            //stopper létrehozása
            stopperora = new Stopwatch();
            /*********************************************************************************************/

            //Az aktuális idő kiíratása
            aktualisido = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, Ido, Application.Current.Dispatcher);
            aktualisido.Start();
            
            JatekKezdoAllapota();

            //lista készítése az eredmények eltárolására
            listaTop5Eredmeny = new List<int>();
            toplistaFajlen = "Toplista.txt";

            //vizsgálom, hogy létezik-e a fájl
            if (File.Exists(toplistaFajlen))
            {//ha van fájl akkor beolvasom a listába ami fent létrejön, különben üres listával indul a program
                using (var fs = new FileStream(toplistaFajlen, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        while (!sr.EndOfStream)
                        {
                            var sor = sr.ReadLine();
                            listaTop5Eredmeny.Add(Convert.ToInt32(sor));
                        }
                    }
                }
            }
            
            Top5ListaKiiratasa();
        }
        private void Ido(object sender, EventArgs e)
        {
            pillIdo += Ido;
            //kiíratoma megfelelő heyre a játékba
            LabelPontosIdo.Content = DateTime.Now.ToLongTimeString();
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
        private void UjraInditasGomb_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Ujraindítás gomb");
            JatekKezdoAllapota();
        }

        //függvények

        private void JatekKezdoAllapota()
        {
            //indítás gomb megjelenítése
            InditasGomb.Visibility = Visibility.Visible;

            //újraindítas gom eltüntetése
            UjraInditasGomb.Visibility = Visibility.Hidden;

            //minden játék újra indításaok kell
            pontszam = 0;
            PontszamKiiras();

            //jatékidő lenullázása/3-ta állítom
            jatekido = TimeSpan.FromSeconds(3);
            JatekIdoKiiras();

            //indítás gomb engedélyezése
            InditasGomb.IsEnabled = true;

            //igen/nem gomb letiltása
            IgenGomb.IsEnabled = false;
            NemGomb.IsEnabled = false;

            //lista készítése a reakció idő tárolására
            listaRekcioIdohoz = new List<long>();
            ReakcioIdoKiiras(0,0);

            UjKartyaHuzasa();
        }
        private void JatekVegeAllapot()
        {
            //ingaóra megállítsa
            ingaora.Stop();

            //igen/nem gomb letiltása
            IgenGomb.IsEnabled = false;
            NemGomb.IsEnabled = false;

            //indítás gomb eltüntetése 
            InditasGomb.Visibility = Visibility.Hidden;

            //újraindítas gomb megjelenítése
            UjraInditasGomb.Visibility = Visibility.Visible;

            //Hozzáadom a listához a pontszámomat
            listaTop5Eredmeny.Add(pontszam);

            //jelen állapot szerint több elem is belefér a listába ez javítjuk
            if (listaTop5Eredmeny.Count > 5)
            {//a listát sorba rendezem és törlöm az első (0. azaz a legkisebb eredményt)
                //sorba rendezem
                listaTop5Eredmeny.Sort();
                //itt meg törlöm
                //listaTop5Eredmeny.Remove(listaTop5Eredmeny[0]); //ez is jó 
                listaTop5Eredmeny.RemoveAt(0); //és ez is jó
            }

            Top5ListaKiiratasa();

            //a lista tartalmának a kiíratása fájlba
            using (var fs = new FileStream(toplistaFajlen, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (var item in listaTop5Eredmeny)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
        }
        private void Top5ListaKiiratasa()
        {
            //kiíratása a képernyőre
            //Ez nem az igazi megoldás mert csak az első elemet jeleníti meg a listából ListBoxTop5.ItemsSource = listaTop5Eredmeny;

            //Az ObservableCollection<int> szorosan össze va fűzve a ListBox-al szért szól neki, hogy frissitse a listát
            //ListBoxTop5.ItemsSource = new ObservableCollection<int>(listaTop5Eredmeny);

            //A listámat csökkenő sorrendben íratom ki!
            ListBoxTop5.ItemsSource = new ObservableCollection<int>(listaTop5Eredmeny.OrderByDescending(eredmény => eredmény));
        }
        /// <summary>
        /// itt tudjuk a játékidőt elkészíteni
        /// ezt a függgvényt hívja az ingaóra másodpercenként
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orautes(object sender, EventArgs e)
        {
            jatekido -= TimeSpan.FromSeconds(1); //ebbe a változóba gyűjtjük a másodperceket
            
            //a játékidő kiíratása függvénnyel
            JatekIdoKiiras();

            if (jatekido <= TimeSpan.FromSeconds(0))
            {
                JatekVegeAllapot();
            }
        }
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

            //ingaóra elindítása az indítás gobra
            ingaora.Start();      

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

            //a pontszám kiíratása függvény segítségével
            PontszamKiiras();

            //Hozzáadom a listához a mért reakció időket
            listaRekcioIdohoz.Add(stopperora.ElapsedMilliseconds);

            //reakció idő kiíratása így ha nincs még listám
            //LabelReacioIdo.Content = stopperora.ElapsedMilliseconds;
            ReakcioIdoKiiras(listaRekcioIdohoz.Last(), (long)listaRekcioIdohoz.Average());

        }
        //Kiíratások a kéernyőre
        private void ReakcioIdoKiiras(long reakcioido, long atlagosreakcioido)
        {
            LabelReacioIdo.Content = reakcioido;

            LabelAtlagosReacioIdo.Content = atlagosreakcioido;
        }

        private void PontszamKiiras()
        {
            LabelPont.Content = pontszam; //megoldom a kiíratást a képernyőre
        }

        private void JatekIdoKiiras()
        {
            LabelJatekido.Content = $"{jatekido.Minutes:00}:{jatekido.Seconds:00}"; //ez a kiíratás a programba
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

            //a stopper óra újraindítása
            stopperora.Restart();
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
                return;
            }

            if (e.Key == Key.Left && InditasGomb.IsEnabled == false && InditasGomb.Visibility == Visibility.Visible)
            {
                IgenValasz();
                return;
            }

            if (e.Key == Key.Right && InditasGomb.IsEnabled == false && InditasGomb.Visibility == Visibility.Visible) 
            {
                NemValasz();
                return;
            }

            if (e.Key == Key.Space && InditasGomb.IsEnabled == false && IgenGomb.IsEnabled == false)
            {
                JatekKezdoAllapota();
                return;
            }  
        }      
    }
}