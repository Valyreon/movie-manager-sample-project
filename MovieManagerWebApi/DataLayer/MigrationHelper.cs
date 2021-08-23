using System;
using Domain;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer
{
    internal static class MigrationHelper
    {
        public static void SeedData(MigrationBuilder migrationBuilder)
        {
            var shawshankRedemption = new Movie()
            {
                Title = "The Shawshank Redemption",
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                ReleaseDate = new DateTime(1994, 1, 1),
            };

            var godfather = new Movie()
            {
                Title = "The Godfather",
                Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                ReleaseDate = new DateTime(1972, 1, 1),
            };

            var theDarkKnight = new Movie()
            {
                Title = "The Dark Knight",
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                ReleaseDate = new DateTime(2008, 1, 1),
            };

            var moviesColumns = new string[] { "Id", "Title", "Description", "ReleaseDate", "CreatedWhen", "ModifiedWhen" };
            migrationBuilder.InsertData(
                "Movies",
                moviesColumns,
                new object[] { 1, shawshankRedemption.Title, shawshankRedemption.Description, shawshankRedemption.ReleaseDate, DateTime.Now, DateTime.Now }
                );
            migrationBuilder.InsertData(
                "Movies",
                moviesColumns,
                new object[] { 2, godfather.Title, godfather.Description, godfather.ReleaseDate, DateTime.Now, DateTime.Now });
            migrationBuilder.InsertData(
                "Movies",
                moviesColumns,
                new object[] { 3, theDarkKnight.Title, theDarkKnight.Description, theDarkKnight.ReleaseDate, DateTime.Now, DateTime.Now });

            var user = new User
            {
                Username = "default",
                Email = "default@default.com",
                CreatedWhen = DateTime.Now,
                ModifiedWhen = DateTime.Now,
                Id = 1
            };
            user.SetPassword("default");

            migrationBuilder.InsertData(
                "Users",
                new string[] { "Id", "Username", "Email", "CreatedWhen", "ModifiedWhen", "Salt", "PassHash", "Private" },
                new object[] { user.Id, user.Username, user.Email, user.CreatedWhen, user.ModifiedWhen, user.Salt, user.PassHash, user.Private });

            var ratingColumns = new string[] { "Value", "MovieId", "UserId", "CreatedWhen", "ModifiedWhen" };
            migrationBuilder.InsertData("Ratings", ratingColumns, new object[] { 5, 2, 1, DateTime.Now, DateTime.Now });
            migrationBuilder.InsertData("Ratings", ratingColumns, new object[] { 3, 2, 1, DateTime.Now, DateTime.Now });
            migrationBuilder.InsertData("Ratings", ratingColumns, new object[] { 5, 1, 1, DateTime.Now, DateTime.Now });

        }
    }
}
