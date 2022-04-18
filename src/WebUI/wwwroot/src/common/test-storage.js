let tests = JSON.parse(localStorage.getItem("tests"));
let currentIndex = parseInt(localStorage.getItem("tests.currentIndex"));
let correctCount = parseInt(localStorage.getItem("tests.correct"));
let incorrectCount = parseInt(localStorage.getItem("tests.incorrect"));
let skippedCount = parseInt(localStorage.getItem("tests.skipped"));

export const testCount = () => tests.length;

export const currentTestIndex = () => currentIndex;

export const isEmpty = () => !localStorage.getItem("tests")?.length;

export const currentTest = () => tests[currentIndex];

export const answerCount = () => [skippedCount, correctCount, incorrectCount];

export const nextTest = answer => {
    if (currentIndex > tests.length) return;
    const test = tests[++currentIndex];
    localStorage.setItem("tests.currentIndex", currentIndex);
    setAnswer(answer);
    return test;
};

export const setTests = testsToSet => {
    localStorage.setItem("tests", JSON.stringify(testsToSet));
    localStorage.setItem("tests.currentIndex", "0");
    localStorage.setItem("tests.correct", "0");
    localStorage.setItem("tests.incorrect", "0");
    localStorage.setItem("tests.skipped", "0");
    tests = testsToSet;
    currentIndex = 0;
};

export const removeTests = () => {
    localStorage.removeItem("tests");
    localStorage.removeItem("tests.currentIndex");
    localStorage.removeItem("tests.correct");
    localStorage.removeItem("tests.incorrect");
    localStorage.removeItem("tests.skipped");
};

const setAnswer = answer => {
    switch (answer) {
        case 0:
            localStorage.setItem("tests.skipped", ++skippedCount);
            break;
        case 1:
            localStorage.setItem("tests.correct", ++correctCount);
            break;
        case 2:
            localStorage.setItem("tests.incorrect", ++incorrectCount);
            break;
    }
};
