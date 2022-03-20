export function onLogin(user) {
    return { type: 'AUTH_LOGIN_USER', payload: user }
}
export function onLogOut() {
    return { type: 'AUTH_LOGOUT' }
}

export function onGetChats(chats) {
    return { type: 'CHAT_GET_ALL', payload: chats }
}