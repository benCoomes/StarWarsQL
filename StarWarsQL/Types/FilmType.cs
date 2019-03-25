using System.Collections.Generic;
using GraphQL.Types;
using StarWarsQL.Data;
using StarWarsQL.Models;

namespace StarWarsQL.Types
{
    public class FilmType : ObjectGraphType<Film>
    {
        public FilmType()
        {
            Field<StringGraphType>("title", resolve: context => context.Source.Title);
            Field<IntGraphType>("episodeId", resolve: context => context.Source.EpisodeID);
            Field<StringGraphType>("opening_crawl", resolve: context => context.Source.OpeningCrawl);
            Field<StringGraphType>("director", resolve: context => context.Source.Director);
            Field<StringGraphType>("producer", resolve: context => context.Source.Producer);
            Field<StringGraphType>("release_date", resolve: context => context.Source.ReleaseDate);
            Field<StringGraphType>("url", resolve: context => context.Source.Url);
            Field<ListGraphType<SpeciesType>>("species", resolve: ResolveSpecies);
            Field<ListGraphType<StarshipType>>("starships", resolve: ResolveStarships);
            Field<ListGraphType<VehicleType>>("vehicles", resolve: ResolveVehicles);
            Field<ListGraphType<PersonType>>("characters", resolve: ResolveCharacters);
            Field<ListGraphType<PlanetType>>("planets", resolve: ResolvePlanets);
        }

        private IEnumerable<Person> ResolveCharacters(ResolveFieldContext<Film> context)
        {
            return SWAPI.ResolveReferences<Person>(context.Source.Characters).GetAwaiter().GetResult();
        }

        private IEnumerable<Starship> ResolveStarships(ResolveFieldContext<Film> context)
        {
            return SWAPI.ResolveReferences<Starship>(context.Source.Starships).GetAwaiter().GetResult();
        }

        private IEnumerable<Vehicle> ResolveVehicles(ResolveFieldContext<Film> context)
        {
            return SWAPI.ResolveReferences<Vehicle>(context.Source.Vehicles).GetAwaiter().GetResult();
        }

        private IEnumerable<Species> ResolveSpecies(ResolveFieldContext<Film> context)
        {
            return SWAPI.ResolveReferences<Species>(context.Source.Species).GetAwaiter().GetResult();
        }

        private IEnumerable<Planet> ResolvePlanets(ResolveFieldContext<Film> context)
        {
            return SWAPI.ResolveReferences<Planet>(context.Source.Planets).GetAwaiter().GetResult();
        }
    }
}