using CITOGAU.Windows.Requests;
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

namespace CITOGAU.Windows.WorkWindows
{
    public partial class MainWindow : Window
    {
        private RequestListControl requestListControl;

        public MainWindow()
        {
            InitializeComponent();
            requestListControl = new RequestListControl();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewRequests_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = requestListControl;
        }

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            CreateRequestWindow createRequestWindow = new CreateRequestWindow(requestListControl);
            createRequestWindow.Show();
        }

        private void ViewReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsWindow reportsWindow = new ReportsWindow();
            reportsWindow.Show();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
