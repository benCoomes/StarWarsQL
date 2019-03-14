using System;
using System.Collections.Generic;
using System.Linq;
using GraphQLParser;
using GraphQL;
using GraphQL.Types;

namespace StarWarsQL
{
    public static class HelloWorld
    {
        public static string Execute()
        {
            var schema = Schema.For(@"
                type Droid {
                    id: String
                    name: String
                }

                type Query {
                    droid(id: String): Droid
                    allDroids: [Droid]
                }
            ", schemaBuilder => {
                schemaBuilder.Types.Include<Query>();
            });

            var json = schema.Execute(executionOptions =>
            {
                executionOptions.Query = "{ allDroids { id name } }";
            });

            return json;
        }
    }

    public class Droid
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class Query
    {
        private Lazy<Dictionary<string, Droid>> droidsLazy = new Lazy<Dictionary<string, Droid>>(() => 
        {
            var droids = new Dictionary<string, Droid>();
            droids.Add("123", new Droid(){ ID = "123", Name = "C3PO"});
            droids.Add("456", new Droid(){ ID = "456", Name = "R2-D2"});
            return droids;
        });

        private Dictionary<string, Droid> droids  { get { return droidsLazy.Value; } }

        [GraphQLMetadata("droid")]
        public Droid Droid(string id)
        {
            droids.TryGetValue(id, out var droid);
            return droid;
        }

        [GraphQLMetadata("allDroids")]
        public IEnumerable<Droid> AllDroids()
        {
            return droids.Values;
        }
    }
}
