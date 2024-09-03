using GlobalAeroTechnicalTest.Model;
using RestSharp;
using Newtonsoft.Json;

namespace GlobalAeroTechnicalTest.Data
{
    public class AirportDatabase : IAirportDatabase
    {
        public IList<Airport> GetByCountry(string countryCode)
        {
            var queryPart = $"country={countryCode}";
            var raw = GetRawContent(queryPart);
            return JsonConvert.DeserializeObject<List<Airport>>(raw ?? "");
        }

        public IList<Airport> LondonAirports
        {
            get
            {
                var queryPart = "v1/airports?country=GB&region=England&city=London";
                var raw = GetRawContent(queryPart);
                return JsonConvert.DeserializeObject<List<Airport>>(raw ?? "");
            }
        }

        private string GetRawContent(string queryPart)
        {
            var client = new RestClient("https://airports-by-api-ninjas.p.rapidapi.com/");
            var request = new RestRequest($"v1/airports?{queryPart}");
            request.AddHeader("x-rapidapi-key", "368669ef32msh88075ec3406582fp1bc8c3jsn2585f16f8d20");
            request.AddHeader("x-rapidapi-host", "airports-by-api-ninjas.p.rapidapi.com");
            RestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
