const key = "stocks_chat_user";

export function IsLoged() {
    var user = loadState();

    return !!user;
}

export function Login(user) {
    saveState(user);
}

function saveState(state) {
    localStorage.setItem(key, JSON.stringify(state));
    return state;
}

function loadState() {
    try {
        const state = JSON.parse(localStorage.getItem(key))
        if (state) {
            return state;
        }
    } catch (e) {
        if (window.console) {
            console.log("invalid loadJson", e)
        }
    }

    return null;
}
export function logOut() {
    localStorage.setItem(key, null);
}