import {
    Link
} from "react-router-dom";
import ReducerConnect from "../../reducers/reducer-connect";
import { useNavigate } from 'react-router-dom';

function Nav({ User, onLogOut }) {

    const navigate = useNavigate();

    const logOut = function (e) {
        e.preventDefault();
        onLogOut();
        navigate("/");
    }

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-dark bg-dark">
                <Link className="navbar-brand" to="/">
                    {!!User ? "Hello, " + User.Email
                        : "Home"
                    }
                </Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">

                        {!!User ?
                            (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/" onClick={logOut}>Logout</Link>
                                    </li>
                                </>
                            )
                            :
                            (
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/login">Login</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="/register">Register</Link>
                                    </li>
                                </>
                            )
                        }

                    </ul>

                </div>
            </nav>
        </header>
    );
}

export default ReducerConnect(Nav);
