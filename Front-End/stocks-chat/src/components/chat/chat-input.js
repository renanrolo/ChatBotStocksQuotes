import React, { useState } from 'react';

const ChatInput = (props) => {
    const userEmail = props.User.Email;
    //const [user, setUser] = useState('');
    const [message, setMessage] = useState('');

    const onSubmit = (e) => {
        e.preventDefault();

        //const isUserProvided = user && user !== '';
        const isMessageProvided = message && message !== '';

        if (isMessageProvided) {
            props.sendMessage(userEmail, message);
        }
        else {
            alert('Please insert an user and a message.');
        }
    }

    // const onUserUpdate = (e) => {
    //     setUser(e.target.value);
    // }

    const onMessageUpdate = (e) => {
        setMessage(e.target.value);
    }

    return (
        <form
            onSubmit={onSubmit}>
            {/* <label htmlFor="user">User:</label>
            <br />
            <input
                id="user"
                name="user"
                value={user}
                onChange={onUserUpdate} />
            <br /> */}
            <label htmlFor="message">Message:</label>
            <br />
            <input
                type="text"
                id="message"
                name="message"
                value={message}
                onChange={onMessageUpdate} />
            <br /><br />
            <button type="submit" className="btn btn-light">Send</button>
        </form>
    )
};

export default ChatInput;