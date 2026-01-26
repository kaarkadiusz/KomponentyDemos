function SetLocalStorage(key, value) {
    localStorage.setItem(key, value);
}

function GetLocalStorage(key) {
    return localStorage.getItem(key);
}

function SetTheme(theme) {
    document.body.className = theme;
}