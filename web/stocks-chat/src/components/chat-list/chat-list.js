import stockApi from "../../services/stock-api";
import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import ReducerConnect from '../../reducers/reducer-connect';

function ChatList({ User, Chats, onLogin, onGetChats }) {

    const navigate = useNavigate()

    const getChatList = function () {
        stockApi.get("api/chat", { headers: { "Authorization": `Bearer ${User.Token}` } })
            .then(res => {
                onGetChats(res.data)
            });
    }

    useEffect(getChatList, []);

    const signInChat = function (chatId) {
        stockApi.get(`api/chat/sign-in/${chatId}`, { headers: { "Authorization": `Bearer ${User.Token}` } })
            .then(res => {
                navigate(`/chat/${chatId}`)
            }).catch(e => {
                getChatList();
            });
    }

    return (
        <div className="container">
            <h1>Chat list</h1>
            <div className="card">
                <ul className="list-group list-group-flush">
                    {!!Chats && Chats.length > 0 ?
                        Chats.map((item) =>
                            <li key={item.id} className="list-group-item cursor-pointer" onClick={() => signInChat(item.id)}>{item.name}</li>
                        )
                        :
                        (<li className="list-group-item">No chats found</li>)
                    }
                </ul>
            </div>

            <br />

            <button type="button" className="btn btn-primary" onClick={getChatList}>Get Chats</button>
        </div>
    )

}

export default ReducerConnect(ChatList);