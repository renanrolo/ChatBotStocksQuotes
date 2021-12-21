import './app.css';
import Routes from "./routes";
import React, { useEffect } from 'react';
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../reducers/auth-action"

import stockApi from "../services/stock-api";

function App({ onLogin, onLogOut }) {

  useEffect(() => {
    initialState()
  });

  function initialState() {
    // stockApi.get("api/Account")
    //   .then(res => {
    //     console.log("Who Am I", res)
    //     const user = res.data;

    //     if (user.logged) {
    //       onLogin({ Name: user.Email });
    //     }
    //     else {
    //       onLogOut();
    //     }
    //   });
  }

  return (
    <Routes />
  );
}

const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(App);
