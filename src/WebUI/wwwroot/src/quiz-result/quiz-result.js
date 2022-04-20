import {answerCount, currentTest, isEmpty, removeTests} from "../common/test-storage.js";
import {color} from "../common/common.js";

if (isEmpty()) window.location.href = "../quiz-settings/quiz-settings.html";
if (currentTest()) window.location.href = "../quiz/quiz.html";

const sectorColors = [color.diagramGray, color.diagramGreen, color.diagramRed];

const circleChart = document.getElementById("circle-chart");

const skippedCountElement = document.getElementById("skipped-count");
const correctCountElement = document.getElementById("correct-count");
const incorrectCountElement = document.getElementById("incorrect-count");
const correctPercentElement = document.getElementById("correct-percent");

const toPercent = num => round(num * 100) + "%";

const round = num => Math.round((num + Number.EPSILON) * 100) / 100;

document.getElementById("restart-button").addEventListener("click", () => {
    removeTests();
    window.location.href = "../quiz-settings/quiz-settings.html";
});

const answers = answerCount();
skippedCountElement.innerText = answers[0];
correctCountElement.innerText = answers[1];
incorrectCountElement.innerText = answers[2];

const testCount = answers.reduce((acc, elem) => acc + elem);
const percents = answers.map((elem, i) => [sectorColors[i], round(elem / testCount)]).sorted((a, b) => b[1] - a[1]);

correctPercentElement.innerText = toPercent(percents[0][1]);

const gradientParams = `${percents[0][0]} 0.00% ${toPercent(percents[0][1])},
    ${percents[1][0]} ${toPercent(percents[1][1])} ${toPercent(1 - percents[2][1])},
    ${percents[2][0]} ${toPercent(1 - percents[2][1])}`;

circleChart.style.background = `conic-gradient(${gradientParams})`;
