using StarWarsApiCSharp;
using SwapiClient.Application;
using SwapiClient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SwapiClient.Infrastructure
{
    public class SwapiHttpClientHelper: ISwapiHttpClientHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        public SwapiHttpClientHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _httpClient = _httpClientFactory.CreateClient("Swapi");
        }
        public async Task<SwapiPeopleResponse> GetPeople(int pageNo)
        {
            //var res = await _httpClient.GetStringAsync($"people?page={pageNo}");
            var httpResponseMessage = await _httpClient.GetAsync(
            $"people?page={pageNo}");

            SwapiPeopleResponse resp = null;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStr = await httpResponseMessage.Content.ReadAsStringAsync();
                resp = Newtonsoft.Json.JsonConvert.DeserializeObject<SwapiPeopleResponse>(contentStr);


                //Is try/catch required here?
            }
            return resp;
        }
    }
}
