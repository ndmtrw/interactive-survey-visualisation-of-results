using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.CheckConstraint("CK_Surveys_Title", "LEN(LTRIM(RTRIM([Title]))) > 0");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.UniqueConstraint("AK_Questions_Id_SurveyId", x => new { x.Id, x.SurveyId });
                    table.CheckConstraint("CK_Questions_Position", "Position > 0");
                    table.CheckConstraint("CK_Questions_Text", "LEN(LTRIM(RTRIM([Text]))) > 0");
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveySessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    JoinCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveySessions", x => x.Id);
                    table.UniqueConstraint("AK_SurveySessions_Id_SurveyId", x => new { x.Id, x.SurveyId });
                    table.CheckConstraint("CK_SurveySessions_JoinCode", "LEN([JoinCode]) = 6 AND [JoinCode] COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%'");
                    table.ForeignKey(
                        name: "FK_SurveySessions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnswerOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOptions", x => x.Id);
                    table.UniqueConstraint("AK_AnswerOptions_Id_QuestionId", x => new { x.Id, x.QuestionId });
                    table.CheckConstraint("CK_AnswerOptions_Position", "Position > 0");
                    table.CheckConstraint("CK_AnswerOptions_Text", "LEN(LTRIM(RTRIM([Text]))) > 0");
                    table.ForeignKey(
                        name: "FK_AnswerOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    SurveySessionId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerOptionId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.CheckConstraint("CK_Votes_ParticipantId", "LEN(LTRIM(RTRIM([ParticipantId]))) > 0");
                    table.ForeignKey(
                        name: "FK_Votes_AnswerOptions_AnswerOptionId_QuestionId",
                        columns: x => new { x.AnswerOptionId, x.QuestionId },
                        principalTable: "AnswerOptions",
                        principalColumns: new[] { "Id", "QuestionId" });
                    table.ForeignKey(
                        name: "FK_Votes_Questions_QuestionId_SurveyId",
                        columns: x => new { x.QuestionId, x.SurveyId },
                        principalTable: "Questions",
                        principalColumns: new[] { "Id", "SurveyId" });
                    table.ForeignKey(
                        name: "FK_Votes_SurveySessions_SurveySessionId_SurveyId",
                        columns: x => new { x.SurveySessionId, x.SurveyId },
                        principalTable: "SurveySessions",
                        principalColumns: new[] { "Id", "SurveyId" });
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
                name: "IX_SurveySessions_JoinCode",
                table: "SurveySessions",
                column: "JoinCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurveySessions_SurveyId",
                table: "SurveySessions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_AnswerOptionId_QuestionId",
                table: "Votes",
                columns: new[] { "AnswerOptionId", "QuestionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_QuestionId_SurveyId",
                table: "Votes",
                columns: new[] { "QuestionId", "SurveyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_SurveySessionId_QuestionId_ParticipantId",
                table: "Votes",
                columns: new[] { "SurveySessionId", "QuestionId", "ParticipantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_SurveySessionId_SurveyId",
                table: "Votes",
                columns: new[] { "SurveySessionId", "SurveyId" });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "AnswerOptions");

            migrationBuilder.DropTable(
                name: "SurveySessions");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");
        }
    }
}
