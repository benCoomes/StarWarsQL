using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarWarsQL.Data
{
    public static class SWAPI
    {
        private static HttpClient client = new HttpClient { BaseAddress = new Uri("https://swapi.co") };

        public static async Task<T> GetByID<T>(string resourceName, string id)
        {
            using(var response = await client.GetAsync($"/api/{resourceName}/{id}"))
            {
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public static async Task<IEnumerable<T>> GetAll<T>(string resourceName)
        {
            var results = new List<T>();
            var next = $"/api/{resourceName}";

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

        public static async Task<T> ResolveReference<T>(string url)
        {
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