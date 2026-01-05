#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Data.Migrations;

/// <inheritdoc />
public partial class BotProviderUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "UserBotProviders",
            table => new
            {
                Id = table.Column<string>("text", nullable: false),
                UserId = table.Column<string>("text", nullable: false),
                Type = table.Column<int>("integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserBotProviders", x => x.Id);
                table.ForeignKey(
                    "FK_UserBotProviders_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 52, 8, 606, DateTimeKind.Utc).AddTicks(9521),
                new DateTime(2026, 1, 2, 23, 52, 8, 606, DateTimeKind.Utc).AddTicks(9523)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 52, 8, 607, DateTimeKind.Utc).AddTicks(1018),
                new DateTime(2026, 1, 2, 23, 52, 8, 607, DateTimeKind.Utc).AddTicks(1018)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            3,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 52, 8, 607, DateTimeKind.Utc).AddTicks(1020),
                new DateTime(2026, 1, 2, 23, 52, 8, 607, DateTimeKind.Utc).AddTicks(1020)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 52, 8, 605, DateTimeKind.Utc).AddTicks(9370),
                new DateTime(2026, 1, 2, 23, 52, 8, 605, DateTimeKind.Utc).AddTicks(9375)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 52, 8, 606, DateTimeKind.Utc).AddTicks(1159),
                new DateTime(2026, 1, 2, 23, 52, 8, 606, DateTimeKind.Utc).AddTicks(1160)
            });

        migrationBuilder.CreateIndex(
            "IX_UserBotProviders_UserId",
            "UserBotProviders",
            "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "UserBotProviders");

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 31, 2, 70, DateTimeKind.Utc).AddTicks(6302),
                new DateTime(2026, 1, 2, 23, 31, 2, 70, DateTimeKind.Utc).AddTicks(6308)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 31, 2, 70, DateTimeKind.Utc).AddTicks(7837),
                new DateTime(2026, 1, 2, 23, 31, 2, 70, DateTimeKind.Utc).AddTicks(7838)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            3,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 31, 2, 70, DateTimeKind.Utc).AddTicks(7840),
                new DateTime(2026, 1, 2, 23, 31, 2, 70, DateTimeKind.Utc).AddTicks(7840)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 31, 2, 69, DateTimeKind.Utc).AddTicks(5929),
                new DateTime(2026, 1, 2, 23, 31, 2, 69, DateTimeKind.Utc).AddTicks(5934)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 2, 23, 31, 2, 69, DateTimeKind.Utc).AddTicks(7313),
                new DateTime(2026, 1, 2, 23, 31, 2, 69, DateTimeKind.Utc).AddTicks(7314)
            });
    }
}