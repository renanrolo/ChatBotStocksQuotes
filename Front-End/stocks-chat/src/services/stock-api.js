import axios from 'axios';

console.log("url", process.env.REACT_APP_STOCK_API)

const stockApi = axios.create({
    baseURL: process.env.REACT_APP_STOCK_API
});

export default stockApi;
