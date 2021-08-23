using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer
{
    internal static class MigrationHelper
    {
        public static void SeedData(MigrationBuilder builder)
        {
            foreach (var movie in InitialData.Movies)
            {
                builder.InsertData(
                    "Movies",
                    new string[] { "Id", "Title", "Description", "ReleaseDate", "ModifiedWhen", "CreatedWhen" },
                    new object[] { movie.Id, movie.Title, movie.Description, movie.ReleaseDate, DateTime.Now, DateTime.Now });
            }

            foreach (var tvShow in InitialData.TVShows)
            {
                builder.InsertData(
                    "TVShows",
                    new string[] { "Id", "Title", "Description", "ReleaseDate", "EndDate", "NumberOfSeasons", "ModifiedWhen", "CreatedWhen" },
                    new object[] { tvShow.Id, tvShow.Title, tvShow.Description, tvShow.ReleaseDate, tvShow.EndDate, tvShow.NumberOfSeasons, DateTime.Now, DateTime.Now });
            }

            foreach (var actor in InitialData.Actors)
            {
                builder.InsertData(
                    "Actors",
                    new string[] { "Id", "Name", "ModifiedWhen", "CreatedWhen" },
                    new object[] { actor.Id, actor.Name, DateTime.Now, DateTime.Now });
            }

            foreach (var user in InitialData.Users)
            {
                builder.InsertData(
                    "Users",
                    new string[] { "Id", "Username", "Email", "Salt", "PassHash", "IsPrivate", "ModifiedWhen", "CreatedWhen" },
                    new object[] { user.Id, user.Username, user.Email, user.Salt, user.PassHash, user.IsPrivate, DateTime.Now, DateTime.Now });
            }

            foreach (var rating in InitialData.Ratings)
            {
                builder.InsertData(
                    "Ratings",
                    new string[] { "Id", "Value", "UserId", "MovieId", "TVShowId", "ModifiedWhen", "CreatedWhen" },
                    new object[] { rating.Id, rating.Value, rating.UserId, rating.MovieId, rating.TVShowId, DateTime.Now, DateTime.Now });
            }

            foreach (var tvShowid in InitialData.TVShowActorsConnections.Keys)
            {
                foreach (var actorId in InitialData.TVShowActorsConnections[tvShowid])
                {
                    builder.InsertData(
                    "ActorTVShow",
                    new string[] { "ActorsId", "StarredInTvShowsId" },
                    new object[] { actorId, tvShowid });
                }
            }

            foreach (var movieId in InitialData.MovieActorsConnections.Keys)
            {
                foreach (var actorId in InitialData.MovieActorsConnections[movieId])
                {
                    builder.InsertData(
                    "ActorMovie",
                    new string[] { "ActorsId", "StarredInMoviesId" },
                    new object[] { actorId, movieId });
                }
            }
        }

        public static void EnsureOneColumnIsNullInRatingTable(MigrationBuilder builder)
        {
            builder.Sql(@"ALTER TABLE Ratings
                          ADD CONSTRAINT OneColumnNull CHECK
                          ((MovieId IS NULL AND SeriesId IS NOT NULL) OR
                          (MovieId IS NOT NULL AND SeriesId IS NULL))");
        }
    }
}
