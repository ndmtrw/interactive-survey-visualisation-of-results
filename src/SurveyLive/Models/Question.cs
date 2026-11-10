namespace SurveyLive.Models;

public class Question
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string Text { get; set; } = "";
    public int Position { get; set; }
    public List<AnswerOption> Options { get; set; } = [];
}
