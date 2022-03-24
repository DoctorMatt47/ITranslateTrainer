import * as testStorage from "../common/test-storage.js";

const nextButton = document.getElementById("next-button");
const optionsElement = document.getElementById("options");
const toTranslateElement = document.getElementById("to-translate");
const restartButton = document.getElementById("restart-button");

const optionHtml = option =>
    `<label class="mb-4">
        <input name="option" type="radio">
        ${option.string}
    </label>`;

const optionsHtml = options => options.map(opt => optionHtml(opt)).reduce((acc, optHtml) => acc + optHtml);

const showTest = test => {
    toTranslateElement.innerText = test.text;
    optionsElement.innerHTML = optionsHtml(test.options);
};

const optionsChangeHandler = () => {
    const optionElements = document.getElementsByName("option");
    const test = testStorage.currentTest();
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
};

const optionClickHandler = (option, isCorrect) => {
    const optionStyle = option.parentElement.style;
    if (isCorrect) {
        optionStyle.backgroundColor = "#132b15";
        return;
    }
    optionStyle.borderColor = "#9E2927";
    optionStyle.backgroundColor = "#22090F";
};

nextButton.addEventListener("click", () => {
    showTest(testStorage.nextTest());
});

restartButton.addEventListener("click", () => {
    testStorage.removeTests();
    window.location.href = "../quiz-settings/quiz-settings.html";
});

if (testStorage.isEmpty()) window.location.href = "../quiz-settings/quiz-settings.html";
showTest(testStorage.currentTest());
