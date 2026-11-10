using Microsoft.EntityFrameworkCore;
using SurveyLive.Models;

namespace SurveyLive.Data;

public class SurveyDbContext(DbContextOptions<SurveyDbContext> options) : DbContext(options)
{
    public DbSet<Survey> Surveys => Set<Survey>();
    public DbSet<SurveySession> SurveySessions => Set<SurveySession>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
    public DbSet<Vote> Votes => Set<Vote>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        var survey = model.Entity<Survey>();
        survey.Property(s => s.Title).IsRequired().HasMaxLength(150);
        survey.HasMany(s => s.Questions).WithOne().HasForeignKey(q => q.SurveyId).OnDelete(DeleteBehavior.NoAction);
        survey.ToTable(t => t.HasCheckConstraint("CK_Surveys_Title", "LEN(LTRIM(RTRIM([Title]))) > 0"));

        var session = model.Entity<SurveySession>();
        session.Property(s => s.JoinCode).IsRequired().HasMaxLength(6);
        session.HasIndex(s => s.JoinCode).IsUnique();
        session.HasOne<Survey>().WithMany().HasForeignKey(s => s.SurveyId).OnDelete(DeleteBehavior.NoAction);
        session.ToTable(t =>
        {
            t.HasCheckConstraint("CK_SurveySessions_JoinCode", "LEN([JoinCode]) = 6 AND [JoinCode] COLLATE Latin1_General_100_BIN2 NOT LIKE '%[^0-9]%'");
        });

        var question = model.Entity<Question>();
        question.Property(q => q.Text).IsRequired().HasMaxLength(300);
        question.HasIndex(q => new { q.SurveyId, q.Position }).IsUnique();
        question.HasMany(q => q.Options).WithOne().HasForeignKey(o => o.QuestionId).OnDelete(DeleteBehavior.NoAction);
        question.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Questions_Text", "LEN(LTRIM(RTRIM([Text]))) > 0");
            t.HasCheckConstraint("CK_Questions_Position", "Position > 0");
        });

        var option = model.Entity<AnswerOption>();
        option.Property(o => o.Text).IsRequired().HasMaxLength(200);
        option.HasIndex(o => new { o.QuestionId, o.Position }).IsUnique();
        option.ToTable(t =>
        {
            t.HasCheckConstraint("CK_AnswerOptions_Text", "LEN(LTRIM(RTRIM([Text]))) > 0");
            t.HasCheckConstraint("CK_AnswerOptions_Position", "Position > 0");
        });

        var vote = model.Entity<Vote>();
        vote.Property(v => v.ParticipantId).IsRequired().HasMaxLength(64);
        vote.HasIndex(v => new { v.SurveySessionId, v.QuestionId, v.ParticipantId }).IsUnique();
        vote.HasOne<Question>().WithMany()
            .HasForeignKey(v => new { v.QuestionId, v.SurveyId })
            .HasPrincipalKey(q => new { q.Id, q.SurveyId }).OnDelete(DeleteBehavior.NoAction);
        vote.HasOne<SurveySession>().WithMany()
            .HasForeignKey(v => new { v.SurveySessionId, v.SurveyId })
            .HasPrincipalKey(s => new { s.Id, s.SurveyId }).OnDelete(DeleteBehavior.NoAction);
        vote.HasOne<AnswerOption>().WithMany()
            .HasForeignKey(v => new { v.AnswerOptionId, v.QuestionId })
            .HasPrincipalKey(o => new { o.Id, o.QuestionId }).OnDelete(DeleteBehavior.NoAction);
        vote.ToTable(t => t.HasCheckConstraint("CK_Votes_ParticipantId", "LEN(LTRIM(RTRIM([ParticipantId]))) > 0"));

    }
}
