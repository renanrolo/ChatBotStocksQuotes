const key = "stocks_auth";

const INITIAL_STATE = loadState();

export default function authReducer(state = INITIAL_STATE, action) {

    console.log("reducer foi chamado", action.type);

    switch (action.type) {

        case 'AUTH_LOGIN_USER': {
            const user = action.payload;

            return saveState({
                ...state,
                User: user
            });
        }

        case 'AUTH_LOGOUT': {
            return saveState({
                Chats: null,
                User: null
            });
        }
            
        case 'CHAT_GET_ALL': {
            const chats = action.payload;

            return saveState({
                ...state,
                Chats: chats
            });
        }

        default:
            return state;
    }
}

function saveState(state) {
    localStorage.setItem(key, JSON.stringify(state));
    return state;
}

function loadState() {
    try {
        const state = JSON.parse(localStorage.getItem(key))
        if (!!state) {
            return state;
        }
    } catch (e) {
        if (window.console) {
            console.log("invalid loadJson", e)
        }
    }

    return {
        User: null,
        Chats: []
    };
}
