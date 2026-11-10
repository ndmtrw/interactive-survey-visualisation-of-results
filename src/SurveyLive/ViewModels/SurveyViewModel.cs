namespace SurveyLive.ViewModels;

public record SurveyViewModel(int Id, string Title, bool IsActive, List<QuestionViewModel> Questions);
public record QuestionViewModel(int Id, string Text, List<AnswerOptionViewModel> Options);
public record AnswerOptionViewModel(int Id, string Text);
