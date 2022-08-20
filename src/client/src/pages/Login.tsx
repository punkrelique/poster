import {observer} from 'mobx-react-lite';
import React, {FC, useContext, useEffect, useRef, useState} from 'react';
import {Link} from "react-router-dom";
import {Context} from "../index";
import {Button, HStack, VStack, Text, Center} from "@chakra-ui/react";

const Login: FC = () => {
    const userRef = useRef<any>();
    const errRef = useRef<any>();

    const {userStore} = useContext(Context);

    const [user, setUser] = useState<string>("");
    const [pwd, setPwd] = useState<string>("");
    const [errMsg, setErrMsg] = useState<string>("");
    const [rememberMe, setRememberMe] = useState<boolean>(false);

    useEffect(() => {
        userRef?.current?.focus();
    }, []);

    useEffect(() => {
        setErrMsg("");
    }, [user, pwd]);

    async function handleSubmit(e: any) {
        e.preventDefault();
        await userStore.login(user, pwd, rememberMe)
            .then(() => {
                setUser("");
                setPwd("");
            });
    }

    // TODO: validation

    return (
        <div style={{marginTop: "5rem", color: "white"}}>
            <p
                ref={errRef}
                /*className={errMsg ? "errMsg" : "offscreen"}*/
                aria-live="assertive"
            > {errMsg}
            </p>
            <form onSubmit={handleSubmit}>
                <VStack>
                    <label htmlFor="username">Username or email:</label>
                    <input
                        type="text"
                        id="username"
                        ref={userRef.current!}
                        autoComplete="off"
                        onChange={(e) => setUser(e.target.value)}
                        value={user}
                        placeholder="Enter email or username"
                        required
                    />
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        autoComplete="off"
                        onChange={(e) => setPwd(e.target.value)}
                        value={pwd}
                        placeholder="Enter password"
                        required
                    />
                    <HStack>
                        <label htmlFor="rememberMe">Remember me</label>
                        <input
                            type="checkbox"
                            id="rememberMe"
                            onChange={() => setRememberMe(!rememberMe)}
                            checked={rememberMe}
                        />
                    </HStack>
                    <Button
                        type="submit"
                        colorScheme='yellow'
                    >
                        Sign-in
                    </Button>
                </VStack>
            </form>
            <div>
                <Center mt={4}>
                    <HStack style={{textAlign: "center"}}>
                        <Text>
                            Don't have an account yet?
                        </Text>
                        <Text
                            color="blue.200"
                            _hover={{
                                backgroundColor: "white",
                                color: "#202023"
                            }}>
                            <Link to='/signup'>Register</Link>
                        </Text>
                    </HStack>
                </Center>
            </div>
        </div>
    );
};

export default observer(Login);