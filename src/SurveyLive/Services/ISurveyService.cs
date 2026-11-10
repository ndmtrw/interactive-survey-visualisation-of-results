using SurveyLive.ViewModels;

namespace SurveyLive.Services;

public interface ISurveyService
{
    Task<SurveyViewModel?> GetAsync(int id, CancellationToken cancellationToken);
}
