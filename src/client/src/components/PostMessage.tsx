import React, {Dispatch, FC, useState} from 'react';
import {Button, HStack, Input} from "@chakra-ui/react";
import MessageService from "../services/MessageService";
import {IMessage} from "../types/IMessage";

interface IPostMessageProps {
    messages: IMessage[],
    setMessages: Dispatch<React.SetStateAction<IMessage[] | undefined>>
}

const PostMessage: FC<IPostMessageProps> = ({messages, setMessages}) => {
    const [body, setBody] = useState<string>();

    const handlePost = async () => {
        // TODO: validate
        await MessageService.postMessage(body!)
            .then((res) => {
                    let message = res.data as IMessage;
                    setMessages([...messages, message]);
                    setBody("");
            });
    }

    return (
        <HStack>
            <Input
                placeholder='what you think now..'
                _placeholder={{ opacity: 2, color: 'yellow.100' }}
                focusBorderColor='yellow.500'
                color="white"
                defaultValue={body}
                onChange={(e) => setBody(e.target.value)}
            >
            </Input>
            <Button
                bg="yellow.100"
                color="#202023"
                onClick={handlePost}
                placeholder="What you think now"
            >
                POST
            </Button>
        </HStack>
    );
};

export default PostMessage;