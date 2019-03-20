using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarWarsQL.Models
{
    public class Vehicle
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("model")]
        public string Model { get; set; }
        
        [JsonProperty("vehicle_class")]
        public string VehicleClass { get; set; }
        
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }
        
        [JsonProperty("length")]
        public string Length { get; set; }
        
        [JsonProperty("cost_in_credits")]
        public string CostInCredits { get; set; }
        
        [JsonProperty("crew")]
        public string Crew { get; set; }
        
        [JsonProperty("passengers")]
        public string Passengers { get; set; }
        
        [JsonProperty("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }
        
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }
        
        [JsonProperty("consumables")]
        public string Consumables { get; set; }
        
        [JsonProperty("films")]
        public IEnumerable<string> Films { get; set; }
        
        [JsonProperty("pilots")]
        public IEnumerable<string> Pilots { get; set; }
    }
}