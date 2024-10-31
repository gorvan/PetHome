using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHome.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "participant_account",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    second_name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participant_account", x => x.id);
                    table.ForeignKey(
                        name: "fk_participant_account_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "accounts",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "volunteer_account",
                schema: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expirience = table.Column<int>(type: "integer", nullable: false),
                    requisites = table.Column<string>(type: "text", nullable: false),
                    sertificates = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    second_name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_account", x => x.id);
                    table.ForeignKey(
                        name: "fk_volunteer_account_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "accounts",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_participant_account_user_id",
                schema: "accounts",
                table: "participant_account",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_volunteer_account_user_id",
                schema: "accounts",
                table: "volunteer_account",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participant_account",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "volunteer_account",
                schema: "accounts");
        }
    }
}
