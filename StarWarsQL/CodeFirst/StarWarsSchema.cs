using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using StarWarsQL.Types;
using Newtonsoft.Json;

namespace StarWarsQL.CodeFirst
{
    public class StarWarsSchema
    {
        private static Schema _schema = new Schema { Query = new StarWarsQuery() };

        public static string Execute(string query) => _schema.Execute(eo => 
		{ 
			eo.Query = query; 
			eo.ExposeExceptions = true; 
		});

        private class StarWarsQuery : ObjectGraphType
        {
            public StarWarsQuery()
            {
                Field<PersonType>(
                  "person",
                    arguments: new QueryArguments(
                        new QueryArgument<IdGraphType> { Name = "id" }
                    ),
                    resolve: context =>
                    {
                        var id = context.GetArgument<string>("id");
                        return SWAPI.GetByID<Person>("people", id);
                    }
                );
            } 
        }

        private class PersonType : ObjectGraphType<Person>
        {
            public PersonType()
            {
                Field<StringGraphType>(
                     "Name",
                     resolve: context => context.Source.Name
                 );
                
                Field<StringGraphType>(
                     "BirthYear",
                     resolve: context => context.Source.BirthYear
                );
                
                Field<StringGraphType>(
                     "EyeColor",
                     resolve: context => context.Source.EyeColor
                );
            
                Field<StringGraphType>(
                     "Gender",
                     resolve: context => context.Source.Gender
                );

                Field<StringGraphType>(
                     "HairColor",
                     resolve: context => context.Source.HairColor
                );
            
                Field<StringGraphType>(
                     "Height",
                     resolve: context => context.Source.Height
                );
            
                Field<StringGraphType>(
                     "Mass",
                     resolve: context => context.Source.Mass
                );
            
                Field<StringGraphType>(
                     "SkinColor",
                     resolve: context => context.Source.SkinColor
                );
            
                Field<StringGraphType>(
                     "Homeworld",
                     resolve: context => context.Source.Homeworld
                );
            
                Field<ListGraphType<FilmType>>(
                    "Films",
                    resolve: context => SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult()
                );
            
                Field<ListGraphType<StringGraphType>>(
                    "Species",
                    resolve: context => context.Source.Species
                );
            
                Field<ListGraphType<StringGraphType>>(
                    "Starships",
                    resolve: context => context.Source.Starships
                );
            
                Field<ListGraphType<StringGraphType>>(
                    "Vehicles",
                    resolve: context => context.Source.Vehicles
                );
            }
        }

        private class FilmType : ObjectGraphType<Film>
        {
            public FilmType()
            {
                Field(f => f.Title);
            }
        }
    }  

    public static class SWAPI
    {
        private static HttpClient client = new HttpClient { BaseAddress = new Uri("https://swapi.co") };

        public static async Task<T> GetByID<T>(string resourceName, string id)
        {
            Console.WriteLine("Requesting...");
            using(var response = await client.GetAsync($"/api/{resourceName}/{id}"))
            {
                Console.WriteLine("Requested " + response.RequestMessage.RequestUri);
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public static async Task<T> ResolveReference<T>(string url)
        {
            Console.WriteLine("Requesting...");
            using(var response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public static async Task<IEnumerable<T>> ResolveReferences<T>(IEnumerable<string> urls)
        {
            List<T> results = new List<T>();
            // await Task.Factory.StartNew(() => Parallel.ForEach(urls, async url => 
            // {
            //     var result = await ResolveReference<T>(url);
            //     results.Add(result);
            // }));

            foreach(var url in urls)
            {
                results.Add(await ResolveReference<T>(url));
            }


            return results;
        }
    }
}