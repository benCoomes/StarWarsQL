using System;
using System.Linq;
using System.Collections.Generic;
using StarWarsQL.CodeFirst;

namespace StarWarsQLConsole
{
    class Program
    {
        static Dictionary<string, string> NamedQueries = new Dictionary<string, string>()
        {
            {"darthVadar", "{ person(id: \"4\") { name gender height skinColor hairColor homeworld films { title } } }"},
            {"allPeople", "{ allPersons { name gender height skinColor hairColor homeworld vehicles } }"}
        };

        private static string ResolveQuery(string[] args)
        {
            string query = null;
            
            if(args.Length >= 1)
            {
                var queryArg = args[0];
                if(queryArg.StartsWith('{'))
                {
                    query = queryArg;
                }
                else
                {
                    bool wasFound = NamedQueries.TryGetValue(queryArg, out var namedQuery);
                    if(wasFound)
                    {
                        query = namedQuery;
                    }
                }
            }
            
            return query;
        }

        static void Main(string[] args)
        {
            var query = ResolveQuery(args);
           
            if(string.IsNullOrWhiteSpace(query))
            {
                Console.WriteLine("No query argument provided, or failed to parse query. Provide either a query or the named of a stored query. Stored queries: \n");
                foreach(var name in NamedQueries.Keys) Console.WriteLine(name);
                return;
            }

            Console.WriteLine(StarWarsSchema.Execute(query));
        }
    }
}