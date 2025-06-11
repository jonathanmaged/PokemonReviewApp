using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class RenamePokemonOwnersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners");

            migrationBuilder.RenameTable(
                name: "PokemonOwners",
                newName: "PokemonsOwners");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonOwners_PokemonId",
                table: "PokemonsOwners",
                newName: "IX_PokemonsOwners_PokemonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonsOwners",
                table: "PokemonsOwners",
                columns: new[] { "OwnerId", "PokemonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonsOwners_Owners_OwnerId",
                table: "PokemonsOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonsOwners_Pokemons_PokemonId",
                table: "PokemonsOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonsOwners_Owners_OwnerId",
                table: "PokemonsOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonsOwners_Pokemons_PokemonId",
                table: "PokemonsOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonsOwners",
                table: "PokemonsOwners");

            migrationBuilder.RenameTable(
                name: "PokemonsOwners",
                newName: "PokemonOwners");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonsOwners_PokemonId",
                table: "PokemonOwners",
                newName: "IX_PokemonOwners_PokemonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners",
                columns: new[] { "OwnerId", "PokemonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
