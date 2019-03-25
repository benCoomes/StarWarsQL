using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarWarsQL.Models
{
    public class Film
    {
        [JsonProperty("episode_id")]
        public int EpisodeID { get; set; }
        
        [JsonProperty("opening_crawl")]
        public string OpeningCrawl { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("producer")]
        public string Producer { get; set; }

        [JsonProperty("director")]
        public string Director { get; set; }

        [JsonProperty("starships")]
        public IEnumerable<string> Starships { get; set; }

        [JsonProperty("species")]
        public IEnumerable<string> Species { get; set; }

        [JsonProperty("planets")]
        public IEnumerable<string> Planets { get; set; }

        [JsonProperty("characters")]
        public IEnumerable<string> Characters { get; set; }

        [JsonProperty("vehicles")]
        public IEnumerable<string> Vehicles { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}