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
        private string MakeYoutubeUrlEmbeded(string youtubeKey) { return String.Format("https://www.youtube.com/embed/{0}", youtubeKey); }
        private enum MovieRequestDetail
        {
            Movie,
            Trailer
        }
        private string GetRequestUrlMovie(int movieID)
        {
            return String.Format("{0}movie/{1}{2}", apiURL, movieID, apiKey);
        }
        private string GetRequestUrlMovieTrailer(int movieID)
        {
            return String.Format("{0}movie/{1}/videos{2}", apiURL, movieID, apiKey);
        }
        public async Task<List<Movie>> GetNowPlayingMovies()
        {
            List<Movie> list = new List<Movie>();
            Random rnd = new Random();

            var client = new HttpClient();
            while(10 > list.Count)
            {
                int movieID = rnd.Next(1, 1000);
                string response = await GetMovieData(client, movieID, MovieRequestDetail.Movie);
                ApiMovie apiMovie = JsonConvert.DeserializeObject<ApiMovie>(response);
                if (null != apiMovie && !string.IsNullOrEmpty(apiMovie.title))
                {
                    Movie movie = ConvertToModelMovie(apiMovie);

                    response = await GetMovieData(client, movieID, MovieRequestDetail.Trailer);
                    ApiTrailer trailer = JsonConvert.DeserializeObject<ApiTrailer>(response);
                    AddTrailerData(movie, trailer);

                    list.Add(movie);
                }

            }
            return list;
        }

        private async Task<string> GetMovieData(HttpClient client, int movieID, MovieRequestDetail detail)
        {
            string request = MovieRequestDetail.Trailer == detail ? GetRequestUrlMovieTrailer(movieID) : GetRequestUrlMovie(movieID);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, request);
            var responseMessage = await client.SendAsync(requestMessage);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            return responseContent;
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

        private void AddTrailerData(Movie movie, ApiTrailer trailer)
        {
            if (null == trailer || null == trailer.results)
                return;

            foreach(ApiTrailerItem item in trailer.results)
            {
                if(item.site.Equals("youtube", StringComparison.InvariantCultureIgnoreCase))
                {
                    movie.trailer = MakeYoutubeUrlEmbeded(item.key);
                    return;
                }
            }
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
