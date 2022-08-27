import React, {useContext} from 'react';
import { Outlet } from 'react-router'
import {Context} from "../index";
import {observer} from "mobx-react-lite";
import Login from "../pages/Login";

const ProtectedRoutes = () => {
    const {userStore} = useContext(Context);
    return (
        userStore.isAuthenticated ? <Outlet/> : <Login/>
    );
};

export default observer(ProtectedRoutes);