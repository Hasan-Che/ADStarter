using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ADStarter.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ann_media",
                table: "Announcements");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Announcements");

            migrationBuilder.AddColumn<byte[]>(
                name: "ann_media",
                table: "Announcements",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
