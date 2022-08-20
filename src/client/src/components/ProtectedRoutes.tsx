import React, {useContext} from 'react';
import { Outlet } from 'react-router'
import {Context} from "../index";
import {Navigate} from "react-router-dom";

const ProtectedRoutes = () => {
    const {userStore} = useContext(Context);
    return (
        userStore.isAuthenticated ? <Outlet/> : <Navigate to="/login"/>
    );
};

export default ProtectedRoutes;