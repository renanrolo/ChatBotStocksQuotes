import './app.css';
import Routes from "./routes";
import React from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../reducers/auth-action"


function App() {

  return (
    <Routes />
  );
}

const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(App);
