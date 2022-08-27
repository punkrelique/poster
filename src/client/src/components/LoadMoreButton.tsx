import React, {Dispatch, FC, useState} from 'react';
import {IMessage} from "../types/IMessage";
import {Button} from "@chakra-ui/react";

interface ILoadMoreButtonProps {
    children?: React.ReactNode,
    offset: number,
    setOffset: Dispatch<React.SetStateAction<number>>,
    limit: number,
    show: boolean,
}

const LoadMoreButton: FC<ILoadMoreButtonProps>
    = ({
           children,
           offset,
           setOffset,
           limit,
           show,
       }) => {

    const handleLoadMore = () => {
        setOffset(limit + offset);
    }

    return (
        <Button
            bg="yellow.100"
            color="#202023"
            style={{display: show ? "block" : "none"}}
            onClick={handleLoadMore}
        >
            {children}
        </Button>
    );
};

export default LoadMoreButton;