using System;
using System.Collections.Generic;

namespace Colosseum.Services
{
	public class SpokenLanguage
	{
		public string name { get; set; }
	}

	public class Genre
	{
		public string name { get; set; }
	}
	public class ApiMovie
	{
		public string title { get; set; }
		public int runtime { get; set; }
		public List<SpokenLanguage> spoken_languages { get; set; }
		public List<Genre> genres { get; set; }
		public DateTime release_date { get; set; }
		public string overview { get; set; }
		public string tagline { get; set; }
		public float vote_average { get; set; }
		public string poster_path { get; set; }
	}
}
