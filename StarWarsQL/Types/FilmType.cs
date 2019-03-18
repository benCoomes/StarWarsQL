using GraphQL.Types;
using StarWarsQL.Models;

namespace StarWarsQL.Types
{
    public class FilmType : ObjectGraphType<Film>
    {
        public FilmType()
        {
            Field(f => f.Title);
        }
    }
}