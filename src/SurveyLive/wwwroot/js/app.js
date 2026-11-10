const status = document.querySelector("#status");

async function loadSurvey() {
  try {
    const response = await fetch("/api/surveys/1");
    if (!response.ok) throw new Error("Survey unavailable");

    const survey = await response.json();
    document.querySelector("#survey-title").textContent = survey.title;
    status.textContent = survey.isActive ? "Анкетата е активна." : "Анкетата не е отворена за гласуване.";

    for (const question of survey.questions) {
      const title = document.createElement("h3");
      title.textContent = question.text;
      const options = document.createElement("ol");

      for (const option of question.options) {
        const item = document.createElement("li");
        item.textContent = option.text;
        options.append(item);
      }

      document.querySelector("#questions").append(title, options);
    }
  } catch {
    status.textContent = "Анкетата не може да бъде заредена. Опитай отново по-късно.";
  }
}

loadSurvey();
