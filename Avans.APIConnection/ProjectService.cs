using Avans.UI.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Avans.APIConnection
{
    public class ProjectService
    {
        HttpClient _client;
        public ProjectService(HttpClient  client)
        {
            _client = client;
        }
        public async Task<List<ProjectSelectDTO>> GetProject()
        {

            try
            {
                var value = await _client.GetAsync("getproject");
                if (value.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<ProjectSelectDTO>>(await value.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return null;
        }
    }
}
