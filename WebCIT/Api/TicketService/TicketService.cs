    using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebCIT.Classed.Ticket;

namespace WebCIT.Api.TicketService
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


        public async Task<Ticket> GetTicket(int ID_Ticket)
        {
            var response = await _httpClient.GetAsync($"/TicketContoller/{ID_Ticket}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Ticket>(content);
            }

            return null;
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

        public async Task UpdateStatus(Ticket ticket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("TicketContoller/updateStatus", content);

            response.EnsureSuccessStatusCode();
        }

    }
}
