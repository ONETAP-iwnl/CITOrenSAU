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

namespace CITOGAU
{
    public partial class ReportsWindow : Window
    {
        private readonly TicketService _ticketService;
        private readonly UserService _userService;

        public ReportsWindow()
        {
            InitializeComponent();
            _ticketService = new TicketService("https://26.191.182.183:7215");
            _userService = new UserService("https://26.191.182.183:7118");
        }

        private async void ReportsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadReports();
        }

        public async Task LoadReports()
        {
            var tickets = await _ticketService.GetAllTicket();
            var users = await _userService.GetAllUserAsync();

            if (tickets != null && users != null)
            {
                foreach (var ticket in tickets)
                {
                    if (ticket.Executor.HasValue)
                    {
                        var executor = users.FirstOrDefault(u => u.ID_User == ticket.Executor.Value);
                        if (executor != null)
                        {
                            ticket.ExecutorName = executor.FIO;
                        }
                    }
                }

                ReportsDataGrid.ItemsSource = tickets;
            }
            else
            {
                MessageBox.Show("Failed to load tickets or users.");
            }
        }

        private async void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDatePicker.SelectedDate;
            var endDate = EndDatePicker.SelectedDate;

            var users = await _userService.GetAllUserAsync();
            var tickets = await _ticketService.GetAllTicket();

            if (tickets != null)
            {
                var filteredTickets = tickets.Where(ticket =>
                    (!startDate.HasValue || ticket.DateOfCreation >= startDate) &&
                    (!endDate.HasValue || ticket.DateOfCreation <= endDate)
                ).ToList();

                foreach (var ticket in filteredTickets)
                {
                    if(ticket.Executor.HasValue)
                    {
                        var executor = users.FirstOrDefault(u => u.ID_User == ticket.Executor.Value);
                        if(executor!= null)
                        {
                            ticket.ExecutorName = executor.FIO;
                        }    
                    }
                }
                ReportsDataGrid.ItemsSource = filteredTickets;
            }
        }

        private async void SearchByID_Click(object sender, RoutedEventArgs e)
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
                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Аудитория";
                    worksheet.Cells[1, 3].Value = "Здание";
                    worksheet.Cells[1, 4].Value = "Дата создания";
                    worksheet.Cells[1, 5].Value = "Дата завершения";
                    worksheet.Cells[1, 6].Value = "Статус";
                    worksheet.Cells[1, 7].Value = "Автор";
                    worksheet.Cells[1, 8].Value = "Исполнитель";

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
                            worksheet.Cells[row, 2].Value = ticket.AudienceNumber;
                            worksheet.Cells[row, 3].Value = ticket.BuildingNumber;
                            worksheet.Cells[row, 4].Value = ticket.DateOfCreation?.ToString("dd.MM.yyyy");
                            worksheet.Cells[row, 5].Value = ticket.DateOfCompletion?.ToString("dd.MM.yyyy");
                            worksheet.Cells[row, 6].Value = ticket.Status;
                            worksheet.Cells[row, 7].Value = ticket.Author; // Здесь можешь добавить приведение автора к строке, если нужно ФИО
                            worksheet.Cells[row, 8].Value = ticket.Executor.HasValue ? ticket.Executor.ToString() : "Не назначен";
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
