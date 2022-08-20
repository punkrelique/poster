import IUser from "../types/IUser";
import {makeAutoObservable} from "mobx";
import AuthService from "../services/AuthService";
import UserService from "../services/UserService";
import {deleteCookie, setCookie} from "../utils/cookies";

export default class UserStore {
    user = {} as IUser;
    isAuthenticated = false;

    constructor() {
        makeAutoObservable(this);
    }

    setAuth(bool: boolean) {
        this.isAuthenticated = bool;
    }

    setUser(user: IUser) {
        this.user = user;
    }

    async login(login: string, password: string, rememberMe: boolean) {
        try {
            const response = await AuthService.login(login, password, rememberMe)
                .then(async res => {
                    setCookie(
                        "access_token",
                        res.data.access_token,
                        rememberMe ? 9999 : 1);
                    const user = await UserService.getUser();
                    this.setAuth(true);
                    this.setUser(user.data);
                });
        } catch (e: any) {
            console.log(e.response?.data?.message)
        }
    }

    async register(email: string, login:string, password: string) {
        try {
            await AuthService.register(email, login, password);
        } catch (e: any) {
            console.log(e.response?.data?.message)
        }
    }

    async logout() {
        try {
            deleteCookie("access_token");
            this.setAuth(false);
            this.setUser({} as IUser);
        } catch (e: any) {
            console.log(e.response?.data?.message)
        }
    }

    async getUser() {
        try {
            const user = (await UserService.getUser()).data;
            this.setUser({
                email: user.email,
                username: user.username,
                id: user.id,
                dateCreated: user.dateCreated});
            if (user)
                this.setAuth(true);
        } catch (e: any){
            console.log(e.response?.data?.message);
        }
    }
}