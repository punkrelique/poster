import React, {createContext} from 'react';
import './index.css';
import ReactDOM from 'react-dom/client';
import App from './App';
import {BrowserRouter} from "react-router-dom";
import UserStore from "./stores/UserStore";
import IStore from "./types/IStore";
import {ChakraProvider} from '@chakra-ui/react'

const userStore = new UserStore();
export const Context = createContext<IStore>({
    userStore,
});

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <React.StrictMode>
        <Context.Provider value={{userStore}}>
            <BrowserRouter>
                <ChakraProvider>
                    <App/>
                </ChakraProvider>
            </BrowserRouter>
        </Context.Provider>
    </React.StrictMode>
);