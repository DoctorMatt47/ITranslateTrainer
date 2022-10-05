import {endpoints, sendAsync} from "../common/common.js";
import * as testStorage from "../common/test-storage.js";

if (!testStorage.isEmpty()) window.location.href = "../quiz/quiz.html";

const startButton = document.getElementById("start-button");
const testCountInput = document.getElementById("test-count");
const optionCountInput = document.getElementById("option-count");
const languageSelect = document.getElementById("language-select");

const retrieveValues = () => {
    const languages = languageSelect.value.split(" ");
    console.log(languages);
    return [languages[0], languages[1], testCountInput.value, optionCountInput.value];
};

const startClickHandler = () => {
    const [from, to, testCount, optionCount] = retrieveValues();
    sendAsync(endpoints.quiz + `?from=${from}&to=${to}&testCount=${testCount}&optionCount=${optionCount}`)
        .then(response => {
            if (response.length === 0) return;
            testStorage.setTests(response);
            window.location.href = "../quiz/quiz.html";
        });
};

startButton.addEventListener("click", startClickHandler);
