using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using StarWarsQL.Types;
using StarWarsQL.Models;
using StarWarsQL.Data;
using Newtonsoft.Json;

namespace StarWarsQL.CodeFirst
{
    public class StarWarsSchema
    {
        private static Schema _schema = new Schema { Query = new StarWarsQuery() };

        public static string Execute(string query) => _schema.Execute(eo => 
		{ 
			eo.Query = query; 
			eo.ExposeExceptions = true; 
		});

        private class StarWarsQuery : ObjectGraphType
        {
            public StarWarsQuery()
            {
                FieldFromID<PersonType, Person>("person", "people");
                FieldFromID<FilmType, Film>("film", "films");
                FieldFromID<StarshipType, Starship>("starship", "starships");
                FieldFromID<VehicleType, Vehicle>("vehicle", "vehicles");
                FieldFromID<SpeciesType, Species>("species", "species");
                FieldFromID<PlanetType, Planet>("planet", "planets");
            } 

            private void FieldFromID<ResolveType, ModelType>(string fieldName, string resourcePath) where ResolveType : IGraphType
            {
                Field<ResolveType>(
                    fieldName,
                    arguments: new QueryArguments(
                        new QueryArgument<IdGraphType> { Name = "id" }
                    ),
                    resolve: context =>
                    {
                        var id = context.GetArgument<string>("id");
                        return SWAPI.GetByID<ModelType>(resourcePath, id);
                    }
                );
            }
        }
    }  
}