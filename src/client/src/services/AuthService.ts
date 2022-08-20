import $api from "../utils/api";
import {AxiosResponse} from "axios";
import IAuthResponse from "../types/IAuthResponse";

export default class AuthService {
    static async login(login: string, password: string, rememberMe: boolean)
        : Promise<AxiosResponse<IAuthResponse>> {
        return $api.post<IAuthResponse>(
            "/v1/authorization/login",
            {login, password, rememberMe},
            {headers: {"Content-Type": "multipart/form-data"}});
    }

    static async register(email: string, username: string, password: string)
        : Promise<AxiosResponse> {
        return $api.post("/v1/authorization/registration",
            {email, username, password},
            {headers: {"Content-Type": "multipart/form-data"}});
    }
}