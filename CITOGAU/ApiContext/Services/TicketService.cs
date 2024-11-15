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

namespace CITOGAU.ApiContext
{
    public class TicketService
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

        public async Task<List<Ticket>> GetAllTicket()
        {
            try
            {
                var response = await _httpClient.GetAsync("/TicketContoller/all");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Ticket>>(content);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return null;
        }
        public async Task<List<Ticket>> CreateTicket(Ticket newTicket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newTicket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/TicketContoller/createTicket", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<Ticket>(responseContent);
                return new List<Ticket> { userResponse };
            }
            Console.WriteLine($"Ticket failed: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
            return null;
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("TicketContoller/updateTicket", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
