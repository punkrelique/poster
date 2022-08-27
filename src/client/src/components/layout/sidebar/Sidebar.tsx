import React, {useContext} from 'react';
import {Link} from "react-router-dom";
import {Context} from "../../../index";
import {observer} from "mobx-react-lite";
import {Stack, Box, Text} from "@chakra-ui/react";
import "./sidebar.css";

const Sidebar = () => {
    const {userStore} = useContext(Context);
    return (
        <Box as="nav" pr={5}>
                <Stack color="black" position="fixed" top={7} ml={2} spacing="10px">
                    <Link className="nav_link" to={"/" + userStore.user.username}> {/*TODO: user by id*/}
                        <Text fontSize='30px'>Account</Text>
                    </Link>
                    <Link className="nav_link" to="/">
                        <Text fontSize='30px'>Feed</Text>
                    </Link>
                    <Link className="nav_link" to="/login">
                        <Text
                            fontSize='30px'
                            onClick={() => userStore.logout()}
                        >
                            Logout
                        </Text>
                    </Link>
                </Stack>
        </Box>
    );
};

export default observer(Sidebar);