import React, { useState } from 'react';
import StockApi from "../../services/stock-api";

function Login() {
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

        StockApi.post("/Account/Login", form)
            .then(res => {
                console.log(res);
                Login(email);
            }).catch(e => {
                console.log("caiu no erro");
                console.log(e.response.data.errors);

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
                    <label for="exampleInputEmail1">Email address</label>
                    <input type="email" className="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email" value={email} onChange={e => setEmail(e.target.value)} />
                </div>
                <div className="form-group">
                    <label for="exampleInputPassword1">Password</label>
                    <input type="password" className="form-control" id="exampleInputPassword1" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} />
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

export default Login;