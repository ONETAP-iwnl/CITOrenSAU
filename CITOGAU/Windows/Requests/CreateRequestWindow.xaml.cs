using CITOGAU.ApiContext;
using CITOGAU.ApiContext.Service;
using CITOGAU.ApiContext.Services;
using CITOGAU.Classes.Department;
using CITOGAU.Classes.RequestType;
using CITOGAU.Classes.Tickets;
using CITOGAU.Classes.Users;
using CITOGAU.Interface.Authors;
using CITOGAU.Interface.Department;
using CITOGAU.Interface.RequestType;
using CITOGAU.Interface.Tickets;
using CITOGAU.Interface.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace CITOGAU.Windows.Requests
{
    public partial class CreateRequestWindow : Window
    {
        private readonly ITicketService _ticketService;
        private readonly IRequestTypeService _requestTypeService;
        private readonly IDepartmentService _departmentService;
        private readonly IAuthorsService _authorsService;
        RequestListControl _requestListControl = new RequestListControl();
        public CreateRequestWindow(RequestListControl requestListControl)
        {
            InitializeComponent();
            _requestListControl = requestListControl;
            _ticketService = new TicketService("https://26.240.38.124:7215");
            _requestTypeService = new RequestTypesService("https://26.240.38.124:7215");
            _departmentService = new DepartmentService("https://26.240.38.124:7215");
            _authorsService = new UserService("https://26.240.38.124:5235");
            DataContext = this;
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRequestType();
            await LoadDepartment();
        }

        private async Task LoadDepartment()
        {
            var items = await GetGroupedDepartmentsAsync();
            var lcv = new ListCollectionView(items);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            DepartmentComboBox.ItemsSource = lcv;
        }

        private async Task LoadRequestType()
        {
            var requestTypes = await _requestTypeService.GetAllRequestsAsync();
            RequestTypeCB.ItemsSource = requestTypes;
            RequestTypeCB.DisplayMemberPath = "RequestTypeName";
        }

        public async Task<List<Classes.Department.ListItem>> GetGroupedDepartmentsAsync()
        {
            var departments = await _departmentService.GetAllDepartmentAsync();
            var departmentTypes = await _departmentService.GetAllDepartmentTypeAsync();

            var items = new List<Classes.Department.ListItem>();

            foreach (var dt in departmentTypes)
            {
                var deptItems = departments
                    .Where(d => d.ID_DepartmentType == dt.ID_DepartmentType)
                    .Select(d => new Classes.Department.ListItem
                    {
                        Title = d.DepartmentName,
                        Value = d.ID_Department.ToString(),
                        Group = dt.DepartmentTypeName
                    }).ToList();

                items.AddRange(deptItems);
            }
            return items;
        }


        private async void CreateRequestButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            int currentUserID = SessionManager.CurrentUser.ID_User;

            int? authorId = await _authorsService.GetAuthorByUserIdAsync(currentUserID);
            if (authorId == null)
            {
                MessageBox.Show("Автор не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedDepartment = DepartmentComboBox.SelectedItem as Classes.Department.ListItem;
            int selectedDepartmentId = selectedDepartment != null ? int.Parse(selectedDepartment.Value) : 0;

            var selectedRequestType = RequestTypeCB.SelectedItem as RequestTypes;
            int selectedRequestTypeId = selectedRequestType != null ? selectedRequestType.ID_RequestType : 0;

            Ticket ticket = new Ticket
            {
                ID_Department = selectedDepartmentId,  
                DateOfCreation = DateTime.Now,
                Status = 2,
                Author = authorId.Value,  
                Type = selectedRequestTypeId, 
                Description = DescriptionTextBox.Text
            };
            var newTicket = _ticketService.CreateTicketAsync(ticket);
            if (newTicket != null)
            {
                MessageBox.Show("Заявка успешно создана", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                _requestListControl.Request_Loaded(this, new RoutedEventArgs());
                this.Close();
            }
            else
            {
                Console.WriteLine($"Ошибка: {ticket}");
            }
        }

    }
}
