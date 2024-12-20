using CITOGAU.ApiContext;
using CITOGAU.Classes.Facade;
using CITOGAU.Windows.Requests;
using System.Windows;

namespace CITOGAU.Windows.WorkWindows
{
    public partial class EmployeeWindow : Window
    {
        private readonly WindowFacade _windowFacade;

        public EmployeeWindow()
        {
            InitializeComponent();
            _windowFacade = new WindowFacade(new AuthService("https://26.240.38.124:7086"));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            var requestListControl = new RequestListControl();
            _windowFacade.OpenCreateRequestWindow(requestListControl);
        }

        private void ViewRequests_Click(object sender, RoutedEventArgs e)
        {
            _windowFacade.OpenRequestListControl(EmployeeMainContent);
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _windowFacade.OpenAboutWindow();
        }

    }
}
