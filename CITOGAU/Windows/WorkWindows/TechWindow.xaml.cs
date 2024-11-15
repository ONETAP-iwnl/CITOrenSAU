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
using System.Windows.Shapes;
using CITOGAU.ApiContext;

namespace CITOGAU
{
    public partial class TechWindow : Window
    {
        private readonly TicketService _ticketService;
        private readonly UserService _userService;
        private List<Ticket> _tickets;

        public TechWindow()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.191.182.183:7215");
            _userService = new UserService("https://26.191.182.183:7118");
            LoadRequests();
        }

        private async void LoadRequests()
        {
            _tickets = await _ticketService.GetAllTicket();
            var users = await _userService.GetAllUserAsync();

            if (_tickets != null && users != null)
            {
                foreach (var ticket in _tickets)
                {
                    var author = users.FirstOrDefault(u => u.ID_User == ticket.Author);
                    if (author != null)
                    {
                        ticket.AuthorName = author.FIO;
                    }

                    if (ticket.Executor.HasValue)
                    {
                        var executor = users.FirstOrDefault(u => u.ID_User == ticket.Executor.Value);
                        if (executor != null)
                        {
                            ticket.ExecutorName = executor.FIO;
                        }
                    }
                }

                RequestsDataGrid.ItemsSource = _tickets;
            }
            else
            {
                MessageBox.Show("Не удалось загрузить заявки или пользователей.");
            }
        }


        private async void TakeInWorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is Ticket selectedTicket)
            {
                if (selectedTicket.Status == "Новая")
                {
                    selectedTicket.Status = "В работе";
                    selectedTicket.Executor = SessionManager.CurrentUser.ID_User;

                    try
                    {
                        await _ticketService.UpdateTicket(selectedTicket);
                        RequestsDataGrid.Items.Refresh();
                        MessageBox.Show("Заявка взята в работу.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении заявки: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Заявка уже взята в работу или завершена.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку.");
            }
        }
        private async void CompleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is Ticket selectedTicket)
            {
                if (selectedTicket.Status == "В работе")
                {
                    selectedTicket.Status = "Завершена";
                    selectedTicket.DateOfCompletion = DateTime.Now;
                    await _ticketService.UpdateTicket(selectedTicket);
                    RequestsDataGrid.Items.Refresh();
                    MessageBox.Show("Заявка завершена.");
                }
                else
                {
                    MessageBox.Show("Заявка не может быть завершена, пока она не находится в работе.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заявку.");
            }
        }
    }
}
