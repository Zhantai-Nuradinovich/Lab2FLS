using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
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
        //База данных угроз
        private static volatile List<Threat> thrlist = new List<Threat>();
        //Старых данных
        private static List<Threat> updateHistory;

        //Словарь изменений
        private static Dictionary<Threat, Threat> res = new Dictionary<Threat, Threat>();

        private static ThreatPage page;
        private static UpdatedPage updatedPage;
        //Объект для сохранения контента, для переключения между страницой и гланвым окном
        public static ContentControl contentControl = new ContentControl();

        private static int pageNumber = 1;
        private static bool firstOrLastPage = true;
        private static bool isClickedAfterUpdate = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string pathToExcelFile = "D:/thrlist.xlsx";
            ParseExcel(pathToExcelFile);
            
            data.ItemsSource = thrlist.Count > 0 ? thrlist.GetRange((pageNumber-1)*20, 20):null;
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
                        wc.DownloadFile(url, pathToExcelFile);

                        ParseExcel(pathToExcelFile);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

        }
        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isClickedAfterUpdate)
            {
                contentControl.Content = this.Content;
                Threat check = e.AddedItems.Count != 0 ? (Threat)e.AddedItems[0] : null;
                page = new ThreatPage(check, thrlist);
                if (check != null)
                {
                    this.Content = page;
                }
            }
            else
            {
                contentControl.Content = this.Content;
                Threat check = e.AddedItems.Count != 0 ? (Threat)e.AddedItems[0] : null;
                updatedPage = new UpdatedPage(check, res);
                if (check != null)
                {
                    this.Content = updatedPage;
                }
            }
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            if(thrlist.Count > 0)
            {
                if (pageNumber * 20 + 20 < thrlist.Count)
                {
                    pageNumber++;
                    data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, 20);
                    PageNumber.Text = pageNumber.ToString();
                    firstOrLastPage = true;
                }
                else if (firstOrLastPage)
                {
                    pageNumber++;
                    data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, thrlist.Count - (pageNumber - 1) * 20);
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
        }

        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            if (thrlist.Count > 0)
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
                    pageNumber = thrlist.Count % 20 == 0 ? thrlist.Count / 20 : thrlist.Count / 20 + 1;
                    data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, thrlist.Count - (pageNumber - 1) * 20);
                    PageNumber.Text = pageNumber.ToString();
                    firstOrLastPage = false;
                }
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Decorate();
            data.ItemsSource = null;
            data.Items.Refresh();

            updateHistory = new List<Threat>(thrlist);
            thrlist.Clear();

            string pathToExcelFile = "D:/thrlist.xlsx";
            WebClient wc = new WebClient();
            string url = "http://bdu.fstec.ru/files/documents/thrlist.xlsx";
            wc.DownloadFile(url, pathToExcelFile);

            try
            {
                ParseExcel(pathToExcelFile);
                Analise(thrlist, updateHistory);
            }
            catch (Exception x)
            {
                string messageBoxText = x.Message;
                string caption = "Ошибка!!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        public void BackComplexButtonPressed(object sender, RoutedEventArgs e)
        {
            isClickedAfterUpdate = false;
            UnDecorate();
            pageNumber = 1;
            data.ItemsSource = thrlist.GetRange((pageNumber - 1) * 20, 20);
            PageNumber.Text = pageNumber.ToString();
            data.Items.Refresh();
        }
        private void Decorate()
        {
            backComplexButton.Visibility = Visibility.Visible;
            prevPageButton.Visibility = Visibility.Hidden;
            nextPageButton.Visibility = Visibility.Hidden;
            updateButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            Page.Visibility = Visibility.Hidden;
            PageNumber.Text = "";
            listOfUpdatedThreats.Visibility = Visibility.Visible;
        }

        private void UnDecorate()
        {
            backComplexButton.Visibility = Visibility.Hidden;
            prevPageButton.Visibility = Visibility.Visible;
            nextPageButton.Visibility = Visibility.Visible;
            updateButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;
            Page.Visibility = Visibility.Visible;
            listOfUpdatedThreats.Visibility = Visibility.Hidden;
        }

        private void Analise(List<Threat> thrlist, List<Threat> updateHistory)
        {
            int count = 0;

            foreach (var item in thrlist)
            {
                foreach (var itemOld in updateHistory)
                {
                    if (item.Id == itemOld.Id &&
                        !item.ToString().Equals(itemOld.ToString()))
                    {
                        res.Add(item, itemOld);
                        count++;
                    }
                }
            }
            if(count == 0)
            {
                string messageBoxText = "Нет обновленных записей!!";
                string caption = "Сделано!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBox.Show(messageBoxText, caption, button);
            }
            else
            {
                string messageBoxText = "Успешно! Число обновленных записей равно " + count + "\nОткрыть подробный список?";
                string caption = "Сделано!";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        //updateHistory = new List<Threat>(thrlist.FindAll(delegate(Threat a) 
                        //{
                        //   return a.IsChanged;
                        //}));

                        //data.ItemsSource = updateHistory;
                        data.ItemsSource = res.Keys;
                        data.Items.Refresh();
                        isClickedAfterUpdate = true;
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }

        private void saveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] res = new string[thrlist.Count];
                string path = "D:/thrlist.csv";
                using (FileStream maker = File.Create(path))
                {
                    for (int i = 0; i < thrlist.Count; i++)
                    {
                        res[i] = thrlist[i].ToString();
                    }
                }
                File.WriteAllLines(path, res);
            }
            finally
            {
                MessageBox.Show("Сохранено!!", "Сохранение файла", MessageBoxButton.OK);
            }
        }
    }
}
