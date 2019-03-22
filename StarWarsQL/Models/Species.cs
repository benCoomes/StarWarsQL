using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarWarsQL.Models
{
	public class Species
	{
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("classification")]
        public string Classification { get; set; }
        
        [JsonProperty("hair_colors")]
        public string HairColors { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("skin_colors")]
        public string SkinColors { get; set; }

        [JsonProperty("eye_colors")]
        public string EyeColors { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("average_height")]
        public string AverageHeight { get; set; }

        [JsonProperty("average_lifespan")]
        public string AverageLifespan { get; set; }
       
        [JsonProperty("homeworld")]
        public string Homeworld { get; set; }
        
        [JsonProperty("people")]
        public IEnumerable<string> People { get; set; }

        [JsonProperty("films")]
        public IEnumerable<string> Films { get; set; }
	}
}