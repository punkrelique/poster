import React, {FC, useContext, useEffect} from 'react';
import './App.css';
import Login from "./pages/Login";
import Signup from "./pages/Signup";
import {Navigate, Route, Routes} from "react-router-dom";
import Account from "./pages/User";
import {Context} from "./index";
import {observer} from "mobx-react-lite";
import {getCookie} from "./utils/cookies";
import Feed from "./pages/Feed";
import ProtectedRoutes from "./components/ProtectedRoutes";
import NotFound from "./pages/404";

const App: FC = () => {
    const {userStore} = useContext(Context);

    useEffect(() => {
        if(getCookie("access_token")) {
            userStore.getUser();
        }
    }, []);

    return (
        <div className="App">
            <Routes>
                <Route element={<ProtectedRoutes/>}>
                    <Route path="/:username" element={<Account/>}/>
                    <Route path="/" element={<Feed/>}/>
                </Route>
                <Route path="/login" element={<Login/>}/>
                <Route path="/signup" element={<Signup/>}/>
                <Route path="/error/404" element={<NotFound/>}/>
            </Routes>
        </div>
    );
}

export default observer(App);
