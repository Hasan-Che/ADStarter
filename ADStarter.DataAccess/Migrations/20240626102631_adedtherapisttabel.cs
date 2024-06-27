using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADStarter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class adedtherapisttabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Therapists_AspNetUsers_UserId",
                table: "Therapists");

            migrationBuilder.DropIndex(
                name: "IX_Therapists_UserId",
                table: "Therapists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Therapists_UserId",
                table: "Therapists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Therapists_AspNetUsers_UserId",
                table: "Therapists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
