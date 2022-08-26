import React, {memo, useEffect, useState} from 'react';
import Sidebar from "../components/layout/sidebar/Sidebar";
import {IMessage} from "../types/IMessage";
import MessageService from "../services/MessageService";
import Messages from "../components/Messages";
import {Box, Button, Center, HStack, VStack} from "@chakra-ui/react";
import Header from "../components/layout/Header";

const Feed = () => {
    const [messages, setMessages] = useState<IMessage[]>();
    const [offset, setOffset] = useState<number>(0);
    let limit: number = 10;

    useEffect(() => {
        (async function () {
            const messages = await MessageService.getFollowingUsersMessages(offset, limit);
            setMessages(messages.data.messages);
        })()
    }, [])

    return (
        <Center>
            <HStack m={10}>
                <VStack>
                    <Header/>
                    <Box>
                        <h1>Following users' messages:</h1>
                        {
                            messages
                                ? <VStack>
                                    <Messages
                                        messages={messages}
                                        onEmptyText={"Here will be messages from your following users.."}
                                    />
                                    <Button
                                        bg="yellow.100"
                                        color="#202023"
                                        onClick={() => setOffset(limit+offset)}
                                    >
                                        Load more
                                    </Button>
                                </VStack>
                                : null
                        }
                    </Box>
                </VStack>
                <Sidebar/>
            </HStack>
        </Center>
    );
};

export default memo(Feed);