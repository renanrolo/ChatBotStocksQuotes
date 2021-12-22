import React from 'react';

import Message from './message';

const ChatWindow = (props) => {
    const chat = props.chat
        .map(m => <Message
            key={Date.now() * Math.random()}
            from={m.from}
            message={m.message} />);
    return (
        <div>
            {chat}
        </div>
    )
};

export default ChatWindow;