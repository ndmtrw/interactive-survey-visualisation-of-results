namespace SurveyLive.Models;

public class Vote
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public int AnswerOptionId { get; set; }
    public string ParticipantId { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
