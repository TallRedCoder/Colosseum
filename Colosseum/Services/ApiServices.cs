using Colosseum.Models;
using ColosseumSecret;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Colosseum.Services
{
    class ApiServices
    {
        private string apiKey { get { return "?api_key=" + Secrets.apiKey; } }
        private string apiURL {  get { return "https://api.themoviedb.org/3/"; } }
        private string getNowPlayingMovieRequestUrl(int movieID)
        {
            return String.Format("{0}movie/{1}{2}", apiURL, movieID, apiKey);
        }
        public async Task<List<Movie>> GetNowPlayingMovies()
        {
            List<Movie> list = new List<Movie>();
            Random rnd = new Random();

            var client = new HttpClient();
            for (int i = 0; 10 > i; ++i)
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, getNowPlayingMovieRequestUrl(rnd.Next(1, 1000)));
                var responseMessage = await client.SendAsync(requestMessage);
                var movieResponse = await responseMessage.Content.ReadAsStringAsync();
                Movie movie = JsonConvert.DeserializeObject<Movie>(movieResponse);
                list.Add(movie);
            }
            return list;
        }
    }
}
