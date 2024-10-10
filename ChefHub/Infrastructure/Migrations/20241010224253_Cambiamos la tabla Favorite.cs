using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CambiamoslatablaFavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Favorites_FavoriteId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Favorites_FavoriteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_FavoriteId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Recipes");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Favorites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Favorites",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorites");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "Recipes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteId",
                table: "Users",
                column: "FavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FavoriteId",
                table: "Recipes",
                column: "FavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Favorites_FavoriteId",
                table: "Recipes",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Favorites_FavoriteId",
                table: "Users",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id");
        }
    }
}
