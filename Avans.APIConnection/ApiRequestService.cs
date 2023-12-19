using Avans.UI.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avans.APIConnection
{
    public class ApiRequestService
    {


        HttpClient _httpClient;



        public ApiRequestService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<LoginResultDto> GetToken(EmployeeDTO dto)
        {

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var donendeger = await _httpClient.PostAsync("Auth/login", stringContent);
            if (donendeger.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LoginResultDto>(await donendeger.Content.ReadAsStringAsync());
                //var tokenDegeri= await  donendeger.Content.ReadAsStringAsync();
                //return tokenDegeri;
            }
            return null;

        }

        public async Task<bool> Register(EmployeeDTO dto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var donendeger = await _httpClient.PostAsync("register", stringContent);
            if (donendeger.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
    }
}
