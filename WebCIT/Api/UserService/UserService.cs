using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebCIT.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebCIT.Api.UserService
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(string baseAddress)
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

        public async Task<bool> RegisterUserToMicroservice(UserRegistrationModel model)
        {
            try {
                model.PasswordHash = model.Password;
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"User/register", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {response.StatusCode}, Content: {responseContent}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

    //using (var client = new HttpClient())
    //{
    //    client.BaseAddress = new Uri("https://26.240.38.124:7118");
    //    client.DefaultRequestHeaders.Accept.Clear();
    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //    var jsonContent = JsonConvert.SerializeObject(model);
    //    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    //    var response = await client.PostAsync("/User/register", contentString);

    //    return response.IsSuccessStatusCode;
    //}
}
}
