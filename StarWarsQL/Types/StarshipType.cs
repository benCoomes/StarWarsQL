using System.Collections.Generic;
using GraphQL.Types;
using StarWarsQL.Data;
using StarWarsQL.Models;

namespace StarWarsQL.Types
{
    public class StarshipType : ObjectGraphType<Starship>
    {
        public StarshipType()
        {
            Field<StringGraphType>("name", resolve: context => context.Source.Name);
            Field<StringGraphType>("model", resolve: context => context.Source.Model);
            Field<StringGraphType>("starshipClass", resolve: context => context.Source.StarshipClass);
            Field<StringGraphType>("manufacturer", resolve: context => context.Source.Manufacturer);
            Field<StringGraphType>("costInCredits", resolve: context => context.Source.CostInCredits);
            Field<StringGraphType>("length", resolve: context => context.Source.Length);
            Field<StringGraphType>("crew", resolve: context => context.Source.Crew);
            Field<StringGraphType>("passengers", resolve: context => context.Source.Passengers);
            Field<StringGraphType>("maxAtmospheringSpeed", resolve: context => context.Source.MaxAtmospheringSpeed);
            Field<StringGraphType>("hyperdriveRating", resolve: context => context.Source.HyperdriveRating);
            Field<StringGraphType>("mglt", resolve: context => context.Source.MGLT);
            Field<StringGraphType>("cargoCapacity", resolve: context => context.Source.CargoCapacity);
            Field<StringGraphType>("consumables", resolve: context => context.Source.Consumables);
            Field<StringGraphType>("url", resolve: context => context.Source.Url);
            Field<ListGraphType<FilmType>>("films", resolve: ResolveFilms);
            Field<ListGraphType<PersonType>>("pilots", resolve: ResolvePilots);
        }
        
        private IEnumerable<Film> ResolveFilms(ResolveFieldContext<Starship> context)
        {
            return SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult();
        }

        private IEnumerable<Person> ResolvePilots(ResolveFieldContext<Starship> context)
        {
            return SWAPI.ResolveReferences<Person>(context.Source.Pilots).GetAwaiter().GetResult();
        }
    }
}