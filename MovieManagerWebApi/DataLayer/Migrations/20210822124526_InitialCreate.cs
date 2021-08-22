using System;
using Domain;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ModifiedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CoverPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TVShows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NumberOfSeasons = table.Column<int>(type: "integer", nullable: false),
                    ModifiedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CoverPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TVShows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "bytea", maxLength: 32, nullable: false),
                    PassHash = table.Column<byte[]>(type: "bytea", maxLength: 32, nullable: false),
                    ProfileImage = table.Column<string>(type: "text", nullable: true),
                    About = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Private = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "integer", nullable: false),
                    StarredInMoviesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.StarredInMoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_StarredInMoviesId",
                        column: x => x.StarredInMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorTVShow",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "integer", nullable: false),
                    StarredInTvShowsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorTVShow", x => new { x.ActorsId, x.StarredInTvShowsId });
                    table.ForeignKey(
                        name: "FK_ActorTVShow_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorTVShow_TVShows_StarredInTvShowsId",
                        column: x => x.StarredInTvShowsId,
                        principalTable: "TVShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<byte>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<int>(type: "integer", nullable: true),
                    SeriesId = table.Column<int>(type: "integer", nullable: true),
                    ModifiedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedWhen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_TVShows_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "TVShows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_StarredInMoviesId",
                table: "ActorMovie",
                column: "StarredInMoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_ActorTVShow_StarredInTvShowsId",
                table: "ActorTVShow",
                column: "StarredInTvShowsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SeriesId",
                table: "Ratings",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            /* === MY CUSTOM STUFF === */

            // add constraint for rating, rating should be for tv show or for movie, cannot be both or neither
            /*migrationBuilder.Sql(@"ALTER TABLE Ratings
                                   ADD CONSTRAINT OneColumnNull CHECK
                                   ((MovieId IS NULL AND SeriesId IS NOT NULL) OR
                                   (MovieId IS NOT NULL AND SeriesId IS NULL))");*/


            SeedData(migrationBuilder);
        }

        private void SeedData(MigrationBuilder migrationBuilder)
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
            migrationBuilder.InsertData("Ratings", ratingColumns, new object[] { 10, 2, 1, DateTime.Now, DateTime.Now });
            migrationBuilder.InsertData("Ratings", ratingColumns, new object[] { 8, 2, 1, DateTime.Now, DateTime.Now });
            migrationBuilder.InsertData("Ratings", ratingColumns, new object[] { 10, 1, 1, DateTime.Now, DateTime.Now });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "ActorTVShow");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "TVShows");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
