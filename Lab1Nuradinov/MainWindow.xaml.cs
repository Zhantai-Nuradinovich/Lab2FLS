using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
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
using LinqToExcel;

namespace Lab1Nuradinov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<Threat> thrlist = new List<Threat>();
        private static ThreatPage page;
        public static ContentControl contentControl = new ContentControl();
        private static int pageNumber = 1;
        private static bool firstOrLastPage = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string pathToExcelFile = "D:/thrlist.xlsx";
            ParseExcel(pathToExcelFile);

            data.ItemsSource = thrlist.GetRange((pageNumber-1)*20, 20);
            PageNumber.Text = pageNumber.ToString();
        }

        private static void ParseExcel(string pathToExcelFile)
        {
            try
            {
                using (ExcelQueryFactory ConxObject = new ExcelQueryFactory(pathToExcelFile))
                {
                    ConxObject.AddMapping("Id", "Идентификатор УБИ");
                    ConxObject.AddMapping("Name", "Наименование УБИ");
                    ConxObject.AddMapping("Description", "Описание");
                    ConxObject.AddMapping("ThreatSource", "Источник угрозы (характеристика и потенциал нарушителя)");
                    ConxObject.AddMapping("ObjectOfInfluence", "Объект воздействия");
                    ConxObject.AddMapping("ConfidentialityViolation", "Нарушение конфиденциальности");
                    ConxObject.AddMapping("IntegrityViolation", "Нарушение целостности");
                    ConxObject.AddMapping("AccessViolation", "Нарушение доступности");

                    var list = from a in ConxObject.WorksheetRange<Threat>("A2", "J250", "Sheet")
                               select a;

                    thrlist.AddRange(list);
                }
            }
            catch (OleDbException)
            {
                string messageBoxText = "Хотите скачать файл из сети?";
                string caption = "Файл не найден!!";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        WebClient wc = new WebClient();
                        string url = "http://bdu.fstec.ru/files/documents/thrlist.xlsx";
                        string savePath = "D:/";
                        string name = "thrlist.xlsx";
                        wc.DownloadFile(url, savePath + name);

                        ParseExcel(pathToExcelFile);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

        }
        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentControl.Content = this.Content;
            Threat check = e.AddedItems.Count != 0 ? (Threat)e.AddedItems[0] : null;
            page = new ThreatPage(check, thrlist);
            if(check != null)
            {
                this.Content = page;
            }
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            if(pageNumber* 20 + 20 < thrlist.Count)
            {
                pageNumber++;
                data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, 20);
                PageNumber.Text = pageNumber.ToString();
                firstOrLastPage = true;
            }
            else if(firstOrLastPage)
            {
                pageNumber++;
                data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, thrlist.Count - (pageNumber - 1)*20);
                PageNumber.Text = pageNumber.ToString();
                firstOrLastPage = false;
            }
            else
            {
                pageNumber = 1;
                data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, 20);
                PageNumber.Text = pageNumber.ToString();
                firstOrLastPage = true;
            }
        }

        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            if (pageNumber * 20 - 20 > 0)
            {
                pageNumber--;
                data.ItemsSource = thrlist.GetRange(pageNumber * 20 - 20, 20);
                PageNumber.Text = pageNumber.ToString();
                firstOrLastPage = true;
            }
            else
            {
                pageNumber = thrlist.Count % 20 == 0 ? thrlist.Count / 20 : thrlist.Count /20 +1;
                data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, thrlist.Count - (pageNumber - 1) * 20);
                PageNumber.Text = pageNumber.ToString();
                firstOrLastPage = false;
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            thrlist.Clear();
            string pathToExcelFile = "D:/thrlist.xlsx";
            ParseExcel(pathToExcelFile);
            data.Items.Refresh();
            data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, 20);
            PageNumber.Text = pageNumber.ToString();
        }
    }
}
