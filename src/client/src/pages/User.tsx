import React, {FC, memo, useContext, useEffect, useState} from 'react';
import Sidebar from "../components/layout/sidebar/Sidebar";
import {Navigate, useParams} from "react-router-dom";
import UserService from "../services/UserService";
import IUser from "../types/IUser";
import MessageService from "../services/MessageService";
import {IMessages} from "../types/IMessage";
import Messages from "../components/messages/Messages";
import {Box, HStack, VStack, Spacer} from "@chakra-ui/react";
import Header from "../components/layout/Header";
import {Context} from "../index";

const User: FC = () => {
    const {userStore} = useContext(Context);
    const [user, setUser] = useState<IUser>();
    const [messages, setMessages] = useState<IMessages>();
    let offset: number = 0;
    let limit: number = 10;
    const [fetching, setFetching] = useState<boolean>(true);
    const {username} = useParams();

    useEffect( () => {
        (async function () {
            if (username !== userStore.user.username) {
                try {
                    const user = await UserService.getUserByUsername(username!);
                    const messages = await MessageService.getUsersMessagesByUsername(username!, offset, limit);
                    setUser(user.data);
                    setMessages(messages.data);
                }
                catch (e: any) {
                    return <Navigate to="/error/404"/>
                }
            }
            else {
                setUser(userStore.user);
                const messages = await MessageService.getUsersMessages(offset, limit);
                setMessages(messages.data);
            }
        })();
    }, []);

    // TODO: follow/unfollow if not users's user
    // TODO: handle delete message function
    // TODO: handle post message function
    // TODO: show id/email only if it is user's account

    return (
        <HStack m={10}>
            <VStack>
                <Header/>
                <Box>
                    <p>id: {user?.id}</p>
                    <p>email: {user?.email}</p>
                    <p>username: {user?.username}</p>
                    <p>date created: {user?.dateCreated.toString()}</p>
                    <h1>Your posts:</h1>
                    <div>
                        {
                            // is it the proper way to render things on load?
                            messages?.messages
                                ? <Messages messages={messages!.messages} />
                                : null
                        }
                    </div>
                </Box>
            </VStack>
            <Spacer/>
            <Sidebar/>
        </HStack>
    );
};

export default memo(User);