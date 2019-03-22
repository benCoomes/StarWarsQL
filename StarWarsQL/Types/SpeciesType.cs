using System.Collections.Generic;
using GraphQL.Types;
using StarWarsQL.Models;
using StarWarsQL.Data;

namespace StarWarsQL.Types
{
    public class SpeciesType : ObjectGraphType<Species>
    {
        public SpeciesType()
        {
            Field<StringGraphType>("name", resolve: context => context.Source.Name);
            Field<StringGraphType>("classification", resolve: context => context.Source.Classification);
            Field<StringGraphType>("hairColors", resolve: context => context.Source.HairColors);
            Field<StringGraphType>("language", resolve: context => context.Source.Language);
            Field<StringGraphType>("skinColors", resolve: context => context.Source.SkinColors);
            Field<StringGraphType>("eyeColors", resolve: context => context.Source.EyeColors);
            Field<StringGraphType>("designation", resolve: context => context.Source.Designation);
            Field<StringGraphType>("averageHeight", resolve: context => context.Source.AverageHeight);
            Field<StringGraphType>("averageLifespan", resolve: context => context.Source.AverageLifespan);
            Field<PlanetType>("homeworld", resolve: ResolveHomeworld);
            Field<ListGraphType<PersonType>>("people", resolve: ResolvePeople);
            Field<ListGraphType<FilmType>>("films", resolve: ResolveFilms);
        }

        private IEnumerable<Film> ResolveFilms(ResolveFieldContext<Species> context) 
        {
            return SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult();
        }

        private IEnumerable<Person> ResolvePeople(ResolveFieldContext<Species> context) 
        {
            return SWAPI.ResolveReferences<Person>(context.Source.People).GetAwaiter().GetResult();
        }

        private Planet ResolveHomeworld(ResolveFieldContext<Species> context) 
        {
            return SWAPI.ResolveReference<Planet>(context.Source.Homeworld).GetAwaiter().GetResult();
        }
    }
}