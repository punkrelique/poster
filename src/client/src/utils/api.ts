import axios from "axios";
import { getCookie } from "./cookies";

console.log(process.env.REACT_APP_BACKEND_API + " API")

export const API_URL = process.env.REACT_APP_BACKEND_API// "https://localhost:7289/api/"

const $api = axios.create({
    baseURL: API_URL // TODO: "process.env.WEB_API",
});

$api.interceptors.request.use((config: any) => {
    const token = getCookie("access_token");
    if (token) {
        config.headers.common["Authorization"] = `Bearer ${token}`;
    }
    return config;
});

export default $api;
