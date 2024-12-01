using CITOGAU.ApiContext.Service;
using CITOGAU.ApiContext.Services;
using CITOGAU.Classes.Department;
using CITOGAU.Classes.Tickets;
using CITOGAU.Classes.Users;
using CITOGAU.Interface.Department;
using CITOGAU.Interface.RequestType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CITOGAU.Windows.Requests
{
    public partial class CreateRequestWindow : Window, INotifyPropertyChanged
    {
        private TicketService _ticketService;
        private readonly IRequestTypeService _requestTypeService;
        private readonly IDepartmentService _departmentService;
        RequestListControl _requestListControl = new RequestListControl();
        public CreateRequestWindow(RequestListControl requestListControl)
        {
            InitializeComponent();
            _requestListControl = requestListControl;
            _ticketService = new TicketService("https://26.191.182.183:7215");
            _requestTypeService = new RequestTypesService("https://26.240.38.124:7215");
            _departmentService = new DepartmentService("https://26.240.38.124:7215");

            DataContext = this;
        }


        private List<DepartmentGroup> _departmentGroups;
        public List<DepartmentGroup> DepartmentGroups
        {
            get => _departmentGroups;
            set
            {
                _departmentGroups = value;
                OnPropertyChanged();
            }
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRequestType();
            await LoadDepartment();
        }

        private async Task LoadRequestType()
        {
            var requestTypes = await _requestTypeService.GetAllRequestsAsync();
            RequestTypeCB.ItemsSource = requestTypes;
            RequestTypeCB.DisplayMemberPath = "RequestTypeName";
        }

        private async Task LoadDepartment()
        {
            var departmentTypes = await _departmentService.GetAllDepartmentTypeAsync();
            var departments = await _departmentService.GetAllDepartmentAsync();

            var groupedDepartments = departmentTypes.Select(dt => new DepartmentGroup
            {
                DepartmentTypeName = dt.DepartmentTypeName,
                Departments = departments.Where(d => d.ID_Department == dt.ID_DepartmentType).ToList()
            }).ToList();

            DepartmentGroups = groupedDepartments;

            var lcv = new ListCollectionView(groupedDepartments);
            lcv.GroupDescriptions.Add(new PropertyGroupDescription("DepartmentTypeName"));

            DepartmentComboBox.ItemsSource = lcv;
        }


        private void CreateRequestButton_Click(object sender, RoutedEventArgs e)
        {
        //// Получаем ФИО текущего пользователя из SessionManager
        //int currentUserFIO = SessionManager.CurrentUser.ID_User;

        //Ticket ticket = new Ticket
        //{
        //    AudienceNumber = AudienceTextBox.Text,
        //    BuildingNumber = BuildingTextBox.Text,
        //    DateOfCreation = DateTime.Now,
        //    Status = "Новая",
        //    Author = currentUserFIO,  // Используем ФИО текущего пользователя
        //    Type = ProblemTypeTextBox.Text,
        //    Description = DescriptionTextBox.Text
        //};

        //// Сохраняем заявку
        //var newTicket = await _ticketService.CreateTicket(ticket);
        //if (newTicket != null)
        //{
        //    MessageBox.Show("Заявка успешно создана", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        //    await _requestListControl.LoadRequests();
        //    this.Close();
        //}
        //else
        //{
        //    Console.WriteLine($"Ошибка: {ticket}");
        //}
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
