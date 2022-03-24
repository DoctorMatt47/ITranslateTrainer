import {endpoints, sendAsync} from "../common/common.js";
import * as testStorage from "../common/test-storage.js";

if (!testStorage.isEmpty()) window.location.href = "../quiz/quiz.html";

let testCount = 0;
let optionCount = 0;

const startButton = document.getElementById("start-button");
const testCountInput = document.getElementById("test-count");
const optionCountInput = document.getElementById("option-count");
const languageSelect = document.getElementById("language-select");

const startClickHandler = () => {
    let from = languageSelect.value[0];
    let to = languageSelect.value[2];
    sendAsync(endpoints.quiz + `?from=${from}&to=${to}&testCount=${testCount}&optionCount=${optionCount}`)
        .then(response => {
            if (response.length === 0) return;
            testStorage.setTests(response);
            window.location.href = "../quiz/quiz.html";
        });
};

startButton.addEventListener("click", startClickHandler);

testCountInput.addEventListener("change", e => {
    testCount = e.target.value;
});

optionCountInput.addEventListener("change", e => {
    optionCount = e.target.value;
});
