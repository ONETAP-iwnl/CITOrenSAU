using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CITOGAU.ApiContext;
using CITOGAU.ApiContext.Service;
using CITOGAU.ApiContext.Services;
using CITOGAU.Classes.Tickets;
using CITOGAU.Interface.Authors;
using CITOGAU.Interface.Department;
using CITOGAU.Interface.Executors;
using CITOGAU.Interface.RequestType;
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
        private readonly IDepartmentService _departmentService;
        private readonly IRequestTypeService _requestTypeService;
        public RequestListControl()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.240.38.124:7215");
            _userService = new UserService("https://26.240.38.124:5235");
            _authorsService = new UserService("https://26.240.38.124:5235");
            _executorsService = new UserService("https://26.240.38.124:5235");
            _departmentService = new DepartmentService("https://26.240.38.124:7215");
            _requestTypeService = new RequestTypesService("https://26.240.38.124:7215");
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
            var department = await _departmentService.GetAllDepartmentAsync();
            var requestType = await _requestTypeService.GetAllRequestsAsync();


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
                    var departmentLink = department.FirstOrDefault(e => e.ID_Department == ticket.ID_Department);
                    if(departmentLink != null)
                    {
                        var _department = department.FirstOrDefault(e => e.ID_Department == departmentLink.ID_Department);
                        if(_department != null)
                        {
                            ticket.DepartmentName = _department.DepartmentName;
                        }
                    }

                    var requestTypeLink = requestType.FirstOrDefault(e => e.ID_RequestType == ticket.Type);
                    if(requestTypeLink != null)
                    {
                        var _requestType = requestType.FirstOrDefault(e => e.ID_RequestType == requestTypeLink.ID_RequestType);
                        if( _requestType != null)
                        {
                            ticket.RequestTypeName = _requestType.RequestTypeName;
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
