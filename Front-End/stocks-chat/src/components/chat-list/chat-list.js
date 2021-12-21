import SotckApi from "../../services/stock-api";
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../../reducers/auth-action"
import stockApi from "../../services/stock-api";
import React, { useState, useEffect } from 'react';

function ChatList({ Chats, onLogin }) {

    useEffect(() => {
        getChatList()
    });

    const getChatList = function () {
        stockApi.get("api/chat")
            .then(res => {
                console.log("chat list", res)
            });
    }

    return (
        <div className="container">
            <h1>Chat list</h1>
            <div className="card">
                <ul className="list-group list-group-flush">
                    {Chats.length > 0 ?
                        Chats.map((item, index) => 
                            <li key="index" className="list-group-item">{item}</li>
                        )
                        :
                        (<li className="list-group-item">No chats found</li>)
                    }
                </ul>
            </div>
        </div>
    )

}

const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(ChatList);