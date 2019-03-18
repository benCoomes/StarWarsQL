using GraphQL.Types;
using StarWarsQL.Models;
using StarWarsQL.Data;

namespace StarWarsQL.Types
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field<StringGraphType>("Name", resolve: context => context.Source.Name);            
            Field<StringGraphType>("BirthYear", resolve: context => context.Source.BirthYear);            
            Field<StringGraphType>("EyeColor", resolve: context => context.Source.EyeColor);        
            Field<StringGraphType>("Gender", resolve: context => context.Source.Gender);
            Field<StringGraphType>("HairColor", resolve: context => context.Source.HairColor);        
            Field<StringGraphType>("Height", resolve: context => context.Source.Height);        
            Field<StringGraphType>("Mass", resolve: context => context.Source.Mass);        
            Field<StringGraphType>("SkinColor", resolve: context => context.Source.SkinColor);        
            Field<StringGraphType>("Homeworld", resolve: context => context.Source.Homeworld);        
            Field<ListGraphType<FilmType>>("Films", resolve: ResolveFilms);
            Field<ListGraphType<StringGraphType>>("Species", resolve: context => context.Source.Species);
            Field<ListGraphType<StringGraphType>>("Starships", resolve: context => context.Source.Starships);
            Field<ListGraphType<StringGraphType>>("Vehicles", resolve: context => context.Source.Vehicles);
        }

        private object ResolveFilms(ResolveFieldContext<Person> context) 
        {
            return SWAPI.ResolveReferences<Film>(context.Source.Films).GetAwaiter().GetResult();
        }
    }
}