using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class updateOnDeleteInModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Countries_CountryId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Categories_CategoryId",
                table: "Pokemons");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Countries_CountryId",
                table: "Owners",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Categories_CategoryId",
                table: "Pokemons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Countries_CountryId",
                table: "Owners");

            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Categories_CategoryId",
                table: "Pokemons");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Countries_CountryId",
                table: "Owners",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Categories_CategoryId",
                table: "Pokemons",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
