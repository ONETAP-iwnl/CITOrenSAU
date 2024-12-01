using CITOGAU.Classes.Department;
using CITOGAU.Classes.Tickets;
using CITOGAU.Interface.Department;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.ApiContext.Services
{
    public class DepartmentService: IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(string baseAddress)
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

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            var response = await _httpClient.GetAsync("/Department/allDepartment");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Department>>(content);
        }

        public async Task<List<DepartmentType>> GetAllDepartmentTypeAsync()
        {
            var response = await _httpClient.GetAsync("/Department/allDepartmentType");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DepartmentType>>(content);
        }
    }
}
