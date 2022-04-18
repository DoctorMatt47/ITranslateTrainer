import {baseUrl} from "./env.js";

export const endpoints = {
    translations: baseUrl + "/api/translations",
    translationsSheet: baseUrl + "/api/translations/sheet",
    quiz: baseUrl + "/api/quiz",
};

export const language = {
    "0": "None",
    "1": "English",
    "2": "Russian",
    "3": "Ukrainian",
};

export const answer = {
    skipped: 0,
    incorrect: 1,
    correct: 2,
};

export const color = {
    darkRed: "#22090F",
    darkGreen: "#132b15",
    lightRed: "#9E2927",
    lightGreen: "#499C54",
    lightGrey: "#686666",
};

export const sendAsync = async (url, method = "GET", body = null) => {
    let response = await fetch(url, {method: method, body: body});
    return await response.json();
};

Array.prototype.sorted = function (compare) {
    const arrayCopy = [...this];
    arrayCopy.sort(compare);
    return arrayCopy;
};
