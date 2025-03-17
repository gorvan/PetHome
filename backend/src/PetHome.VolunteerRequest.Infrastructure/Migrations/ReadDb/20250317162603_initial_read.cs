using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHome.VolunteerRequests.Infrastructure.Migrations.ReadDb
{
    /// <inheritdoc />
    public partial class initial_read : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteer_requests",
                columns: table => new
                {
                    request_id = table.Column<Guid>(type: "uuid", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rejection_comment = table.Column<string>(type: "text", nullable: false),
                    disscusion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    volunteer_info_description = table.Column<string>(type: "text", nullable: false),
                    volunteer_info_email = table.Column<string>(type: "text", nullable: false),
                    volunteer_info_phone = table.Column<string>(type: "text", nullable: false),
                    volunteer_info_full_name_first_name = table.Column<string>(type: "text", nullable: false),
                    volunteer_info_full_name_second_name = table.Column<string>(type: "text", nullable: false),
                    volunteer_info_full_name_surname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_requests", x => x.request_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "volunteer_requests");
        }
    }
}
