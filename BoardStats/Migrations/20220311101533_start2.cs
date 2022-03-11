using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoardStats.Migrations
{
    public partial class start2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_BoardGames_Id",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_Id",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Collections");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_IdGame",
                table: "Collections",
                column: "IdGame");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_BoardGames_IdGame",
                table: "Collections",
                column: "IdGame",
                principalTable: "BoardGames",
                principalColumn: "IdGame",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_BoardGames_IdGame",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_IdGame",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_Id",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_BoardGames_Id",
                table: "Collections",
                column: "Id",
                principalTable: "BoardGames",
                principalColumn: "IdGame",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
