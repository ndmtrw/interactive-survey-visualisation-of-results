namespace SurveyLive.Models;

public class SurveySession
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string JoinCode { get; set; } = "";
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
