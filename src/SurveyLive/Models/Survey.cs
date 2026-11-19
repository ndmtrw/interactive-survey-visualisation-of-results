namespace SurveyLive.Models;

/// <summary>Описва анкета, която групира въпроси и може да бъде провеждана в сесии.</summary>
public class Survey
{
    /// <summary>Уникален идентификатор на анкетата.</summary>
    public int Id { get; set; }

    /// <summary>Заглавие, което позволява на автора да разпознае анкетата.</summary>
    public string Title { get; set; } = "";

    /// <summary>Моментът на създаване, записан в UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Въпросите, включени в тази анкета.</summary>
    public List<Question> Questions { get; set; } = [];
}
