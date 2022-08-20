import React, {FC} from 'react';
import {IMessages} from "../../types/IMessage";
import {HStack, Stack, Text} from "@chakra-ui/react";
import {Link} from "react-router-dom";

const Messages: FC<IMessages> =
    ({messages}) => {
        return (
            <Stack>
                {
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
                                    <Link to={"/" + message.username}>@{message.username}</Link>
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