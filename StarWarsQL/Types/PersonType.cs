using System.Collections.Generic;
using GraphQL.Types;
using StarWarsQL.Models;
using StarWarsQL.Data;

namespace StarWarsQL.Types
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field<StringGraphType>("name", resolve: context => context.Source.Name);            
            Field<StringGraphType>("birthYear", resolve: context => context.Source.BirthYear);            
            Field<StringGraphType>("eyeColor", resolve: context => context.Source.EyeColor);        
            Field<StringGraphType>("gender", resolve: context => context.Source.Gender);
            Field<StringGraphType>("hairColor", resolve: context => context.Source.HairColor);        
            Field<StringGraphType>("height", resolve: context => context.Source.Height);        
            Field<StringGraphType>("mass", resolve: context => context.Source.Mass);        
            Field<StringGraphType>("skinColor", resolve: context => context.Source.SkinColor);        
            Field<StringGraphType>("homeworld", resolve: context => context.Source.Homeworld);        
            Field<ListGraphType<FilmType>>("films", resolve: ResolveFilms);
            Field<ListGraphType<SpeciesType>>("species", resolve: ResolveSpecies);
            Field<ListGraphType<StarshipType>>("starships", resolve: ResolveStarships);
            Field<ListGraphType<VehicleType>>("vehicles", resolve: ResolveVehicles);
        }

        private IEnumerable<Film> ResolveFilms(ResolveFieldContext<Person> context) 
        {
            return SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult();
        }

        private IEnumerable<Starship> ResolveStarships(ResolveFieldContext<Person> context)
        {
            return SWAPI.ResolveReferences<Starship>(context.Source.Starships).GetAwaiter().GetResult();
        }

        private IEnumerable<Vehicle> ResolveVehicles(ResolveFieldContext<Person> context)
        {
            return SWAPI.ResolveReferences<Vehicle>(context.Source.Vehicles).GetAwaiter().GetResult();
        }

        private IEnumerable<Species> ResolveSpecies(ResolveFieldContext<Person> context)
        {
            return SWAPI.ResolveReferences<Species>(context.Source.Species).GetAwaiter().GetResult();
        }
    }
}