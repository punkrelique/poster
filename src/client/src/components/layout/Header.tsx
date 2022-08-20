import React from 'react';
import {Input} from "@chakra-ui/react";

const Header = () => {
    return (
        <div>
            <Input
                htmlSize={33}
                placeholder='Search for users!'
                _placeholder={{ opacity: 2, color: 'yellow.200' }}
                focusBorderColor='yellow.500'
            />
        </div>
    );
};

export default Header;