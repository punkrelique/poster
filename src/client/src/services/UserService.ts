import {IUser, IUsers} from "../types/IUser";
import {AxiosResponse} from "axios";
import $api from "../utils/api";
import IFollowing from "../types/IFollowing";

export default class UserService {
    static async getUser(): Promise<AxiosResponse<IUser>> {
        return $api.get<IUser>("/v1/user")
    }

    static async getUserById(userId: string): Promise<AxiosResponse<IUser>> {
        return $api.get<IUser>("/v1/user", {params: userId});
    }

    static async getUserByUsername(username: string): Promise<AxiosResponse<IUser>> {
        return $api.get<IUser>("/v1/user/username", {params: {username}});
    }

    static async getUserList(username: string, offset: number, limit: number)
        : Promise<AxiosResponse<IUsers>> {
        return $api.get<IUsers>("/v1/user/list/", {params: {username, offset, limit}});
    }

    static async getUserFollowersById(userId: string): Promise<AxiosResponse> {
        return $api.get("/v1/user/followers", {params: {userId}});
    }

    static async getUserFollowers(): Promise<AxiosResponse> {
        return $api.get("/v1/user/followers");
    }

    static async followUser(to: string): Promise<AxiosResponse<void>> {
        return $api.post("/v1/user/follow", {},{params: {to}});
    }

    static async unfollowUser(from: string): Promise<AxiosResponse<void>> {
        return $api.post("/v1/user/unfollow", {}, {params: {from}});
    }

    static async isFollowing(username: string): Promise<AxiosResponse<IFollowing>> {
        return $api.get<IFollowing>("/v1/user/isfollowing", {params: {username}});
    }
}