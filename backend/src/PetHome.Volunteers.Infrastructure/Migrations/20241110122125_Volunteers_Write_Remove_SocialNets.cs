using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHome.Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Volunteers_Write_Remove_SocialNets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requisites",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "social_networks",
                table: "volunteers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "requisites",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "social_networks",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
