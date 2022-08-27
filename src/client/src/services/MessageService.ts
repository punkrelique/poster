import {IMessages} from "../types/IMessage";
import {AxiosResponse} from "axios";
import $api from "../utils/api";

export default class MessageService {
    static async getUsersMessages(offset: number, limit: number)
        : Promise<AxiosResponse<IMessages>> {
        return $api.get<IMessages>("/v1/message/caller", {params: {offset, limit}});
    }

    static async getFollowingUsersMessages(offset: number, limit: number)
        : Promise<AxiosResponse<IMessages>> {
        return $api.get<IMessages>("/v1/message", {params: {offset, limit}});
    }

    static async getUsersMessagesByUsername(username: string, offset: number, limit: number)
        : Promise<AxiosResponse<IMessages>> {
        return $api.get<IMessages>(
            "/v1/message/" + username,
            {
                params: {offset, limit}
            });
    }

    static async postMessage(body: string): Promise<AxiosResponse> {
        return $api.post(
            "/v1/message",
            {body},
            {headers: {"Content-Type": "multipart/form-data"}});
    }

    static async deleteMessage(messageId: string): Promise<AxiosResponse> {
        return $api.delete("/v1/message", {params: messageId});
    }
}