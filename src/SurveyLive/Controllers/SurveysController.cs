using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyLive.Data;

namespace SurveyLive.Controllers;

[ApiController]
[Route("api/surveys")]
public class SurveysController(SurveyDbContext db) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var survey = await db.Surveys.AsNoTracking().AsSplitQuery()
            .Where(s => s.Id == id)
            .Select(s => new
            {
                s.Id,
                s.Title,
                s.IsActive,
                Questions = s.Questions.OrderBy(q => q.Position).Select(q => new
                {
                    q.Id,
                    q.Text,
                    Options = q.Options.OrderBy(o => o.Position).Select(o => new { o.Id, o.Text }).ToList()
                }).ToList()
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (survey is null)
            return NotFound(new { message = "Анкетата не е намерена." });

        return Ok(survey);
    }
}
