namespace SurveyLive.Models;

/// <summary>Описва въпрос в анкета и възможните отговори към него.</summary>
public class Question
{
    /// <summary>Уникален идентификатор на въпроса.</summary>
    public int Id { get; set; }

    /// <summary>Идентификатор на анкетата, към която принадлежи въпросът.</summary>
    public int SurveyId { get; set; }

    /// <summary>Текстът, който се показва на участниците.</summary>
    public string Text { get; set; } = "";

    /// <summary>Поредност на въпроса в анкетата, започваща от 1.</summary>
    public int Position { get; set; }

    /// <summary>Вариантите за отговор, налични за този въпрос.</summary>
    public List<AnswerOption> Options { get; set; } = [];
}
