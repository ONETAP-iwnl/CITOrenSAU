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
using CITOGAU.ApiContext.Service;
using CITOGAU.ApiContext.Services;
using CITOGAU.Classes.Tickets;
using CITOGAU.Classes.Users;
using CITOGAU.Interface.Authors;
using CITOGAU.Interface.Department;
using CITOGAU.Interface.Executors;
using CITOGAU.Interface.RequestType;
using CITOGAU.Interface.Tickets;
using CITOGAU.Interface.Users;
using CITOGAU.Windows.Requests;

namespace CITOGAU
{
    public partial class TechWindow : Window
    {
        //private readonly TicketService _ticketService;
        //private readonly UserService _userService;
        //private List<Ticket> _tickets;

        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IAuthorsService _authorsService;
        private readonly IExecutorsService _executorsService;
        private readonly IDepartmentService _departmentService;
        private readonly IRequestTypeService _requestTypeService;


        public TechWindow()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.240.38.124:7215");
            _userService = new UserService("https://26.240.38.124:5235");
            _authorsService = new UserService("https://26.240.38.124:5235");
            _executorsService = new UserService("https://26.240.38.124:5235");
            _departmentService = new DepartmentService("https://26.240.38.124:7215");
            _requestTypeService = new RequestTypesService("https://26.240.38.124:7215");
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
                    if (departmentLink != null)
                    {
                        var _department = department.FirstOrDefault(e => e.ID_Department == departmentLink.ID_Department);
                        if (_department != null)
                        {
                            ticket.DepartmentName = _department.DepartmentName;
                        }
                    }

                    var requestTypeLink = requestType.FirstOrDefault(e => e.ID_RequestType == ticket.Type);
                    if (requestTypeLink != null)
                    {
                        var _requestType = requestType.FirstOrDefault(e => e.ID_RequestType == requestTypeLink.ID_RequestType);
                        if (_requestType != null)
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


        private async void TakeInWorkButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsDataGrid.SelectedItem is Ticket selectedTicket)
            {
                if (selectedTicket.Status == 2) // 2 = новая
                {
                    int currentUserID = SessionManager.CurrentUser.ID_User;
                    int? executorID = await _executorsService.GetExecutorsByUserIdAsync(currentUserID);

                    if (executorID.HasValue)
                    {
                        selectedTicket.Status = 3; // 3 = в процессе
                        selectedTicket.Executor = executorID.Value;

                        try
                        {
                            bool statusUpdated = await _ticketService.UpdateTicketStatusAsync(selectedTicket.ID_Ticket, selectedTicket.Status);
                            bool executorAssigned = await _ticketService.AssignExecutorToTicketAsync(selectedTicket.ID_Ticket, selectedTicket.Executor.Value);

                            if (statusUpdated && executorAssigned)
                            {
                                Request_Loaded(this, new RoutedEventArgs());
                                MessageBox.Show("Заявка взята в работу.");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении заявки.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при обновлении заявки: {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Исполнитель не найден для текущего пользователя.");
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
                if (selectedTicket.Status == 3) // 3 = в процессе
                {
                    selectedTicket.Status = 1; // 1 = выполнена
                    selectedTicket.DateOfCompletion = DateTime.Now;
                    DateTime completionDate = selectedTicket.DateOfCompletion ?? DateTime.Now;
                    try
                    {
                        bool dateUpdated = await _ticketService.UpdateTicketCompletionDateAsync(selectedTicket.ID_Ticket, selectedTicket.DateOfCompletion);
                        bool statusUpdated = await _ticketService.UpdateTicketStatusAsync(selectedTicket.ID_Ticket, selectedTicket.Status);

                        if (dateUpdated && statusUpdated)
                        {
                            MessageBox.Show("Заявка завершена.");
                            Request_Loaded(this, new RoutedEventArgs()); // Обновляем DataGrid после изменения данных
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении заявки.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при завершении заявки: {ex.Message}");
                    }
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

        private void Request_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRequests();
        }
    }
}
