using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Builders;
using GraphQL;
using GraphQL.Types;
using Newtonsoft.Json;
using StarWarsQL.Types;

namespace StarWarsQL.SchemaFirst
{
	public static class StarWarsSchema
	{
		private static ISchema _schema = Schema.For(@"
			type Person {
				name: String
				birthYear: String
				eyeColor: String
				gender: String
				hairColor: String
				height: String
				mass: String
				skinColor: String
				homeworld: String
				films: [String]
				species: [String]
				starships: [String]
				vehicles: [String]
			}

			type Query {
				allPersons: [Person]
				person(id: String): Person
			}
		", sb => {
			sb.Types.Include<Query>();
		});

		public static string Execute(string query) => _schema.Execute(eo => { eo.Query = query; });

		private class Query
		{
			private static HttpClient client = new HttpClient();

			[GraphQLMetadata("person")]
			public async Task<Person> GetPersonByID(string id){
				using (var response = await client.GetAsync($"https://swapi.co/api/people/{id}/"))
				{
					if(response.IsSuccessStatusCode)
					{
						var json = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<Person>(json);
					}
					else
					{
						return null;
					}
				}
			}

			[GraphQLMetadata("allPersons")]
			public async Task<IEnumerable<Person>> AllPersons()
			{
				using(var response = await client.GetAsync("https://swapi.co/api/people/"))
				{
					if(response.IsSuccessStatusCode)
					{
						var json = await response.Content.ReadAsStringAsync();
						var jobject = JsonConvert.DeserializeAnonymousType(json, new {Results = new List<Person>()});
						return jobject.Results;
					}
					else
					{
						return new List<Person>();
					}
				}
			}
		}
	}
}