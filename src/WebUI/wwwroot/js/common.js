const baseUrl = "https://localhost:7207";

export const endpoints = {
    translations: baseUrl + "/api/translations",
    translationsSheet: baseUrl + "/api/translations/sheet",
    quiz: baseUrl + "/api/quiz",
};

export const languages = {
    "0": "None",
    "1": "English",
    "2": "Russian",
    "3": "Ukrainian",
};

export const sendAsync = async (url, method = "GET", body = null) => {
    let response = await fetch(url, {method: method, body: body});
    return await response.json();
}
