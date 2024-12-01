using CITOGAU.Classes.Department;
using CITOGAU.Classes.RequestType;
using CITOGAU.Interface.RequestType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.ApiContext.Services
{
    public class RequestTypesService: IRequestTypeService
    {
        private readonly HttpClient _httpClient;

        public RequestTypesService(string baseAddress)
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

        public async Task<List<RequestTypes>> GetAllRequestsAsync()
        {
            var response = await _httpClient.GetAsync("/RequestType/allRequest");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RequestTypes>>(content);
        }
    }
}
