using CITOGAU.ApiContext;
using CITOGAU.Classes.Facade;
using CITOGAU.Classes.Users;
using CITOGAU.Interface.Auth;
using CITOGAU.Windows.WorkWindows;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace CITOGAU.Windows.Login
{
    public partial class LoginWindow : Window
    {
        private readonly WindowFacade _windowFacade;

        public LoginWindow()
        {
            InitializeComponent();
            _windowFacade = new WindowFacade(new AuthService("https://26.240.38.124:7086"));
        }

        public LoginWindow(IAuthService authService)
        {
            InitializeComponent();
            _windowFacade = new WindowFacade(authService);
        }

        public async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var password = PasswordBox.Password;

            bool loginSuccess = await _windowFacade.LoginAsync(login, password);

            if (loginSuccess)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }
    }
}
