import React, {memo, useCallback, useEffect, useState} from 'react';
import Sidebar from "../components/layout/sidebar/Sidebar";
import {IMessage} from "../types/IMessage";
import MessageService from "../services/MessageService";
import Messages from "../components/Messages";
import {Box, Button, Center, HStack, VStack} from "@chakra-ui/react";
import Header from "../components/layout/Header";
import LoadMoreButton from "../components/LoadMoreButton";

const Feed = () => {
    const [messages, setMessages] = useState<IMessage[]>([]);
    const [offset, setOffset] = useState<number>(0);
    const [showLoadMore, setShowLoadMore] = useState<boolean>(true);
    let limit: number = 10;

    const fetchMessages = useCallback(async function () {
        const fetchedMessages = await MessageService.getFollowingUsersMessages(offset, limit);
        if (fetchedMessages.data.messages.length === 0)
            setShowLoadMore(false);
        else
            setMessages(messages!.concat(fetchedMessages.data.messages));
    }, [offset])

    useEffect(() => {
        fetchMessages();
    }, [offset])

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
                </VStack>
                <Sidebar/>
            </HStack>
        </Center>
    );
};

export default memo(Feed);