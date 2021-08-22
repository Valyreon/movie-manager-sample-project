using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RatingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_TVShows_SeriesId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_SeriesId",
                table: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "TVShowId",
                table: "Ratings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TVShowId",
                table: "Ratings",
                column: "TVShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_TVShows_TVShowId",
                table: "Ratings",
                column: "TVShowId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_TVShows_TVShowId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_TVShowId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "TVShowId",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SeriesId",
                table: "Ratings",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_TVShows_SeriesId",
                table: "Ratings",
                column: "SeriesId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
