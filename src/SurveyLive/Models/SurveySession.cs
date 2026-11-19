namespace SurveyLive.Models;

/// <summary>Представя едно конкретно провеждане на анкета.</summary>
public class SurveySession
{
    /// <summary>Уникален идентификатор на сесията.</summary>
    public int Id { get; set; }

    /// <summary>Идентификатор на анкетата, която се провежда.</summary>
    public int SurveyId { get; set; }

    /// <summary>Шестцифрен код, чрез който участниците намират активната сесия.</summary>
    public string JoinCode { get; set; } = "";

    /// <summary>Показва дали сесията приема участие в момента.</summary>
    public bool IsActive { get; set; }

    /// <summary>Моментът на създаване на сесията, записан в UTC.</summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
