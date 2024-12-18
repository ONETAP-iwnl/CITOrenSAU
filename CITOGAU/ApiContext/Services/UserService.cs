using CITOGAU.Classes.Authors;
using CITOGAU.Classes.Executors;
using CITOGAU.Classes.Users;
using CITOGAU.Interface.Authors;
using CITOGAU.Interface.Executors;
using CITOGAU.Interface.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CITOGAU.ApiContext
{
    public class UserService: IUserService, IAuthorsService, IExecutorsService
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

        public async Task<List<UserResponse>> GetAllUserAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/User/allUser");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserResponse>>(content);
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

        public async Task<List<Authors>> GetAllAuthorsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Authors/allAuthors");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Authors>>(content);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return null;
        }

        public async Task<List<Executors>> GetAllExecutorsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Executors/allExecutors");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Executors>>(content);
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

        public async Task<int?> GetAuthorByUserIdAsync(int  userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Authors/user/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var author = JsonConvert.DeserializeObject<Authors>(content);
                    return author?.ID_Author;
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

        public async Task<int?> GetExecutorsByUserIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Executors/user/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var executors = JsonConvert.DeserializeObject<Executors>(content);
                    return executors?.ID_Executor;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return null;
        }
    }
}
