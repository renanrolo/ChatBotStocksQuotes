import './app.css';
import Routes from "./routes";
import React from 'react';
import ReducerConnect from '../reducers/reducer-connect';

function App() {

  return (
    <Routes />
  );
}

export default ReducerConnect(App);
