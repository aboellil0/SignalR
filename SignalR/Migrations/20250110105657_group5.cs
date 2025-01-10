using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalR.Migrations
{
    /// <inheritdoc />
    public partial class group5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_GroupId",
                table: "GroupMessages",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessages_Groups_GroupId",
                table: "GroupMessages",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessages_Groups_GroupId",
                table: "GroupMessages");

            migrationBuilder.DropIndex(
                name: "IX_GroupMessages_GroupId",
                table: "GroupMessages");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupMessages");
        }
    }
}
