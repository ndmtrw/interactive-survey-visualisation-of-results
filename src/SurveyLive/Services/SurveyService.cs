using Microsoft.EntityFrameworkCore;
using SurveyLive.Data;
using SurveyLive.ViewModels;

namespace SurveyLive.Services;

public class SurveyService(SurveyDbContext db) : ISurveyService
{
    public Task<SurveyViewModel?> GetAsync(int id, CancellationToken cancellationToken) =>
        db.Surveys.AsNoTracking().AsSplitQuery()
            .Where(s => s.Id == id)
            .Select(s => new SurveyViewModel(
                s.Id,
                s.Title,
                db.SurveySessions.Any(session => session.SurveyId == s.Id && session.IsActive),
                s.Questions.OrderBy(q => q.Position).Select(q => new QuestionViewModel(
                    q.Id,
                    q.Text,
                    q.Options.OrderBy(o => o.Position)
                        .Select(o => new AnswerOptionViewModel(o.Id, o.Text)).ToList())).ToList()))
            .SingleOrDefaultAsync(cancellationToken);
}
