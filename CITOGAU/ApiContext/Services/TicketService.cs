using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CITOGAU.Classes.Tickets;
using CITOGAU.Interface.Tickets;

namespace CITOGAU.ApiContext.Service
{
    public class TicketService: ITicketService
    {
        private readonly HttpClient _httpClient;
        public TicketService(string baseAddress)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true
            };


            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            var response = await _httpClient.GetAsync("/Ticket/all");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Ticket>>(content);
        }

        public async Task<Ticket> CreateTicketAsync(Ticket newTicket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newTicket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/Ticket/create", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Ticket>(responseContent);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/Ticket/update", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> UpdateTicketStatusAsync(int id, int statusId)
        {
            var response = await _httpClient.PutAsync($"/Ticket/{id}/status/{statusId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AssignExecutorToTicketAsync(int id, int executorId)
        {
            var response = await _httpClient.PutAsync($"/Ticket/{id}/assign/{executorId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTicketCompletionDateAsync(int ticketId, DateTime? completionDate)
        {
            var json = JsonConvert.SerializeObject(completionDate);
            var content = new StringContent(JsonConvert.SerializeObject(completionDate), Encoding.UTF8, "application/json");
            Console.WriteLine($"JSON content: {json}");
            var response = await _httpClient.PutAsync($"/Ticket/dateComplition/{ticketId}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
