const toQuiz = document.getElementById("to-quiz");
toQuiz.addEventListener("click", () => {
    window.location.href = localStorage.getItem("tests")
        ? "../html/quiz.html"
        : "../html/quiz-settings.html";
});
