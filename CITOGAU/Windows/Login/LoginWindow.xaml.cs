using CITOGAU.ApiContext;
using System;
using System.Windows;
using System.Windows.Controls;


namespace CITOGAU
{
    public partial class LoginWindow : Window
    {
        private readonly AuthService _authService;
        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthService("https://26.191.182.183:7086");
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;

            var userResponse = await _authService.LoginAsync(login, password);

            if (userResponse != null)
            {
                // Сохраняем текущего пользователя в SessionManager
                SessionManager.CurrentUser = userResponse;
                Console.WriteLine(userResponse.FIO);

                if (userResponse.Role == "Админ")
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else if (userResponse.Role == "Тех. Специалист")
                {
                    TechWindow _techWindow = new TechWindow();
                    _techWindow.Show();
                    this.Close();
                }
                else if (userResponse.Role == "Сотрудник")
                {
                    EmployeeWindow _employeeWindow = new EmployeeWindow();
                    _employeeWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }
    }
}
