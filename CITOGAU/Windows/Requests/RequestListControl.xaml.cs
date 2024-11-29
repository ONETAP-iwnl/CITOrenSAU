using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CITOGAU.ApiContext;
using CITOGAU.ApiContext.Service;
using CITOGAU.Classes.Tickets;
using CITOGAU.Interface.Authors;
using CITOGAU.Interface.Executors;
using CITOGAU.Interface.Tickets;
using CITOGAU.Interface.Users;

namespace CITOGAU.Windows.Requests
{
    public partial class RequestListControl : UserControl
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IAuthorsService _authorsService;
        private readonly IExecutorsService _executorsService;
        public RequestListControl()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.240.38.124:7215");
            _userService = new UserService("https://26.240.38.124:5235");
            _authorsService = new UserService("https://26.240.38.124:5235");
            _executorsService = new UserService("https://26.240.38.124:5235");
        }

        public async void Request_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRequests();
        }

        private async Task LoadRequests()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            var users = await _userService.GetAllUserAsync();
            var authors = await _authorsService.GetAllAuthorsAsync();
            var executors = await _executorsService.GetAllExecutorsAsync();


            var statusDictionary = new Dictionary<int, string>
            {
                { 1, "Выполнена" },
                { 2, "Новая" },
                { 3, "В процессе" },
                { 4, "Отменена" }
            };


            if (tickets != null && users != null && authors != null && executors != null)
            {
                foreach (var ticket in tickets)
                {
                    var authorLink = authors.FirstOrDefault(a => a.ID_Author == ticket.Author);
                    if (authorLink != null)
                    {
                        var author = users.FirstOrDefault(u => u.ID_User == authorLink.ID_User);
                        if (author != null)
                        {
                            ticket.AuthorName = author.FIO;
                        }
                    }

                    if (ticket.Executor.HasValue)
                    {
                        var executorLink = executors.FirstOrDefault(e => e.ID_Executor == ticket.Executor.Value);
                        if (executorLink != null)
                        {
                            var executor = users.FirstOrDefault(u => u.ID_User == executorLink.ID_User);
                            if (executor != null)
                            {
                                ticket.ExecutorName = executor.FIO;
                            }
                        }
                    }
                    if (statusDictionary.TryGetValue(ticket.Status, out var status))
                    {
                        ticket.StatusName = status;
                    }
                }

                RequestsDataGrid.ItemsSource = tickets;
            }
            else
            {
                MessageBox.Show("Failed to load tickets, users, authors, or executors.");
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
