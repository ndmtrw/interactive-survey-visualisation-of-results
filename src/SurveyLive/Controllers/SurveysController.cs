using Microsoft.AspNetCore.Mvc;
using SurveyLive.Services;

namespace SurveyLive.Controllers;

[ApiController]
[Route("api/surveys")]
public class SurveysController(ISurveyService surveys) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var survey = await surveys.GetAsync(id, cancellationToken);

        if (survey is null)
            return NotFound(new { message = "Анкетата не е намерена." });

        return Ok(survey);
    }
}
