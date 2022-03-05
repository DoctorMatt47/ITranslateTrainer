export const sendAsync = async (url, method = "GET", body = null) => {
    let response = await fetch(url, {method: method, body: body});
    return await response.json();
}
