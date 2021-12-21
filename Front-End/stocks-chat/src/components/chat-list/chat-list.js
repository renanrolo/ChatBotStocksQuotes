import SotckApi from "../../services/stock-api";
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../../reducers/auth-action"
import stockApi from "../../services/stock-api";
import React, { useState, useEffect } from 'react';

function ChatList({ User, Chats, onLogin, onGetChats }) {

    // useEffect(() => {
    //     getChatList()
    // });

    const getChatList = function () {
        stockApi.get("api/chat", { headers: { "Authorization": `Bearer ${User.Token}` } })
            .then(res => {
                onGetChats(res.data)
                console.log("chat list", res)
            });
    }

    return (
        <div className="container">
            <h1>Chat list</h1>
            <div className="card">
                <ul className="list-group list-group-flush">
                    {console.log(Chats)}
                    {Chats.length > 0 ?
                        Chats.map((item) => 
                            <li key={item.id} className="list-group-item">{item.name}</li>
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

const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(ChatList);