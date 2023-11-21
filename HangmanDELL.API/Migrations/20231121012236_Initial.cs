using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangmanDELL.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ip_address = table.Column<string>(type: "varchar(15)", nullable: true),
                    ip_port = table.Column<string>(type: "varchar(5)", nullable: false),
                    word_to_guess = table.Column<string>(type: "varchar(50)", nullable: true),
                    word_progress = table.Column<string>(type: "varchar(50)", nullable: true),
                    number_of_lives = table.Column<int>(type: "int", nullable: false, defaultValue: 5)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guessed_letter = table.Column<string>(type: "char(1)", nullable: false),
                    is_success = table.Column<bool>(type: "bit", nullable: false),
                    creation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 11, 20, 22, 22, 36, 182, DateTimeKind.Local).AddTicks(3547)),
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_Guess",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guesses_HistoryId",
                table: "Guesses",
                column: "HistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guesses");

            migrationBuilder.DropTable(
                name: "Histories");
        }
    }
}
