import {endpoints, languages, sendAsync} from "../common/common.js";

const emptyMessage = `You don't have any translations. Press "import" to add new.`;

const translationRowHtml = (translation, index) =>
    `<tr class="text-center">
        <th scope="row">${index}</th>
        <td class="w-20">${languages[translation.first.language]}</td>
        <td class="w-20">${languages[translation.second.language]}</td>
        <td class="w-30">${translation.first.string}</td>
        <td class="w-30">${translation.second.string}</td>
    </tr>`;

const translationRowsHtml = translations =>
    translations.map((t, i) => translationRowHtml(t, i)).reduce((acc, rowHtml) => acc + rowHtml);

const showTranslationRows = translations => {
    if (translations.length === 0) {
        document.getElementById("empty-message").innerText = emptyMessage;
        return;
    }
    document.getElementById("empty-message").innerText = "";
    document.getElementById("translations").innerHTML = translationRowsHtml(translations);
};

document.getElementById("import-sheet").addEventListener("change", e => {
    const sheetFile = e.currentTarget.files[0];
    const formData = new FormData();
    formData.append("sheet", sheetFile);
    sendAsync(endpoints.translationsSheet, "POST", formData)
        .then(_ => sendAsync(endpoints.translations))
        .then(showTranslationRows)
        .catch(err => console.log(err));
});

sendAsync(endpoints.translations).then(showTranslationRows).catch(e => console.log(e));
