using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarWarsQL.Models
{
	public class Person
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		
		[JsonProperty("birth_year")]
		public string BirthYear { get; set; }
		
		[JsonProperty("eye_color")]
		public string EyeColor { get; set; }
	
		[JsonProperty("gender")]
		public string Gender { get; set; }

		[JsonProperty("hair_color")]
		public string HairColor { get; set; }
	
		[JsonProperty("height")]
		public string Height { get; set; }
	
		[JsonProperty("mass")]
		public string Mass { get; set; }
	
		[JsonProperty("skin_color")]
		public string SkinColor { get; set; }
	
		[JsonProperty("homeworld")]
		public string Homeworld { get; set; }
	
		[JsonProperty("films")]
		public IEnumerable<string> Films { get; set; }
	
		[JsonProperty("species")]
		public IEnumerable<string> Species { get; set; }
	
		[JsonProperty("starships")]
		public IEnumerable<string> Starships { get; set; }
	
		[JsonProperty("vehicles")]
		public IEnumerable<string> Vehicles { get; set; }

		[JsonProperty("url")]
        public string Url { get; set; }
	}
}