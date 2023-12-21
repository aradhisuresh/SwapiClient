using StarWarsApiCSharp;
using SwapiClient.Application;
using SwapiClient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace SwapiClient.Infrastructure
{
    public class SwapiHttpClientHelper: ISwapiHttpClientHelper
    {
        private readonly HttpClient _httpClient;
        public SwapiHttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://swapi.dev/api/");
        }
        public async ValueTask<SwapiPeopleResponse> GetPeople(int pageNo)
        {
            //var res = await _httpClient.GetStringAsync($"people?page={pageNo}");
            return await _httpClient.GetFromJsonAsync<SwapiPeopleResponse>($"people?page={pageNo}");
        }
    }
}
