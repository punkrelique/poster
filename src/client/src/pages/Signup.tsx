import React, {useContext, useEffect, useRef, useState} from 'react';
import {Link, Navigate} from "react-router-dom";
import {Context} from "../index";
import {observer} from "mobx-react-lite";
import {Button, Center, HStack, Text, VStack} from '@chakra-ui/react';

const Signup = () => {
    const userRef = useRef<any>();
    const errRef = useRef<any>();

    const {userStore} = useContext(Context);

    const [user, setUser] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [pwd, setPwd] = useState<string>("");
    const [errMsg, setErrMsg] = useState<string>("");

    useEffect(() => {
        userRef?.current?.focus();
    }, []);

    useEffect(() => {
        setErrMsg("");
    }, [user, pwd]);

    async function handleSubmit(e: any) {
        e.preventDefault();

        await userStore.register(email, user, pwd)
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
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        ref={userRef.current!}
                        autoComplete="off"
                        onChange={(e) => setEmail(e.target.value)}
                        value={email}
                        required
                    />
                    <label htmlFor="username">Username:</label>
                    <input
                        type="text"
                        id="username"
                        autoComplete="off"
                        onChange={(e) => setUser(e.target.value)}
                        value={user}
                        required
                    />
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        autoComplete="off"
                        onChange={(e) => setPwd(e.target.value)}
                        value={pwd}
                        required
                    />
                    <Button
                        type="submit"
                        colorScheme='yellow'
                    >
                        Sign-up
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
                            <Link to='/login'>Login</Link>
                        </Text>
                    </HStack>
                </Center>
            </div>
        </div>
    );
};

export default observer(Signup);