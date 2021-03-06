import React, { useState } from 'react';
import StockApi from "../../services/stock-api";
import { useNavigate } from 'react-router-dom';
import ReducerConnect from '../../reducers/reducer-connect';

function Login({ onLogin }) {

    const navigate = useNavigate();

    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    const [errors, setErrors] = useState([]);

    const handleSubmit = (evt) => {
        evt.preventDefault();

        if (validateForm()) {
            submitForm();
        }

    }

    const validateForm = function () {
        let _errors = [];

        if (!email) {
            _errors.push("Email is required");
        }

        if (!password) {
            _errors.push("Password is required");
        }

        setErrors(_errors);

        return _errors.length === 0;
    }

    const submitForm = function () {

        const form = {
            "Email": email,
            "Password": password
        }

        StockApi.post("api/Account/Login", form)
            .then(res => {
                const { token, expiration, id, email } = res.data;

                onLogin({
                    Token: token,
                    Expiration: expiration,
                    Id: id,
                    Email: email
                });

                navigate("/");

            }).catch(e => {
                const responseErrors = e?.response?.data?.errors;

                if (!!responseErrors) {
                    setErrors(responseErrors);
                }
                else {
                    setErrors(["Server didn't respond accordingly"]);
                }
            });
    }

    return (
        <div className="container">
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="loginInputEmail">Email address</label>
                    <input type="email" id="loginInputEmail" className="form-control" aria-describedby="emailHelp" placeholder="Enter email" value={email} onChange={e => setEmail(e.target.value)} />
                </div>
                <div className="form-group">
                    <label htmlFor="loginInputPassword">Password</label>
                    <input type="password" id="loginInputPassword" className="form-control" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
                </div>

                <br />

                <button type="submit" className="btn btn-primary">Submit</button>

                <br />
                <br />

                {errors.map((error, index) => <div key={index}> {error}  </div>)}

            </form>
        </div>
    );
}

export default ReducerConnect(Login);
