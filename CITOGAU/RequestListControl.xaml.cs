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
using CITOGAU.ApiContext;

namespace CITOGAU
{
    public partial class RequestListControl : UserControl
    {
        private readonly TicketService _ticketService;
        private readonly UserService _userService;
        public RequestListControl()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.191.182.183:7215");
            _userService = new UserService("https://26.191.182.183:7118");
        }

        public async void Request_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRequests();
        }

        public async Task LoadRequests()
        {
            var tickets = await _ticketService.GetAllTicket();
            var users = await _userService.GetAllUserAsync();

            if (tickets != null && users != null)
            {
                // Сопоставляем ID авторов и исполнителей с их ФИО
                foreach (var ticket in tickets)
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

                RequestsDataGrid.ItemsSource = tickets;
            }
            else
            {
                MessageBox.Show("Failed to load tickets or users.");
            }

        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var ticket = button?.Tag as Ticket;

            if (ticket != null)
            {
                RequestDetailsWindow detailsWindow = new RequestDetailsWindow(ticket);
                detailsWindow.Show();
            }
        }
    }

}
