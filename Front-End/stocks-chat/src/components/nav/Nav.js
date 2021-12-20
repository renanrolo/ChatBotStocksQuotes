import {
    Link
} from "react-router-dom";
import { IsLoged, logOut } from "../../services/auth-service"

function Nav() {
    return (
        <header>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <Link className="navbar-brand" to="/">Home</Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">
                        {IsLoged() ?
                            (<li className="nav-item">
                                <Link className="nav-link" to="/" onClick={logOut}>Logout</Link>
                            </li>)
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

export default Nav;
