using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientApi.Migrations
{
    public partial class PlaygroundsFixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_playgrounds_PlaygroundId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_playgrounds",
                table: "playgrounds");

            migrationBuilder.RenameTable(
                name: "playgrounds",
                newName: "Playgrounds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playgrounds",
                table: "Playgrounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Playgrounds_PlaygroundId",
                table: "Events",
                column: "PlaygroundId",
                principalTable: "Playgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Playgrounds_PlaygroundId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playgrounds",
                table: "Playgrounds");

            migrationBuilder.RenameTable(
                name: "Playgrounds",
                newName: "playgrounds");

            migrationBuilder.AddPrimaryKey(
                name: "PK_playgrounds",
                table: "playgrounds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_playgrounds_PlaygroundId",
                table: "Events",
                column: "PlaygroundId",
                principalTable: "playgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
