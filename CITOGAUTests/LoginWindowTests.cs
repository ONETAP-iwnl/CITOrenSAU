using Microsoft.VisualStudio.TestTools.UnitTesting;
using CITOGAU.Windows.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CITOGAU.ApiContext;
using CITOGAU.Classes.Users;
using Moq;
using System.Windows.Controls;
using CITOGAU.Interface.Auth;

namespace CITOGAU.Windows.Login.Tests
{
    [TestClass]
    public class LoginWindowTests
    {
        private Mock<IAuthService> _authServiceMock;
        private LoginWindow _loginWindow;

        [TestInitialize]
        public void Setup()
        {
            _authServiceMock = new Mock<IAuthService>();
            _loginWindow = new LoginWindow(_authServiceMock.Object);

            _loginWindow.LoginTextBoxPublic.Text = "admin";
            _loginWindow.PasswordBoxPublic.Password = "admin";
        }

        [TestMethod]
        public async Task LoginButton_ClickTest_AdminRole()
        {
            var userResponse = new UserResponse
            {
                FIO = "Admin User",
                Role = "Администратор"
            };

            _authServiceMock.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(userResponse);
            await _loginWindow.LoginButton_ClickAsync(null, null);
            Assert.AreEqual(SessionManager.CurrentUser, userResponse);
        }

        [TestMethod]
        public async Task LoginButton_ClickTest_InvalidCredentials()
        {
            _authServiceMock.Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((UserResponse)null);

            _loginWindow.LoginTextBoxPublic.Text = "";
            _loginWindow.PasswordBoxPublic.Password = "";

            await _loginWindow.LoginButton_ClickAsync(null, null);
        }
    }
}