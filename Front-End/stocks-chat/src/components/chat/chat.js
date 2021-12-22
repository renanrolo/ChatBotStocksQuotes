import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useParams } from 'react-router-dom';
import ChatWindow from './chat-window';
import ChatInput from './chat-input';
import stockApi from "../../services/stock-api";
import ReducerConnect from '../../reducers/reducer-connect';

function Chat({ User }) {
    const [chat, setChat] = useState([]);
    const latestChat = useRef(null);

    latestChat.current = chat;
    const { id } = useParams();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_STOCK_CHAT_HUB)
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(result => {
                connection.on('ReceiveMessage', message => {
                    const updatedChat = [...latestChat.current];
                    updatedChat.push(message);
                    setChat(updatedChat);
                })

                const chatParam = {
                    ChatId: id,
                    UserId: User.Id
                }
            })
            .catch(e => console.log('Connection failed: ', e));
        
    }, [User, id]);

    const sendMessage = async (user, message) => {
        const chatMessage = {
            From: user,
            Message: message,
            ChatId: id
        };

        try {
            stockApi.post("api/chat/message", chatMessage, { headers: { "Authorization": `Bearer ${User.Token}` } })
                .then(res => {
                }).catch(e => {
                    console.log("Unable to send message", e)
                });
        }
        catch (e) {
            console.log('Sending message failed.', e);
        }
    }

    return (
        <div>
            <ChatInput sendMessage={sendMessage} User={User} />
            <hr />
            <ChatWindow chat={chat} />
        </div>
    );
};

export default ReducerConnect(Chat);
