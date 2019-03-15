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

			type Film {
				episodeID: Int
				openingCrawl: String
				releaseDate: String
				title: String
				producer: String
				director: String
				starships: [String]
				species: [String]
				planets: [String]
				characters: [String]
				vehicles: [String]
			}

			type Query {
				person(id: String): Person
				allPersons: [Person]
				film(id: String): Film
				allFilms: [Film]
			}
		", sb => {
			sb.Types.Include<Query>();
		});

		public static string Execute(string query) => _schema.Execute(eo => 
		{ 
			eo.Query = query; 
			eo.ExposeExceptions = true; 
		});

		private class Query
		{
			private static HttpClient client = new HttpClient();

			[GraphQLMetadata("person")]
			public Task<Person> GetPersonByID(string id){
				return GetResourceByID<Person>("people", id);
			}

			[GraphQLMetadata("allPersons")]
			public Task<IEnumerable<Person>> AllPersons()
			{
				return GetAll<Person>("people");
			}

			[GraphQLMetadata("film")]
			public Task<Film> GetFilmByID(string id)
			{
				return GetResourceByID<Film>("films", id);
			}

			[GraphQLMetadata("allFilms")]
			public Task<IEnumerable<Film>> AllFilms()
			{
				return GetAll<Film>("films");
			}

			private async Task<T> GetResourceByID<T>(string resourceName, string id)
			{
				using(var response = await client.GetAsync($"https://swapi.co/api/{resourceName}/{id}"))
				{
					response.EnsureSuccessStatusCode();
					
					var json = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<T>(json);
				}
			}

			private async Task<IEnumerable<T>> GetAll<T>(string resourceName)
			{
				var results = new List<T>();
				var next = $"https://swapi.co/api/{resourceName}";

				do
				{
					
					using(var response = await client.GetAsync(next))
					{
						response.EnsureSuccessStatusCode();

						var json = await response.Content.ReadAsStringAsync();
						var jobject = JsonConvert.DeserializeAnonymousType(json, new 
						{
							Results = new List<T>(),
							Next = ""
						});
						
						results.AddRange(jobject.Results);
						next = jobject.Next;
					}

				} while( !String.IsNullOrWhiteSpace(next) );

				return results;
			}
		}
	}
}