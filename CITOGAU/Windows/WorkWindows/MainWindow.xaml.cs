using CITOGAU.ApiContext;
using CITOGAU.Classes.Facade;
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
        private readonly WindowFacade _windowFacade;

        public MainWindow()
        {
            InitializeComponent();
            _windowFacade = new WindowFacade(new AuthService("https://26.240.38.124:7086"));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ViewRequests_Click(object sender, RoutedEventArgs e)
        {
            _windowFacade.OpenRequestListControl(MainContent);
        }

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            var requestListControl = new RequestListControl();
            _windowFacade.OpenCreateRequestWindow(requestListControl);
        }

        private void ViewReports_Click(object sender, RoutedEventArgs e)
        {
            _windowFacade.OpenReportsWindow();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _windowFacade.OpenAboutWindow();
        }
    }
}
