using Colosseum.Models;
using ColosseumSecret;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Colosseum.Services
{
    class ApiServices
    {
        private string apiKey { get { return "?api_key=" + Secrets.apiKey; } }
        private string apiURL { get { return "https://api.themoviedb.org/3/"; } }
        private string apiImgURL { get { return "https://image.tmdb.org/t/p/w500/"; } }
        private string MakeImgUrl(string imagePath) { return string.IsNullOrEmpty(imagePath) ? "" : apiImgURL + imagePath; }
        private string GetNowPlayingMovieRequestUrl(int movieID)
        {
            return String.Format("{0}movie/{1}{2}", apiURL, movieID, apiKey);
        }
        public async Task<List<Movie>> GetNowPlayingMovies()
        {
            List<Movie> list = new List<Movie>();
            Random rnd = new Random();

            var client = new HttpClient();
            while(10 > list.Count)
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, GetNowPlayingMovieRequestUrl(rnd.Next(1, 1000)));
                var responseMessage = await client.SendAsync(requestMessage);
                var movieResponse = await responseMessage.Content.ReadAsStringAsync();
                ApiMovie movie = JsonConvert.DeserializeObject<ApiMovie>(movieResponse);
                if(null != movie && !string.IsNullOrEmpty(movie.title))
                    list.Add(ConvertToModelMovie(movie));
            }
            return list;
        }

        private Movie ConvertToModelMovie(ApiMovie apiMovie)
        {
            Movie movie = new Movie();

            movie.title = apiMovie.title;
            movie.tagline = apiMovie.tagline;
            movie.duration = apiMovie.runtime;
            movie.description = apiMovie.overview;
            movie.rating = apiMovie.vote_average;
            movie.releaseDate = apiMovie.release_date;
            movie.poster = MakeImgUrl(apiMovie.poster_path);
            movie.languages = ConvertToTextList(apiMovie.spoken_languages);
            movie.genres = ConvertToTextList(apiMovie.genres);

            return movie;
        }

        private List<string> ConvertToTextList<T>(List<T> list)
        {
            if (null == list)
                return new List<string>();

            PropertyInfo pi = typeof(T).GetProperty("name");
            return list.ConvertAll(x => pi.GetValue(x) as string);
        }
    }
}
