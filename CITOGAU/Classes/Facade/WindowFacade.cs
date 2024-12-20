using CITOGAU.Classes.Users;
using CITOGAU.Interface.Auth;
using CITOGAU.Windows.Requests;
using CITOGAU.Windows.WorkWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CITOGAU.Classes.Facade
{
    public class WindowFacade
    {
        private readonly IAuthService _authService;

        public WindowFacade(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            var userResponse = await _authService.LoginAsync(login, password);

            if (userResponse != null)
            {
                SessionManager.CurrentUser = userResponse;
                Console.WriteLine(userResponse.FIO);

                OpenMainWindow(userResponse.Role);
                return true;
            }

            return false;
        }

        public void OpenMainWindow(string role)
        {
            Window mainWindow = role switch
            {
                "Администратор" => new MainWindow(),
                "Тех. Специалист" => new TechWindow(),
                "Сотрудник" => new EmployeeWindow(),
                _ => null
            };

            mainWindow?.Show();
        }

        public void OpenRequestListControl(ContentControl contentControl)
        {
            contentControl.Content = new RequestListControl();
        }

        public void OpenCreateRequestWindow(RequestListControl requestListControl)
        {
            var createRequestWindow = new CreateRequestWindow(requestListControl);
            createRequestWindow.Show();
        }

        public void OpenReportsWindow()
        {
            var reportsWindow = new ReportsWindow();
            reportsWindow.Show();
        }

        public void OpenAboutWindow()
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
