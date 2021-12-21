import stockApi from "../services/stock-api";

const key = "stocks_auth";

const INITIAL_STATE = loadState();

export default function authReducer(state = INITIAL_STATE, action) {

    console.log("reducer foi chamado", action.type);

    switch (action.type) {

        case 'AUTH_LOGIN_USER': {
            const user = action.payload;

            return {
                ...state,
                User: user
            };
        }

        case 'AUTH_LOGOUT': {
            return {
                ...state,
                User: null
            };
        }
            
        // case 'ADD_TURNO':
        //     {
        //         const newTurnos = [...state.Turnos];
        //         newTurnos.push({ ini: '', fim: '' });
        //         return saveState({ ...state, Turnos: newTurnos })
        //     }

        // case 'CHANGE_TURNO_VALUE':
        //     {
        //         const { propName, value, index } = action.payload;

        //         const newTurno = (propName === "ini") ?
        //             { ini: value } :
        //             { fim: value }

        //         const newTurnos = state.Turnos.map((item, i) => {
        //             if (i !== index) { // This isn't the item we care about - keep it as-is
        //                 return item
        //             }
        //             return { // Otherwise, this is the one we want - return an updated value
        //                 ...item, ...newTurno
        //             }
        //         });

        //         previewLastTime(propName, index, newTurnos, state.CargaHoraria)

        //         return saveState({ ...state, Turnos: newTurnos })
        //     }

        // case 'UPDATE_CARGA_HORARIA':
        //     {
        //         const newTurnos = state.Turnos.map((item, i) => {
        //             return {
        //                 ...item
        //             }
        //         });

        //         previewLastTime('ini', 0, newTurnos, action.payload)

        //         return saveState({ ...state, CargaHoraria: action.payload, Turnos: newTurnos })
        //     }

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
            console.log("user loaded from localStorage", state)
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
