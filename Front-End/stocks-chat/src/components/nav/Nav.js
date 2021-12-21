import {
    Link
} from "react-router-dom";
import { connect } from 'react-redux'
import { bindActionCreators } from "redux"
import * as AuthAction from "../../reducers/auth-action"

function Nav({ User, onLogOut }) {

    const logOut = function (e) {
        e.preventDefault();
        onLogOut();
    }

    return (
        <header>
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <Link className="navbar-brand" to="/">Home</Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">

                        {!!User ?
                            (
                                <>
                                    <li className="nav-item">
                                        <p className="nav navbar-text">Hello, {User.Email}</p>
                                    </li>
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

const mapStateToProps = state => (state)
const mapDispatchToProps = dispatch => bindActionCreators(AuthAction, dispatch)
export default connect(mapStateToProps, mapDispatchToProps)(Nav);
