import React from 'react';

const Message = (props) => (
    <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px' }}>
        {console.log(props)}
        <p><strong>{props.from}</strong> says:</p>
        <p>{props.message}</p>
    </div>
);

export default Message;