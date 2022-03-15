const tests = JSON.parse(localStorage.getItem("tests"));
if (!tests) window.location.href = "../html/quiz-settings.html";

let currentIndex = parseInt(localStorage.getItem("tests.currentIndex"));

const testElement = document.getElementById("test");
const nextButton = document.getElementById("next-button");
const optionsElement = document.getElementById("options");
const toTranslateElement = document.getElementById("to-translate");
const restartButton = document.getElementById("restart-button");

const currentTest = () => tests[currentIndex];

const optionsHtml = options => options.map(opt => optionHtml(opt)).reduce((acc, optHtml) => acc + optHtml);

const optionHtml = option =>
    `<label class="mb-4">
        <input name="option" type="radio">
        ${option.string}
    </label>`

const nextTest = () => {
    if (currentIndex > tests.length) return;
    const test = tests[++currentIndex];
    localStorage.setItem("tests.currentIndex", currentIndex);
    return test;
}
const showTest = test => {
    toTranslateElement.innerText = test.text;
    optionsElement.innerHTML = optionsHtml(test.options);
};

const optionsChangeHandler = () => {
    const optionElements = document.getElementsByName("option");
    const test = currentTest();
    for (let i = 0; i < optionElements.length; i++) {
        if (test.options[i].isCorrect) {
            showCorrectOption(optionElements[i]);
        }
        if (optionElements[i].checked) {
            optionClickHandler(optionElements[i], test.options[i].isCorrect);
        }
    }
    optionElements.forEach(opt => opt.disabled = true);
};

optionsElement.addEventListener("change", optionsChangeHandler);

const showCorrectOption = option => {
    option.parentElement.style.borderColor = "#499C54";
}

const optionClickHandler = (option, isCorrect) => {
    let optionStyle = option.parentElement.style;
    if (isCorrect) {
        optionStyle.backgroundColor = "#132b15";
        return;
    }
    optionStyle.borderColor = "#9E2927";
    optionStyle.backgroundColor = "#22090F";
}

nextButton.addEventListener("click", () => {
    showTest(nextTest());
});

restartButton.addEventListener("click", () => {
    localStorage.removeItem("tests");
    localStorage.removeItem("tests.currentIndex");
    window.location.href = "../html/quiz-settings.html";
});

showTest(currentTest());
