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

namespace bib
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        int gg = 0;
        string idd = "0";
        int idd2 = 0;

        public Window2(DataGrid MainDataGrid)
        {
            InitializeComponent();


        }

        public Window2(DataGrid MainDataGrid, ksiazka mumu)
        {
            InitializeComponent();

            tytul.Text = mumu.Tytul;
            autor.Text = mumu.Autor;
            rok.Text = mumu.Rok;
            if (mumu.Przeczytana == "true")
            {
                przeczytana.Text = "Przeczytana";
            }
            else if (mumu.Przeczytana == "false")
            {
                przeczytana.Text = "Nieprzeczytana";
            }

            idd2 = Int32.Parse(mumu.Id);
            idd = mumu.Id;
            gg = 1;
            zmod.Content = "Zmodyfikuj książke";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int licc = MainWindow.licz;
            //MainWindow.tytul = tytul.Text;
            //MainWindow.autor = autor.Text;
            //MainWindow.rok = rok.Text;
            string prze = "false";

            if (przeczytana.Text=="Przeczytana")
            {
                prze = "true";
            }
            else if (przeczytana.Text == "Nieprzeczytana")
            {
                prze = "false";
            }

            ksiazka book;
            int m = 0, n = 0;

            foreach (char k in rok.Text)
            {
                if(k >= '0' && k <= '9')
                {
                    blad.Visibility = Visibility.Hidden;
                }
                else
                {
                    m = 1;
                    rok.Margin = new Thickness(10,10,10,20);
                    blad.Visibility = Visibility.Visible;
                }

            }

            if(tytul.Text == "")
            {
                n = 1;
                tytul.Margin = new Thickness(10, 10, 10, 20);
                blad2.Visibility = Visibility.Visible;
            }
            else
            {
                blad2.Visibility = Visibility.Hidden;
            }




            if(m == 0 && n==0 && gg ==0)
            {
                book = new ksiazka(licc.ToString(), tytul.Text, autor.Text, rok.Text, prze);
                MainWindow.instance.zapisz(book);
                this.Close();
            }
            else if (m == 0 && n == 0 && gg == 1)
            {
                book = new ksiazka(idd, tytul.Text, autor.Text, rok.Text, prze);
                MainWindow.instance.modd(book, idd2);
                this.Close();
            }

        }


        private void PackIcon_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
