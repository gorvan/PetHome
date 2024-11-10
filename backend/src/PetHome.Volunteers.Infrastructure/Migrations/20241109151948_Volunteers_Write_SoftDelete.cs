using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHome.Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Volunteers_Write_SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_delete",
                table: "volunteers",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "is_delete",
                table: "pets",
                newName: "is_deleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_date",
                table: "volunteers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_date",
                table: "pets",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deletion_date",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "deletion_date",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "volunteers",
                newName: "is_delete");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "pets",
                newName: "is_delete");
        }
    }
}
