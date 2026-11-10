using Microsoft.EntityFrameworkCore;
using SurveyLive.Models;

namespace SurveyLive.Data;

public class SurveyDbContext(DbContextOptions<SurveyDbContext> options) : DbContext(options)
{
    public DbSet<Survey> Surveys => Set<Survey>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
    public DbSet<Vote> Votes => Set<Vote>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        var survey = model.Entity<Survey>();
        survey.Property(s => s.Title).IsRequired().HasMaxLength(150);
        survey.Property(s => s.JoinCode).IsRequired().HasMaxLength(6);
        survey.HasIndex(s => s.JoinCode).IsUnique();
        survey.HasMany(s => s.Questions).WithOne().HasForeignKey(q => q.SurveyId);
        survey.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Surveys_Title", "length(trim(Title)) BETWEEN 1 AND 150");
            t.HasCheckConstraint("CK_Surveys_JoinCode", "length(JoinCode) = 6 AND JoinCode NOT GLOB '*[^0-9]*'");
        });

        var question = model.Entity<Question>();
        question.Property(q => q.Text).IsRequired().HasMaxLength(300);
        question.HasIndex(q => new { q.SurveyId, q.Position }).IsUnique();
        question.HasMany(q => q.Options).WithOne().HasForeignKey(o => o.QuestionId);
        question.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Questions_Text", "length(trim(Text)) BETWEEN 1 AND 300");
            t.HasCheckConstraint("CK_Questions_Position", "Position > 0");
        });

        var option = model.Entity<AnswerOption>();
        option.Property(o => o.Text).IsRequired().HasMaxLength(200);
        option.HasIndex(o => new { o.QuestionId, o.Position }).IsUnique();
        option.ToTable(t =>
        {
            t.HasCheckConstraint("CK_AnswerOptions_Text", "length(trim(Text)) BETWEEN 1 AND 200");
            t.HasCheckConstraint("CK_AnswerOptions_Position", "Position > 0");
        });

        var vote = model.Entity<Vote>();
        vote.Property(v => v.ParticipantId).IsRequired().HasMaxLength(64);
        vote.HasIndex(v => new { v.QuestionId, v.ParticipantId }).IsUnique();
        vote.HasOne<Question>().WithMany().HasForeignKey(v => v.QuestionId);
        vote.HasOne<AnswerOption>().WithMany()
            .HasForeignKey(v => new { v.AnswerOptionId, v.QuestionId })
            .HasPrincipalKey(o => new { o.Id, o.QuestionId });
        vote.ToTable(t => t.HasCheckConstraint("CK_Votes_ParticipantId", "length(trim(ParticipantId)) BETWEEN 1 AND 64"));

        survey.HasData(new Survey
        {
            Id = 1,
            Title = "Участие в интерактивни анкети",
            JoinCode = "123456",
            CreatedAt = new DateTime(2026, 11, 10, 0, 0, 0, DateTimeKind.Utc)
        });
        question.HasData(new Question { Id = 1, SurveyId = 1, Text = "Кой начин за присъединяване предпочиташ?", Position = 1 });
        option.HasData(
            new AnswerOption { Id = 1, QuestionId = 1, Text = "Кратък код", Position = 1 },
            new AnswerOption { Id = 2, QuestionId = 1, Text = "QR код", Position = 2 },
            new AnswerOption { Id = 3, QuestionId = 1, Text = "Директна връзка", Position = 3 });
    }
}
