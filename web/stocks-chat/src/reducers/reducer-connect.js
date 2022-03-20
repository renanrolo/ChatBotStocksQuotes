import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../reducers/auth-action"

function ReducerConnect(component) {
    const mapStateToProps = state => (state)
    const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
    return connect(mapStateToProps, mapDispatchToProps)(component);
}

export default ReducerConnect;