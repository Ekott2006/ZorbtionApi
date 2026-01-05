#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Core.Data.Migrations;

/// <inheritdoc />
public partial class AddedUserBot : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "UserBotCodes",
            table => new
            {
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RandomCode = table.Column<string>("text", nullable: false),
                UserId = table.Column<string>("text", nullable: false),
                Type = table.Column<int>("integer", nullable: false),
                ExpirationDate = table.Column<DateTime>("timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserBotCodes", x => x.Id);
                table.ForeignKey(
                    "FK_UserBotCodes_AspNetUsers_UserId",
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

        migrationBuilder.CreateIndex(
            "IX_UserBotCodes_UserId",
            "UserBotCodes",
            "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "UserBotCodes");

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2025, 12, 29, 5, 24, 38, 771, DateTimeKind.Utc).AddTicks(6131),
                new DateTime(2025, 12, 29, 5, 24, 38, 771, DateTimeKind.Utc).AddTicks(6136)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2025, 12, 29, 5, 24, 38, 771, DateTimeKind.Utc).AddTicks(7508),
                new DateTime(2025, 12, 29, 5, 24, 38, 771, DateTimeKind.Utc).AddTicks(7509)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            3,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2025, 12, 29, 5, 24, 38, 771, DateTimeKind.Utc).AddTicks(7512),
                new DateTime(2025, 12, 29, 5, 24, 38, 771, DateTimeKind.Utc).AddTicks(7512)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2025, 12, 29, 5, 24, 38, 770, DateTimeKind.Utc).AddTicks(4409),
                new DateTime(2025, 12, 29, 5, 24, 38, 770, DateTimeKind.Utc).AddTicks(4413)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2025, 12, 29, 5, 24, 38, 770, DateTimeKind.Utc).AddTicks(5898),
                new DateTime(2025, 12, 29, 5, 24, 38, 770, DateTimeKind.Utc).AddTicks(5898)
            });
    }
}