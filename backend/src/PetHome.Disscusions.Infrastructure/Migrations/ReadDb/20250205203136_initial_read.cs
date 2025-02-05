using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHome.Disscusions.Infrastructure.Migrations.ReadDb
{
    /// <inheritdoc />
    public partial class initial_read : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disscusions",
                columns: table => new
                {
                    disscusion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    relation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    users = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disscusions", x => x.disscusion_id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false),
                    disscusion_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.message_id);
                    table.ForeignKey(
                        name: "fk_messages_disscusions_disscusion_id",
                        column: x => x.disscusion_id,
                        principalTable: "disscusions",
                        principalColumn: "disscusion_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_messages_disscusion_id",
                table: "messages",
                column: "disscusion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "disscusions");
        }
    }
}
