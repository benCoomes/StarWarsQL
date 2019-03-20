using System.Collections.Generic;
using GraphQL.Types;
using StarWarsQL.Data;
using StarWarsQL.Models;

namespace StarWarsQL.Types
{
    public class VehicleType : ObjectGraphType<Vehicle>
    {
        public VehicleType()
        {
            Field<StringGraphType>("name", resolve: context => context.Source.Name);
            Field<StringGraphType>("model", resolve: context => context.Source.Model);
            Field<StringGraphType>("vehicleClass", resolve: context => context.Source.VehicleClass);
            Field<StringGraphType>("manufacturer", resolve: context => context.Source.Manufacturer);
            Field<StringGraphType>("length", resolve: context => context.Source.Length);
            Field<StringGraphType>("costInCredits", resolve: context => context.Source.CostInCredits);
            Field<StringGraphType>("crew", resolve: context => context.Source.Crew);
            Field<StringGraphType>("passengers", resolve: context => context.Source.Passengers);
            Field<StringGraphType>("maxAtmospheringSpeed", resolve: context => context.Source.MaxAtmospheringSpeed);
            Field<StringGraphType>("cargoCapacity", resolve: context => context.Source.CargoCapacity);
            Field<StringGraphType>("consumables", resolve: context => context.Source.Consumables);
            Field<ListGraphType<FilmType>>("films", resolve: ResolveFilms);
            Field<ListGraphType<PersonType>>("pilots", resolve: ResolvePilots);
        }

        private IEnumerable<Film> ResolveFilms(ResolveFieldContext<Vehicle> context)
        {
            return SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult();
        }

        private IEnumerable<Person> ResolvePilots(ResolveFieldContext<Vehicle> context)
        {
            return SWAPI.ResolveReferences<Person>(context.Source.Pilots).GetAwaiter().GetResult();
        }
    }
}