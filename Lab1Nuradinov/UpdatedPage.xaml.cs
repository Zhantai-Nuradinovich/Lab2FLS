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

namespace Lab1Nuradinov
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class UpdatedPage
    {
        private Threat threat;
        private Dictionary<Threat, Threat> res;
        public UpdatedPage(Threat threatLink, Dictionary<Threat, Threat> res) 
        {
            InitializeComponent();
            threat = threatLink;
            this.res = res;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            List<Threat> comparative = new List<Threat>();

            comparative.Add(threat);
            comparative.Add(res[threat]);

            dataThreatWas.DataContext = comparative;
        }

        private void BackButtonPressed(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = MainWindow.contentControl.Content;
        }
    }
}
