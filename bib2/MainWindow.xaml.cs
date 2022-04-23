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
using System.IO;
using System.Collections.ObjectModel;

namespace bib
{
    public class ksiazka
    {
        public ksiazka() { }
        public ksiazka(string ida, string tytull, string autorr, string rokk, string przeczytanaa)
        {
            Id = ida;
            Tytul = tytull;
            Autor = autorr;
            Rok = rokk;
            Przeczytana = przeczytanaa;
        }
        public string Id { get; set; }
        public string Autor { get; set; }
        public string Tytul { get; set; }
        public string Rok { get; set; }
        public string Przeczytana { get; set; }

    }


    public partial class MainWindow : Window
    {
        public int[] usuniente= new int[20000];
        
        public static int licz = 1;
        public int zapisane = 1;
        public string id = "";
        public string autor = "a";
        public string tytul = "";
        public string rok = "";
        public string przeczytana = "";
        string path1 = @"biblioteczka1.txt";

        public ksiazka[] books = new ksiazka[200];

        public static MainWindow instance;

        public void zapisz( ksiazka book)
        {
            //books[licz].Id = book.Id;
            //books[licz].Tytul = book.Tytul;
            //books[licz].Autor = book.Autor;
            //books[licz].Rok = book.Rok;
            //books[licz].Przeczytana = book.Przeczytana;

            books[licz] = new ksiazka(book.Id, book.Tytul, book.Autor, book.Rok, book.Przeczytana);

            MainDataGrid.Items.Add(books[licz]);
            licz++;
            zapisane = 0;
        }


        public void modd(ksiazka book, int x)
        {
            books[x].Tytul = book.Tytul;
            books[x].Autor = book.Autor;
            books[x].Rok = book.Rok;
            books[x].Przeczytana = book.Przeczytana;

            zapiszdo2();
            MainDataGrid.Items.Clear();
            licz = 1;
            wczytaj2(books);

            zapisane = 0;
        }


        public void zapiszdo2()
        {
            using (StreamWriter writetext = new StreamWriter(path1))
            {
                for (int x = 1; x < licz; x++)
                {
                    writetext.WriteLine(books[x].Id);
                    writetext.WriteLine(books[x].Tytul);
                    writetext.WriteLine(books[x].Autor);
                    writetext.WriteLine(books[x].Rok);
                    writetext.WriteLine(books[x].Przeczytana);
                }
                writetext.WriteLine("");
            }
            zapisane = 1;
        }

        public void zapiszdo()
        {
            using (StreamWriter writetext = new StreamWriter(@"biblioteczka.txt"))
            {
                for (int x = 1; x < licz; x++)
                {
                    writetext.WriteLine(books[x].Id);
                    writetext.WriteLine(books[x].Tytul);
                    writetext.WriteLine(books[x].Autor);
                    writetext.WriteLine(books[x].Rok);
                    writetext.WriteLine(books[x].Przeczytana);
                }
                writetext.WriteLine("");
            }
            zapisane = 1;
        }

        public void wczytaj2(ksiazka[] books)
        {

            int i = 1;
            foreach (string line in File.ReadLines(@"biblioteczka1.txt"))
            {
                if (i == 1) id = line;
                else if (i == 2) tytul = line;
                else if (i == 3) autor = line;
                else if (i == 4) rok = line;
                else if (i == 5)
                {
                    przeczytana = line;
                    books[licz] = new ksiazka(id, tytul, autor, rok, przeczytana);
                    MainDataGrid.Items.Add(books[licz]);
                    i = 0;
                    licz++;
                }
                i++;
            }
        }

        public void wczytaj(ksiazka[] books)
        {

            int i = 1;
            foreach (string line in File.ReadLines(@"biblioteczka.txt"))
            {
                if (i == 1) id = line;
                else if (i == 2) tytul = line;
                else if (i == 3) autor = line;
                else if (i == 4) rok = line;
                else if (i == 5)
                {
                    przeczytana = line;
                    i = 0;

                    books[licz] = new ksiazka(id, tytul, autor, rok, przeczytana);
                    MainDataGrid.Items.Add(books[licz]);
                    
                    licz++;  
                }
                i++;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            instance = this;
            usuniente[0] = 0;

            wczytaj(books);
            zapiszdo2();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2(MainDataGrid);
 
            win2.Show();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            zapiszdo();
        }

        private void usun(object sender, RoutedEventArgs e)
        {
            ksiazka mu = MainDataGrid.SelectedItem as ksiazka;
            MainDataGrid.Items.Remove(mu);
            
            zapisane = 0;

            using (StreamWriter writetext = new StreamWriter(@"biblioteczka1.txt"))
            {
                int k = 1;
                for (int x = 1; x < licz; x++)
                {
                    if(x.ToString() != mu.Id)
                    {
                        writetext.WriteLine(k);
                        writetext.WriteLine(books[x].Tytul);
                        writetext.WriteLine(books[x].Autor);
                        writetext.WriteLine(books[x].Rok);
                        writetext.WriteLine(books[x].Przeczytana);
                    }
                    else
                    {
                        k--;
                    }
                    k++;
                }
                
                writetext.WriteLine("");
            }
            MainDataGrid.Items.Clear();
            licz = 1;
            wczytaj2(books);
        }

        private void PackIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if(zapisane == 0)
            {
                Window3 win3 = new Window3();

                win3.Show();
            }
            else if(zapisane == 1)
            {
                this.Close();
            }
            
        }

        private void MainDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            
            ksiazka mumu = e.Row.Item as ksiazka;
            if(mumu.Przeczytana == "true")
            {
                e.Row.Background = new SolidColorBrush(Colors.LightGreen);
            }
            
        }



        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            ksiazka mumu = (ksiazka)MainDataGrid.SelectedItem;
            int k = Int32.Parse(mumu.Id);
            if(books[k].Przeczytana == "true")
            {
                books[k].Przeczytana = "false";
                zapiszdo2();
                MainDataGrid.Items.Clear();
                licz = 1;
                wczytaj2(books);
            }
            else if(books[k].Przeczytana == "false")
            {
                books[k].Przeczytana = "true";
                zapiszdo2();
                MainDataGrid.Items.Clear();
                licz = 1;
                wczytaj2(books);
            }

        }

        private void Modyfikuj(object sender, RoutedEventArgs e)
        {
            ksiazka mu = MainDataGrid.SelectedItem as ksiazka;
            Window2 win2 = new Window2(MainDataGrid, mu);

            win2.Show();
        }
    }
}
