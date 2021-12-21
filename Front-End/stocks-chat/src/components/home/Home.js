import ChatList from "../chat-list"
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../../reducers/auth-action"

function Home({ User }) {
    return (!!User ? <ChatList /> : <div className="container"><h1>Home</h1 ></div >);
}


const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(Home);