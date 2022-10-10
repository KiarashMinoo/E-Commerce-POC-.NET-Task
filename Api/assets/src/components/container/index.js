import React from 'react';
import { Buy } from '../buy/index.js';
import { Login } from '../login/index.js';

import './style.scss';

export class ECommerceContainer extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <a href="/swagger">Swagger</a>
                <div>
                    {
                        localStorage.getItem("Token")
                            ?
                            <Buy></Buy>
                            :
                            <Login onLoggedIn={() => this.forceUpdate()}></Login>
                    }
                </div>
            </div>
        );
    }
}
