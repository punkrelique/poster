import React, {memo, useEffect, useState} from 'react';
import Sidebar from "../components/layout/sidebar/Sidebar";
import {IMessages} from "../types/IMessage";
import MessageService from "../services/MessageService";
import Messages from "../components/messages/Messages";
import {Box, HStack, Spacer, VStack} from "@chakra-ui/react";
import Header from "../components/layout/Header";

const Feed = () => {
    const [messages, setMessages] = useState<IMessages>();
    let offset: number = 0;
    let limit: number = 10;

    useEffect(() => {
        (async function () {
            const messages = await MessageService.getFollowingUsersMessages(offset, limit);
            setMessages(messages.data);
        })()
    }, [])

    return (
        <HStack m={10}>
            <VStack>
                <Header/>
                <Box>
                    <h1>Following users' messages:</h1>
                    {
                        messages?.messages
                            ? <Messages messages={messages!.messages}/>
                            : null
                    }
                </Box>
            </VStack>
            <Spacer/>
            <Sidebar/>
        </HStack>
    );
};

export default memo(Feed);