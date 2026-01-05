#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Core.Data.Migrations;

/// <inheritdoc />
public partial class BotRenameandUpdate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "UserBotProviders");

        migrationBuilder.DropIndex(
            "IX_Decks_Name_Id",
            "Decks");

        migrationBuilder.DropColumn(
            "Type",
            "UserBotCodes");

        migrationBuilder.CreateTable(
            "UserBots",
            table => new
            {
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                BotId = table.Column<string>("text", nullable: false),
                UserId = table.Column<string>("text", nullable: false),
                Type = table.Column<int>("integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserBots", x => x.Id);
                table.ForeignKey(
                    "FK_UserBots_AspNetUsers_UserId",
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
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(8397),
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(8398)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(9441),
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(9441)
            });

        migrationBuilder.UpdateData(
            "NoteTypeTemplates",
            "Id",
            3,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(9443),
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(9443)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            1,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(902),
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(904)
            });

        migrationBuilder.UpdateData(
            "NoteTypes",
            "Id",
            2,
            new[] { "CreatedAt", "UpdatedAt" },
            new object[]
            {
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(2032),
                new DateTime(2026, 1, 3, 10, 6, 30, 611, DateTimeKind.Utc).AddTicks(2033)
            });

        migrationBuilder.CreateIndex(
            "IX_NoteTypes_Name_CreatorId",
            "NoteTypes",
            new[] { "Name", "CreatorId" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Decks_Name_CreatorId",
            "Decks",
            new[] { "Name", "CreatorId" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_UserBots_BotId_UserId",
            "UserBots",
            new[] { "BotId", "UserId" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_UserBots_UserId",
            "UserBots",
            "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "UserBots");

        migrationBuilder.DropIndex(
            "IX_NoteTypes_Name_CreatorId",
            "NoteTypes");

        migrationBuilder.DropIndex(
            "IX_Decks_Name_CreatorId",
            "Decks");

        migrationBuilder.AddColumn<int>(
            "Type",
            "UserBotCodes",
            "integer",
            nullable: false,
            defaultValue: 0);

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
            "IX_Decks_Name_Id",
            "Decks",
            new[] { "Name", "Id" },
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_UserBotProviders_UserId",
            "UserBotProviders",
            "UserId");
    }
}