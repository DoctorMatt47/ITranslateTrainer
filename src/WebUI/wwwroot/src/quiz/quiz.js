import * as testStorage from "../common/test-storage.js";
import {answer, color} from "../common/common.js";

const goToSettings = () => window.location.href = "../quiz-settings/quiz-settings.html";
const goToResults = () => window.location.href = "../quiz-result/quiz-result.html";

if (testStorage.isEmpty()) goToSettings();
if (!testStorage.currentTest()) goToResults();

const optionsElement = document.getElementById("options");
const toTranslateElement = document.getElementById("to-translate");

let currentAnswer = answer.skipped;

const optionHtml = option =>
    `<label class="mb-4">
        <input name="option" type="radio">
        ${option.string}
    </label>`;

const optionsHtml = options => options
    .map(opt => optionHtml(opt))
    .reduce((acc, optHtml) => acc + optHtml);

const showTest = test => {
    toTranslateElement.innerText = test.string;
    optionsElement.innerHTML = optionsHtml(test.options);
};

const shuffled = array => {
    const copy = [...array];
    let i = copy.length;
    while (i !== 0) {
        const j = Math.floor(Math.random() * i--);
        [copy[i], copy[j]] = [copy[j], copy[i]];
    }

    return copy;
};

const optionsChangeHandler = () => {
    const optionElements = document.getElementsByName("option");
    const test = testStorage.currentTest();
    for (let i = 0; i < optionElements.length; i++) {
        if (test.options[i].isCorrect) {
            showCorrectOption(optionElements[i]);
            currentAnswer = answer.correct;
        }
        if (optionElements[i].checked) {
            optionClickHandler(optionElements[i], test.options[i].isCorrect);
            currentAnswer = answer.incorrect;
        }
    }
    optionElements.forEach(opt => opt.disabled = true);
};

optionsElement.addEventListener("change", optionsChangeHandler);

const showCorrectOption = option => {
    option.parentElement.style.borderColor = color.lightGreen;
};

const optionClickHandler = (option, isCorrect) => {
    const optionStyle = option.parentElement.style;
    if (isCorrect) {
        optionStyle.backgroundColor = color.darkGreen;
        return;
    }
    optionStyle.borderColor = color.lightRed;
    optionStyle.backgroundColor = color.darkRed;
};

document.getElementById("next-button").addEventListener("click", () => {
    const nextTest = testStorage.nextTest(currentAnswer);
    currentAnswer = answer.skipped;
    if (!nextTest) goToResults();
    showTest(nextTest);
});

document.getElementById("restart-button").addEventListener("click", () => {
    testStorage.removeTests();
    goToSettings();
});

showTest(testStorage.currentTest());
