namespace SurveyLive.Models;

/// <summary>Записва избора на участник за въпрос в конкретна сесия на анкета.</summary>
public class Vote
{
    /// <summary>Уникален идентификатор на записа за отговор.</summary>
    public int Id { get; set; }

    /// <summary>Идентификатор на анкетата, към която се отнася отговорът.</summary>
    public int SurveyId { get; set; }

    /// <summary>Идентификатор на сесията, в която е подаден отговорът.</summary>
    public int SurveySessionId { get; set; }

    /// <summary>Идентификатор на въпроса, на който е отговорено.</summary>
    public int QuestionId { get; set; }

    /// <summary>Идентификатор на избрания вариант за отговор.</summary>
    public int AnswerOptionId { get; set; }

    /// <summary>Идентификатор на участника, използван за ограничаване на повторен отговор.</summary>
    public string ParticipantId { get; set; } = "";

    /// <summary>Моментът на подаване, записан в UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
