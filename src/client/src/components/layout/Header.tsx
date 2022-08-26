import {Box, Button, HStack, Input, Stack, Text, VStack} from "@chakra-ui/react";
import {IUser} from "../../types/IUser";
import UserService from "../../services/UserService";
import React, {FC, useEffect, useState} from "react";
import {Link} from "react-router-dom";
import {observer} from "mobx-react-lite";

const Header: FC = () => {
    const [input, setInput] = useState<string>("");
    const [users, setUsers] = useState<IUser[]>();
    const [show, setShow] = useState<boolean>(false);

    let offset = 0;
    let limit = 10;

    const handleSearch = async (e: any) => {
        if (e.keyCode === 13 && input !== "") {
            const users = await UserService.getUserList(input, offset, limit);
            setUsers(users.data.users);
            setShow(true);
        }
    }

    return (
        <Stack>
            <HStack>
                <Input
                    w="500px"
                    value={input}
                    onChange={(e) => setInput(e.target.value)}
                    onClick={() => setShow(true)}
                    onKeyDown={(e) => handleSearch(e)}
                    placeholder='Search for users!'
                    _placeholder={{color: 'yellow.200'}}
                    focusBorderColor='yellow.500'
                    color="white"
                />
            </HStack>
            <Box
                w="500px"
                maxHeight="100px"
                overflow="auto"
                bg="#eeeeee"
                borderRadius="2px"
                color="black"
                onBlur={() => setShow(false)}
                paddingRight={2}
                style={{
                    display: show && input !== "" ? "block" : "none",
                    scrollbarColor:  "#FEFCBF #D69E2E"
                }}
            >
                {
                    users
                        ? users!.length === 0 ? "No people matching query were found.." : users!.map((user, index) => {
                            return (
                                <Box key={index}>
                                    <Text _hover={{
                                        textDecoration: "underline solid #202023 3px"
                                    }}>
                                        <Link to={"/" + user.username}>@{user.username}</Link>
                                    </Text>
                                </Box>
                            )
                        })
                        : null
                }
            </Box>
        </Stack>
    );
};

export default observer(Header);