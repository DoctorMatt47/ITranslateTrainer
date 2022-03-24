import * as testStorage from "../common/test-storage.js";

document.getElementById("to-quiz").addEventListener("click", () => {
    window.location.href = testStorage.isEmpty()
        ? "../quiz-settings/quiz-settings.html"
        : "../quiz/quiz.html";
});
