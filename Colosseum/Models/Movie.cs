using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colosseum.Models
{
	public class SpokenLanguage
	{
		public string name { get; set; }
	}

	public class Genres
	{
		public string name { get; set; }
	}
	public class Movie
	{
		public string title { get; set; }
		public int runtime { get; set; }
		public List<SpokenLanguage> spoken_languages { get; set; }
		public string language { get { return spoken_languages.First().name; } }
		public List<Genres> genres { get; set; }
		public string genresText { get { return genres.Aggregate("", (current, next) => current + ", " + next.name); } }
		public DateTime release_date { get; set; }

		public string overview { get; set; }
		public string tagline { get; set; }

		public float vote_average { get; set; }
		public string poster_path { get; set; }
		public string poster { get { return "https://image.tmdb.org/t/p/w500/" + poster_path; } }

/*	"popularity": 52.173,
	
	"tagline": "Mischief. Mayhem. Soap.",*/
    }
}
