using System.Collections.Generic;
using GraphQL.Types;
using StarWarsQL.Models;
using StarWarsQL.Data;

namespace StarWarsQL.Types
{
    public class PlanetType : ObjectGraphType<Planet>
    {
        public PlanetType()
        {
            Field<StringGraphType>("rotationPeriod", resolve: context => context.Source.RotationPeriod);
            Field<StringGraphType>("orbitalPeriod", resolve: context => context.Source.OrbitalPeriod);
            Field<StringGraphType>("population", resolve: context => context.Source.Population);
            Field<StringGraphType>("diameter", resolve: context => context.Source.Diameter);
            Field<StringGraphType>("name", resolve: context => context.Source.Name);
            Field<StringGraphType>("climate", resolve: context => context.Source.Climate);
            Field<StringGraphType>("surfaceWater", resolve: context => context.Source.SurfaceWater);
            Field<StringGraphType>("gravity", resolve: context => context.Source.Gravity);
            Field<StringGraphType>("terrain", resolve: context => context.Source.Terrain);
            Field<ListGraphType<FilmType>>("films", resolve: ResolveFilms);
            Field<ListGraphType<PersonType>>("residents", resolve: ResolveResidents);
        }

        private IEnumerable<Film> ResolveFilms(ResolveFieldContext<Planet> context) 
        {
            return SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult();
        }

        private IEnumerable<Person> ResolveResidents(ResolveFieldContext<Planet> context)
        {
            return SWAPI.ResolveReferences<Person>(context.Source.Residents).GetAwaiter().GetResult();
        }
    }
}