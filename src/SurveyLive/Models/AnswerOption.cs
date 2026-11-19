namespace SurveyLive.Models;

/// <summary>Представя един възможен отговор на въпрос.</summary>
public class AnswerOption
{
    /// <summary>Уникален идентификатор на варианта.</summary>
    public int Id { get; set; }

    /// <summary>Идентификатор на въпроса, към който принадлежи вариантът.</summary>
    public int QuestionId { get; set; }

    /// <summary>Текстът на варианта, който участникът може да избере.</summary>
    public string Text { get; set; } = "";

    /// <summary>Поредност на варианта сред отговорите на въпроса, започваща от 1.</summary>
    public int Position { get; set; }
}
