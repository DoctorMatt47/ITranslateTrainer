let tests = JSON.parse(localStorage.getItem("tests"));
let currentIndex = parseInt(localStorage.getItem("tests.currentIndex"));

export const isEmpty = () => !localStorage.getItem("tests")?.length;

export const currentTest = () => tests[currentIndex];

export const nextTest = () => {
    if (currentIndex > tests.length) return;
    const test = tests[++currentIndex];
    localStorage.setItem("tests.currentIndex", currentIndex);
    return test;
};

export const setTests = testsToSet => {
    localStorage.setItem("tests", JSON.stringify(testsToSet));
    localStorage.setItem("tests.currentIndex", "0");
    tests = testsToSet;
    currentIndex = 0;
};

export const removeTests = () => {
    localStorage.removeItem("tests");
    localStorage.removeItem("tests.currentIndex");
};
