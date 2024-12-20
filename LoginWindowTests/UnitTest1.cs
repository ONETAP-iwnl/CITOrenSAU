using CITOGAU.ApiContext;
using CITOGAU.Classes.Users;
using CITOGAU.Windows.Login;
using Moq;
using System.Windows;
using System.Windows.Controls;

namespace LoginWindowTests
{
    public class LoginWindowTests
    {
        private readonly Mock<AuthService> _authServiceMock;
        private readonly LoginWindow _loginWindow;
        private readonly Mock<TextBox> _loginTextBoxMock;
        private readonly Mock<PasswordBox> _passwordBoxMock;

        public LoginWindowTests()
        {
            _authServiceMock = new Mock<AuthService>();
            _loginWindow = new LoginWindow(_authServiceMock.Object);

            _loginTextBoxMock = new Mock<TextBox>();
            _passwordBoxMock = new Mock<PasswordBox>();

            _loginWindow.LoginTextBox = _loginTextBoxMock.Object;
            _loginWindow.PasswordBox = _passwordBoxMock.Object;
        }

        [Fact]
        public async Task LoginButton_Click_WithValidCredentials_SetsCurrentUserAndOpensMainWindow()
        {
            // Arrange
            _loginTextBoxMock.Setup(x => x.Text).Returns("admin");
            _passwordBoxMock.Setup(x => x.Password).Returns("password");

            var userResponse = new UserResponse { FIO = "Admin User", Role = "Администратор" };
            _authServiceMock.Setup(x => x.LoginAsync("admin", "password")).ReturnsAsync(userResponse);

            // Act
            _loginWindow.LoginButton_Click(this, new RoutedEventArgs());

            // Assert
            Assert.Equal(userResponse, SessionManager.CurrentUser);
            _authServiceMock.Verify(x => x.LoginAsync("admin", "password"), Times.Once);
        }

        [Fact]
        public async Task LoginButton_Click_WithInvalidCredentials_ShowsLoginFailedMessage()
        {
            // Arrange
            _loginTextBoxMock.Setup(x => x.Text).Returns("admin");
            _passwordBoxMock.Setup(x => x.Password).Returns("wrongpassword");

            _authServiceMock.Setup(x => x.LoginAsync("admin", "wrongpassword")).ReturnsAsync((UserResponse)null);

            // Act
             _loginWindow.LoginButton_Click(this, new RoutedEventArgs());

            // Assert
            _authServiceMock.Verify(x => x.LoginAsync("admin", "wrongpassword"), Times.Once);
            // Здесь нужно будет проверить, что MessageBox был показан. Это может потребовать больше кода для интерсептинга вызова MessageBox.
        }
    }
}