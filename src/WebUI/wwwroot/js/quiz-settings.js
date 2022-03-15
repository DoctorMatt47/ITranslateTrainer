import {endpoints, sendAsync} from "./common.js"

if (localStorage.getItem("tests")) window.location.href = "../html/quiz.html";

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
            localStorage.setItem("tests", JSON.stringify(response));
            localStorage.setItem("tests.currentIndex", "0");
            window.location.href = "../html/quiz.html";
            console.log(response);
        });
};

startButton.addEventListener("click", startClickHandler);

testCountInput.addEventListener("change", e => {
    testCount = e.target.value;
});

optionCountInput.addEventListener("change", e => {
    optionCount = e.target.value;
});
