import React from "react";
import {
    Route,
    BrowserRouter as Router,
    Routes as Switch
} from "react-router-dom";

import Home from '../components/home';
import Login from '../components/login';
import Register from '../components/register';
import Nav from "../components/nav/nav"

const Routes = () => {
    return (
        <>
            <Router>
                <Nav />
                <Switch>
                    <Route element={<Home />} path="/" exact />
                    <Route element={<Login />} path="/Login" />
                    <Route element={<Register />} path="/Register" />
                </Switch>
            </Router>
        </>
    )
}

export default Routes;