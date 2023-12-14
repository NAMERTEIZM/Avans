using Avans.UI.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avans.APIConnection
{
    public class AdvanceService
    {
        HttpClient _client;
        public AdvanceService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<AdvanceSelectDTO>> GetAdvance()
        {

            try
             {
                var value = await _client.GetAsync("getadvance");
                if (value.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<AdvanceSelectDTO>>(await value.Content.ReadAsStringAsync());
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
