import React, {useContext} from 'react';
import {Link} from "react-router-dom";
import {Context} from "../../../index";
import {observer} from "mobx-react-lite";
import {Stack, Box, Text} from "@chakra-ui/react";
import "./sidebar.css";

const Sidebar = () => {
    const {userStore} = useContext(Context);
    return (
        <Box as="nav" position="fixed" right="0" top="38" pr={5}>
                <Stack spacing="10px">
                    <Link className="nav_link" to={"/" + userStore.user.username}> {/*TODO: user by id*/}
                        <Text fontSize='30px' color="yellow.100">Account</Text>
                    </Link>
                    <Link className="nav_link" to="/">
                        <Text fontSize='30px' color="yellow.100">Feed</Text>
                    </Link>
                    <Link className="nav_link" to="/login">
                        <Text
                            fontSize='30px'
                            color="yellow.100"
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