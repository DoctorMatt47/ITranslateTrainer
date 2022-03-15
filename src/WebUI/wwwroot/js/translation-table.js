import {endpoints, languages, sendAsync} from "./common.js";

const getTranslationRowHtml = (id, firstLanguage, secondLanguage, firstText, secondText) =>
    `<tr class="text-center"><th scope="row">${id}</th>
          <td class="w-20">${firstLanguage}</td>
          <td class="w-20">${secondLanguage}</td>
          <td class="w-30">${firstText}</td>
          <td class="w-30">${secondText}</td></tr>`;

const showTranslationTable = translations => {
    let translationsHtml = "";
    let counter = 1;
    for (let translation of translations) {
        translationsHtml += getTranslationRowHtml(counter,
            languages[translation.first.language],
            languages[translation.second.language],
            translation.first.string,
            translation.second.string);
        counter++;
    }
    const translationTableElement = document.getElementById("translations");
    translationTableElement.innerHTML = translationsHtml;
};

sendAsync(endpoints.translations).then(showTranslationTable).catch(e => console.log(e));

const importSheetInput = document.getElementById("import-sheet");
importSheetInput.addEventListener("change", e => {
    const sheetFile = e.currentTarget.files[0];
    const formData = new FormData();
    formData.append("sheet", sheetFile);
    sendAsync(endpoints.translationsSheet, "POST", formData).then(_ =>
        sendAsync(endpoints.translations).then(showTranslationTable))
        .catch(err => console.log(err));
});
