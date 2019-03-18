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
            Field<ListGraphType<StringGraphType>>("species", resolve: context => context.Source.Species);
            Field<ListGraphType<StringGraphType>>("starships", resolve: context => context.Source.Starships);
            Field<ListGraphType<StringGraphType>>("vehicles", resolve: context => context.Source.Vehicles);
            Field<ListGraphType<PersonType>>("characters", resolve: ResolveCharacters);
            Field<ListGraphType<StringGraphType>>("planets", resolve: context => context.Source.Planets);
        }

        private object ResolveCharacters(ResolveFieldContext<Film> context)
        {
            return SWAPI.ResolveReferences<Person>(context.Source.Characters).GetAwaiter().GetResult();
        }
    }
}