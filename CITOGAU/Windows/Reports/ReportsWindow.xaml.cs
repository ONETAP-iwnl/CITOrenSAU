using Microsoft.Win32;
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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using CITOGAU.ApiContext;
using CITOGAU.ApiContext.Service;
using CITOGAU.Classes.Tickets;
using CITOGAU.Interface.Authors;
using CITOGAU.Interface.Department;
using CITOGAU.Interface.Executors;
using CITOGAU.Interface.RequestType;
using CITOGAU.Interface.Tickets;
using CITOGAU.Interface.Users;
using CITOGAU.ApiContext.Services;
using CITOGAU.Classes.Authors;
using System.Net.Sockets;

namespace CITOGAU
{
    public partial class ReportsWindow : Window
    {
        private readonly ITicketService _ticketService;
        private readonly IUserService _userService;
        private readonly IAuthorsService _authorsService;
        private readonly IExecutorsService _executorsService;
        private readonly IDepartmentService _departmentService;
        private readonly IRequestTypeService _requestTypeService;

        public ReportsWindow()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.240.38.124:7215");
            _userService = new UserService("https://26.240.38.124:5235");
            _authorsService = new UserService("https://26.240.38.124:5235");
            _executorsService = new UserService("https://26.240.38.124:5235");
            _departmentService = new DepartmentService("https://26.240.38.124:7215");
            _requestTypeService = new RequestTypesService("https://26.240.38.124:7215");
        }

        private async void ReportsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadReports();
        }

        public async Task LoadReports()
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

                ReportsDataGrid.ItemsSource = tickets;
            }
            else
            {
                MessageBox.Show("Failed to load tickets, users, authors, or executors.");
            }
        }

        private async void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;

            var tickets = await _ticketService.GetAllTicketsAsync();
            var users = await _userService.GetAllUserAsync();
            var authors = await _authorsService.GetAllAuthorsAsync();
            var executors = await _executorsService.GetAllExecutorsAsync();
            var department = await _departmentService.GetAllDepartmentAsync();
            var requestType = await _requestTypeService.GetAllRequestsAsync();

            if (tickets != null)
            {
                var filteredTickets = tickets.Where(ticket =>
                    (!startDate.HasValue || ticket.DateOfCreation >= startDate) &&
                    (!endDate.HasValue || ticket.DateOfCreation <= endDate)
                ).ToList();

                foreach (var ticket in filteredTickets)
                {
                    if (ticket.Executor.HasValue)
                    {
                        var executor = users.FirstOrDefault(u => u.ID_User == ticket.Executor.Value);
                        if (executor != null)
                        {
                            ticket.ExecutorName = executor.FIO;
                        }
                    }
                    var authorLink = authors.FirstOrDefault(a => a.ID_Author == ticket.Author);
                    if (authorLink != null)
                    {
                        var author = users.FirstOrDefault(u => u.ID_User == authorLink.ID_User);
                        if (author != null)
                        {
                            ticket.AuthorName = author.FIO;
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
                }
                
                ReportsDataGrid.ItemsSource = filteredTickets;
            }
        }

        private void SearchByID_Click(object sender, RoutedEventArgs e)
        {
            var id = Convert.ToInt32(SearchByIdTextBox.Text);
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var tickets = (List<Ticket>)ReportsDataGrid.ItemsSource;
                var ticket = tickets?.FirstOrDefault(t => t.ID_Ticket == id);

                if (ticket != null)
                {
                    ReportsDataGrid.ItemsSource = new List<Ticket> { ticket };
                }
                else
                {
                    MessageBox.Show("Ticket not found.");
                }
            }
        }

        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Открываем диалог для сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "Отчеты.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                // Создаем новый Excel файл
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Отчеты");

                    // Заполняем заголовки
                    worksheet.Cells[1, 1].Value = "Номер заявки";
                    worksheet.Cells[1, 2].Value = "Подразделение";
                    worksheet.Cells[1, 3].Value = "Дата создания";
                    worksheet.Cells[1, 4].Value = "Дата завершения";
                    worksheet.Cells[1, 5].Value = "Статус";
                    worksheet.Cells[1, 6].Value = "Автор";
                    worksheet.Cells[1, 7].Value = "Исполнитель";

                    // Стиль для заголовков
                    using (var range = worksheet.Cells[1, 1, 1, 8])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Получаем данные из ReportsDataGrid
                    var tickets = ReportsDataGrid.ItemsSource as IEnumerable<Ticket>;

                    if (tickets != null)
                    {
                        int row = 2; // Начинаем с 2-й строки, т.к. первая — заголовки
                        foreach (var ticket in tickets)
                        {
                            worksheet.Cells[row, 1].Value = ticket.ID_Ticket;
                            worksheet.Cells[row, 2].Value = ticket.DepartmentName;
                            worksheet.Cells[row, 3].Value = ticket.DateOfCreation?.ToString("dd/MM/yyyy HH:mm:ss");
                            worksheet.Cells[row, 4].Value = ticket.DateOfCompletion?.ToString("dd/MM/yyyy HH:mm:ss");
                            worksheet.Cells[row, 5].Value = ticket.StatusName;
                            worksheet.Cells[row, 6].Value = ticket.AuthorName;
                            worksheet.Cells[row, 7].Value = ticket.ExecutorName;
                            row++;
                        }
                    }

                    // Сохраняем файл
                    File.WriteAllBytes(saveFileDialog.FileName, package.GetAsByteArray());
                    MessageBox.Show("Отчеты успешно экспортированы в Excel.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SearchByIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchByIdTextBox.Text))
            {
                await LoadReports();
            }
        }
    }
}
