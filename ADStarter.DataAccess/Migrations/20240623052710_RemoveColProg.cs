using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADStarter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColProg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Programs_prog_ID",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_prog_ID",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "c_status",
                table: "Children");

            migrationBuilder.RenameColumn(
                name: "prog_ID",
                table: "Children",
                newName: "c_step");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "c_step",
                table: "Children",
                newName: "prog_ID");

            migrationBuilder.AddColumn<int>(
                name: "c_status",
                table: "Children",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Children_prog_ID",
                table: "Children",
                column: "prog_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Programs_prog_ID",
                table: "Children",
                column: "prog_ID",
                principalTable: "Programs",
                principalColumn: "prog_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
