using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace SurveyLive.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    JoinCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.CheckConstraint("CK_Surveys_JoinCode", "length(JoinCode) = 6 AND JoinCode NOT GLOB '*[^0-9]*'");
                    table.CheckConstraint("CK_Surveys_Title", "length(trim(Title)) BETWEEN 1 AND 150");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SurveyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.CheckConstraint("CK_Questions_Position", "Position > 0");
                    table.CheckConstraint("CK_Questions_Text", "length(trim(Text)) BETWEEN 1 AND 300");
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.UniqueConstraint("AK_AnswerOptions_Id_QuestionId", x => new { x.Id, x.QuestionId });
                    table.CheckConstraint("CK_AnswerOptions_Position", "Position > 0");
                    table.CheckConstraint("CK_AnswerOptions_Text", "length(trim(Text)) BETWEEN 1 AND 200");
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnswerOptionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipantId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.CheckConstraint("CK_Votes_ParticipantId", "length(trim(ParticipantId)) BETWEEN 1 AND 64");
                    table.ForeignKey(
                        name: "FK_Votes_AnswerOptions_AnswerOptionId_QuestionId",
                        columns: x => new { x.AnswerOptionId, x.QuestionId },
                        principalTable: "AnswerOptions",
                        principalColumns: new[] { "Id", "QuestionId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "CreatedAt", "IsActive", "JoinCode", "Title" },
                values: new object[] { 1, new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), false, "123456", "Участие в интерактивни анкети" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Position", "SurveyId", "Text" },
                values: new object[] { 1, 1, 1, "Кой начин за присъединяване предпочиташ?" });

            migrationBuilder.InsertData(
                table: "AnswerOptions",
                columns: new[] { "Id", "Position", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, 1, 1, "Кратък код" },
                    { 2, 2, 1, "QR код" },
                    { 3, 3, 1, "Директна връзка" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOptions_QuestionId_Position",
                table: "AnswerOptions",
                columns: new[] { "QuestionId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId_Position",
                table: "Questions",
                columns: new[] { "SurveyId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_JoinCode",
                table: "Surveys",
                column: "JoinCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_AnswerOptionId_QuestionId",
                table: "Votes",
                columns: new[] { "AnswerOptionId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_QuestionId_ParticipantId",
                table: "Votes",
                columns: new[] { "QuestionId", "ParticipantId" },
                unique: true);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
