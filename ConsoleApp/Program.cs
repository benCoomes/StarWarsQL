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
            {"darthVadar", "{ person(id: \"4\") { name gender height skinColor hairColor homeworld films { title } starships { name } species { name } } }"},
            {"allPeople", "{ allPersons { name gender height skinColor hairColor homeworld vehicles { name } } }"},
            {"aNewHope", "{ film(id: \"1\") { title producer director characters { name } vehicles { name } starships { name } planets { name } } }"},
            {"slave1", "{ starship(id: \"21\") { name model starshipClass manufacturer length crew passengers consumables films {title} pilots {name} } }"},
            {"snowspeeder", "{ vehicle(id: \"14\") { name model vehicleClass manufacturer length crew passengers consumables films { title } pilots { name } } }"},
            {"hutt", "{ species(id: \"5\") { name classification averageHeight skinColors homeworld people { name } films { title } } }"},
            {"endor", "{planet(id: \"7\") { name diameter climate terrain residents { name } films { title } } }"},
            {"killTheAPI", "{ film(id: \"1\") { title characters { name homeworld { name residents { name } } } } }"}
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