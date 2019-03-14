using System;
using StarWarsQL.SchemaFirst;

namespace StarWarsQLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StarWarsSchema.Execute("{ person(id: \"4\") { name gender height skinColor hairColor homeworld vehicles} }"));
        }
    }
}
