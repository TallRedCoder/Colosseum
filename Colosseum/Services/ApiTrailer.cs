using System.Collections.Generic;

namespace Colosseum.Services
{
    public class ApiTrailerItem
    {
        public string key { get; set; }
        public string site { get; set; }
    }
    class ApiTrailer
    {
        public List<ApiTrailerItem> results { get; set; }
    }
}
