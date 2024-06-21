using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADStarter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddtIDFKToChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "t_ID",
                table: "Children",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Children_t_ID",
                table: "Children",
                column: "t_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Therapists_t_ID",
                table: "Children",
                column: "t_ID",
                principalTable: "Therapists",
                principalColumn: "t_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Therapists_t_ID",
                table: "Children");

            migrationBuilder.DropIndex(
                name: "IX_Children_t_ID",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "t_ID",
                table: "Children");
        }
    }
}
