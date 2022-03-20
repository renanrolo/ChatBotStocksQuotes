import ChatList from "../chat-list"
import ReducerConnect from "../../reducers/reducer-connect";

function Home({ User }) {
    return (!!User ? <ChatList /> : <div className="container"><h1>Home</h1 ></div >);
}

export default ReducerConnect(Home);