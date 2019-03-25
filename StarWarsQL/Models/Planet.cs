using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarWarsQL.Models
{
	public class Planet
	{
        [JsonProperty("rotation_period")]
        public string RotationPeriod { get; set; }
        
        [JsonProperty("orbital_period")]
        public string OrbitalPeriod { get; set; }
        
        [JsonProperty("population")]
        public string Population { get; set; }
        
        [JsonProperty("diameter")]
        public string Diameter { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("climate")]
        public string Climate { get; set; }
        
        [JsonProperty("surface_water")]
        public string SurfaceWater { get; set; }
        
        [JsonProperty("gravity")]
        public string Gravity { get; set; }
        
        [JsonProperty("terrain")]
        public string Terrain { get; set; }
        
        [JsonProperty("films")]
        public IEnumerable<string> Films { get; set; }
    
        [JsonProperty("residents")]
        public IEnumerable<string> Residents { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}