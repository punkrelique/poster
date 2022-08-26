import React, {useContext, useEffect, useRef, useState} from 'react';
import {Link, Navigate} from "react-router-dom";
import {Context} from "../index";
import {observer} from "mobx-react-lite";
import {
    Button,
    Center,
    FormControl,
    HStack,
    Input,
    InputGroup,
    InputRightElement,
    Text,
    VStack
} from '@chakra-ui/react';

const Signup = () => {
    const userRef = useRef<any>();
    const errRef = useRef<any>();

    const [show, setShow] = React.useState(false)
    const handleClick = () => setShow(!show)

    const {userStore} = useContext(Context);

    const [user, setUser] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [pwd, setPwd] = useState<string>("");
    const [repPwd, setRepPwd] = useState<string>("");
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
        userStore.isAuthenticated ? <Navigate to="/"/> :
        <div style={{marginTop: "5rem", color: "white"}}>
            <p
                ref={errRef}
                /*className={errMsg ? "errMsg" : "offscreen"}*/
                aria-live="assertive"
            > {errMsg}
            </p>
            <form onSubmit={handleSubmit}>
                <FormControl>
                    <Center>
                        <VStack>
                            <label htmlFor="email">Email:</label>
                            <Input
                                type="email"
                                id="email"
                                ref={userRef.current!}
                                placeholder="Enter email"
                                autoComplete="off"
                                onChange={(e) => setEmail(e.target.value)}
                                value={email}
                                required
                            />
                            <label htmlFor="username">Username:</label>
                            <Input
                                type="text"
                                id="username"
                                autoComplete="off"
                                placeholder="Enter username"
                                onChange={(e) => setUser(e.target.value)}
                                value={user}
                                required
                            />
                            <label htmlFor="password">Password:</label>
                            <InputGroup size='md'>
                                <Input
                                    type={show ? 'text' : 'password'}
                                    id="password"
                                    autoComplete="off"
                                    onChange={(e) => setPwd(e.target.value)}
                                    value={pwd}
                                    placeholder="Enter password"
                                    required
                                />
                                <InputRightElement width='4.5rem' mr={2}>
                                    <Button h='1.75rem' color="#202023" size='sm' onClick={handleClick}>
                                        {show ? 'Hide' : 'Show'}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            <label>Repeat password:</label> {/*TODO: validation enter password again*/}
                            <InputGroup size='md'>
                                <Input
                                    type={show ? 'text' : 'password'}
                                    id="repeatPassword"
                                    autoComplete={"off"}
                                    onChange={(e) => setRepPwd(e.target.value)}
                                    value={repPwd}
                                    placeholder="Repeat password"
                                    required
                                />
                                <InputRightElement width='4.5rem' mr={2}>
                                    <Button h='1.75rem' color="#202023" size='sm' onClick={handleClick}>
                                        {show ? 'Hide' : 'Show'}
                                    </Button>
                                </InputRightElement>
                            </InputGroup>
                            <Button
                                type="submit"
                                colorScheme='yellow'
                            >
                                Sign-up
                            </Button>
                        </VStack>
                    </Center>
                </FormControl>
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