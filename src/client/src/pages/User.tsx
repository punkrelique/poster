import React, {FC, memo, useCallback, useContext, useEffect, useState} from 'react';
import Sidebar from "../components/layout/sidebar/Sidebar";
import {Navigate, useParams} from "react-router-dom";
import UserService from "../services/UserService";
import {IUser} from "../types/IUser";
import MessageService from "../services/MessageService";
import {IMessage} from "../types/IMessage";
import Messages from "../components/Messages";
import {Box, HStack, VStack, Spacer, Button, Center} from "@chakra-ui/react";
import Header from "../components/layout/Header";
import {Context} from "../index";
import PostMessage from "../components/PostMessage";
import {observer} from "mobx-react-lite";
import LoadMoreButton from "../components/LoadMoreButton";

const User: FC = () => {
    const {userStore} = useContext(Context);
    const [user, setUser] = useState<IUser>();
    const [messages, setMessages] = useState<IMessage[]>([]);
    const [isFollowed, setIsFollowed] = useState<Boolean>(false);
    const [offset, setOffset] = useState<number>(0);
    const limit: number = 10;
    const [fetching, setFetching] = useState<boolean>(true);
    const {username} = useParams();
    const [showLoadMore, setShowLoadMore] = useState<boolean>(true);

    const handleFollowButton = async () => {
        if (isFollowed)
            await UserService.unfollowUser(user!.id!);
        else
            await UserService.followUser(user!.id!);

        setIsFollowed(!isFollowed);
    }

    const fetchUserInformation = useCallback(async function () {
        if (username !== userStore.user.username) {
            try {
                const user = await UserService.getUserByUsername(username!);
                setUser(user.data);
                const isFollowing = await UserService.isFollowing(username!);
                setIsFollowed(isFollowing.data.isFollowing);
            } catch (e: any) {
                return <Navigate to="/error/404"/> // TODO: 404 on notfound user
            }
        } else {
            setUser(userStore.user);
        }
    }, [username]);

    const fetchMessages = useCallback(async function () {
        const fetchedMessages = await MessageService.getUsersMessagesByUsername(username!, offset, limit);
        if (fetchedMessages.data.messages.length === 0)
            setShowLoadMore(false);
        else
            setMessages(messages.concat(fetchedMessages.data.messages));
    }, [offset])

    useEffect(() => {
        fetchUserInformation();
        fetchMessages();
    }, [username])

    useEffect(() => {
        fetchMessages();
    }, [offset])

    // TODO: handle delete message function

    return (
        <Center>
            <HStack m={10}>
                <VStack>
                    <Header/>
                    <Box>
                        <Box color="white">
                            {
                                username === userStore.user.username
                                    ? <p>email: {user?.email}</p>
                                    : null
                            }
                            <p>username: {user?.username}</p>
                            <p>date created: {user?.dateCreated.toString()}</p>
                            {
                                username !== userStore.user.username
                                    ? <Button
                                        bg="yellow.100"
                                        color="#202023"
                                        onClick={handleFollowButton}
                                    >
                                        {isFollowed ? "Unfollow" : "Follow"}
                                    </Button>
                                    : <PostMessage
                                        messages={messages!}
                                        setMessages={setMessages}
                                    />
                            }
                            <h1>{user?.username}'s posts:</h1>
                        </Box>
                        <Box>
                            {
                                // is it the proper way to render things on load?
                                messages
                                    ? <VStack>
                                        <Messages
                                            messages={messages}
                                            onEmptyText={"Here will be posted messages.. Empty for now"}
                                        />
                                        <LoadMoreButton
                                            offset={offset}
                                            setOffset={setOffset}
                                            limit={limit}
                                            show={showLoadMore}
                                        >
                                            Load more
                                        </LoadMoreButton>
                                    </VStack>
                                    : null
                            }
                        </Box>
                    </Box>
                </VStack>
                <Spacer/>
                <Sidebar/>
            </HStack>
        </Center>
    );
};

export default memo(observer(User));