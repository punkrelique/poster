import React, {FC} from 'react';
import {IMessage, IMessages} from "../types/IMessage";
import {Box, HStack, Stack, Text} from "@chakra-ui/react";
import {Link} from "react-router-dom";

interface IMessagesProps {
    messages: IMessage[],
    onEmptyText: string
}

const Messages: FC<IMessagesProps> =
    ({messages, onEmptyText}) => {
        return (
            <Stack>
                {
                    messages.length === 0
                        ? <Box
                            w="500px"
                            bg="#eeeeee"
                            borderRadius="2px"
                            color="black"
                        >
                            {onEmptyText}
                        </Box> :

                        messages.map(message => {
                            return (
                                <Stack
                                    w="500px"
                                    bg="#eeeeee"
                                    key={message.id}
                                    borderRadius="2px"
                                    color="black"
                                >
                                    <HStack spacing="20px">
                                        <Text _hover={{
                                            textDecoration: "underline solid #202023 3px",
                                        }}>
                                            <Link to={"/" + message.username}>@{message.username}</Link>
                                        </Text>
                                        <Text>{message.dateCreated.toString()}</Text>
                                    </HStack>
                                    <Text>{message.body}</Text>
                                </Stack>
                            )
                        })
                }
            </Stack>
        );
    };

export default Messages;