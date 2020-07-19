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
    public partial class ThreatPage : Page
    {
        private List<Threat> thrlist;
        private Threat threat;
        public ThreatPage(Threat threatLink, List<Threat> thrlist)
        {
            InitializeComponent();
            threat = threatLink;
            this.thrlist = thrlist;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            List<Threat> singleton = new List<Threat>();
            singleton.Add(threat);
            dataThreat.DataContext = singleton;
        }

        private void BackButtonPressed(object sender, RoutedEventArgs e)
        {
            ((Window)this.Parent).Content = MainWindow.contentControl.Content;
        }

        private void ShowNextThreat(object sender, RoutedEventArgs e)
        {
            List<Threat> singleton = new List<Threat>();
            threat = thrlist[thrlist.IndexOf(threat) + 1 < thrlist.Count ? thrlist.IndexOf(threat) + 1 : 0];
            singleton.Add(threat);
            dataThreat.DataContext = singleton;
        }

        private void ShowPreviousThreat(object sender, RoutedEventArgs e)
        {
            List<Threat> singleton = new List<Threat>();
            threat = thrlist[thrlist.IndexOf(threat) - 1 >= 0 ? thrlist.IndexOf(threat) - 1 : thrlist.Count - 1];
            singleton.Add(threat);
            dataThreat.DataContext = singleton;
        }

        private void SaveEdit(object sender, RoutedEventArgs e)
        {

        }
    }
}
