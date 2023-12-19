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
    public class TitleService
    {
        HttpClient _client;

        public TitleService(HttpClient client)
        {
            _client = client;
        }


        public async Task<List<TitleDTO>> GetTitle()
        {

            try
            {
                var value = await _client.GetAsync("gettitle");
                if (value.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<TitleDTO>>(await value.Content.ReadAsStringAsync());
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
