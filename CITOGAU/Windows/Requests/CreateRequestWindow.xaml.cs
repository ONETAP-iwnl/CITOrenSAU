using CITOGAU.ApiContext;
using System;
using System.Windows;

namespace CITOGAU
{
    public partial class CreateRequestWindow : Window
    {
        private TicketService _ticketService;
        RequestListControl _requestListControl = new RequestListControl();
        public CreateRequestWindow(RequestListControl requestListControl)
        {
            InitializeComponent();
            _requestListControl = requestListControl;
            _ticketService = new TicketService("https://26.191.182.183:7215");
        }

        private async void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем ФИО текущего пользователя из SessionManager
            int currentUserFIO = SessionManager.CurrentUser.ID_User;

            Ticket ticket = new Ticket
            {
                AudienceNumber = AudienceTextBox.Text,
                BuildingNumber = BuildingTextBox.Text,
                DateOfCreation = DateTime.Now,
                Status = "Новая",
                Author = currentUserFIO,  // Используем ФИО текущего пользователя
                Type = ProblemTypeTextBox.Text,
                Description = DescriptionTextBox.Text
            };

            // Сохраняем заявку
            var newTicket = await _ticketService.CreateTicket(ticket);
            if (newTicket != null)
            {
                MessageBox.Show("Заявка успешно создана", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                await _requestListControl.LoadRequests();
                this.Close();
            }
            else
            {
                Console.WriteLine($"Ошибка: {ticket}");
            }
        }
    }
}
