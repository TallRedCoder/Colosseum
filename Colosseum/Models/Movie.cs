using System;
using System.Collections.Generic;
using System.Linq;

namespace Colosseum.Models
{
	public class Movie
	{
		public string title { get; set; }
		public string tagline { get; set; }
		public string description { get; set; }
		public float rating { get; set; }
		public int duration { get; set; }
		public string durationText { get { return duration.ToString() + " min"; } }
		public DateTime releaseDate { get; set; }
		public string releaseDateShort { get { return releaseDate.ToShortDateString(); } }
		public string poster { get; set; }
		public List<string> languages { get; set; }
		public List<string> genres { get; set; }
		public string languagesList { get { return listToString(languages); } }
		public string genresList { get { return listToString(genres); } }
		public string trailer { get; set; }

		private string listToString(List<string> list) { return string.Join(", ", list); }
	}
}
