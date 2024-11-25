using CITOGAU.Windows.Requests;
using System.Windows;

namespace CITOGAU.Windows.WorkWindows
{
    public partial class EmployeeWindow : Window
    {
        private RequestListControl requestListControl;

        public EmployeeWindow()
        {
            InitializeComponent();
            requestListControl = new RequestListControl();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            CreateRequestWindow createRequestWindow = new CreateRequestWindow(requestListControl);
            createRequestWindow.Show();
        }

        private void ViewRequests_Click(object sender, RoutedEventArgs e)
        {
            EmployeeMainContent.Content = requestListControl;
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }

}
