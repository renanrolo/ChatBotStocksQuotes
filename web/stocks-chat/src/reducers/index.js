import { createStore } from 'redux';

import AuthReducer from './auth-reducer';

const store = createStore(AuthReducer);

export default store;