import axios from 'axios';

const stockApi = axios.create({
    baseURL: process.env.REACT_APP_STOCK_API
});

export default stockApi;
