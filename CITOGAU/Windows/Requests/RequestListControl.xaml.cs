using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CITOGAU.ApiContext;
using CITOGAU.ApiContext.Service;
using CITOGAU.Classes.Tickets;
using CITOGAU.Interface.Tickets;

namespace CITOGAU.Windows.Requests
{
    public partial class RequestListControl : UserControl
    {
        private readonly ITicketService _ticketService;
        private readonly UserService _userService;
        public RequestListControl()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.240.38.124:7215");
            _userService = new UserService("https://26.240.38.124:5235");
        }

        public async void Request_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRequests();
        }

        private async Task LoadRequests()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            var users = await _userService.GetAllUserAsync();

            if (tickets != null && users != null)
            {
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
            //var button = sender as Button;
            //var ticket = button?.Tag as Ticket;

            //if (ticket != null)
            //{
            //    RequestDetailsWindow detailsWindow = new RequestDetailsWindow(ticket);
            //    detailsWindow.Show();
            //}
        }
    }
}
