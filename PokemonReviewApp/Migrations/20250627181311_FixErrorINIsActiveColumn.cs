using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class FixErrorINIsActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                computedColumnSql: "Case When [IsUsed] = 0 And [IsRevoked] = 0 And [Expires] > GETUTCDATE() THEN 1 ELSE 0 END",
                stored: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "Case When [IsUsed] = 0 And [IsRevoked] = 0 And [Expires] > GETUTCDATE() THEN 1 ELSE 0",
                oldStored: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                computedColumnSql: "Case When [IsUsed] = 0 And [IsRevoked] = 0 And [Expires] > GETUTCDATE() THEN 1 ELSE 0",
                stored: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "Case When [IsUsed] = 0 And [IsRevoked] = 0 And [Expires] > GETUTCDATE() THEN 1 ELSE 0 END",
                oldStored: false);
        }
    }
}
