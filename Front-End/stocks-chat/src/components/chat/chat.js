import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useParams } from 'react-router-dom';
import ChatWindow from './chat-window';
import ChatInput from './chat-input';
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import stockApi from "../../services/stock-api";
import * as AuthAction from "../../reducers/auth-action";

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
                console.log("entered chat")

                connection.on('ReceiveMessage', message => {
                    console.log("ReceiveMessage", message);
                    const updatedChat = [...latestChat.current];
                    updatedChat.push(message);
                    setChat(updatedChat);
                })

                const chatParam = {
                    ChatId: id,
                    UserId: User.Id
                }

                connection.send("EnterChat", chatParam)
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
                    console.log("message sent", res)
                }).catch(e => {
                    console.log("Unable to send message", e)
                });

            // await fetch('https://localhost:5001/api/chat/message', {
            //     method: 'POST',
            //     body: JSON.stringify(chatMessage),
            //     headers: {
            //         'Content-Type': 'application/json'
            //     }
            // });
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

const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(Chat);
