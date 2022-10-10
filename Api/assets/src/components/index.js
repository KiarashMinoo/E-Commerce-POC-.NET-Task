import React from 'react';
import { render } from 'react-dom';
import axios from 'axios';

import { ECommerceContainer } from './container/index.js';

import './style.scss';
import 'rc-easyui/dist/themes/default/easyui.css';
import 'rc-easyui/dist/themes/icon.css';
import 'rc-easyui/dist/themes/color.css';
import 'rc-easyui/dist/themes/react.css';

axios.interceptors.request.use(
    (request) => {
        const token = localStorage.getItem('Token');
        if (token)
            request.headers.Authorization = `Bearer ${token}`;

        request.headers['accept'] = 'application/json';
        request.headers['Content-Type'] = 'application/json';

        return request;
    },
    (error) => {
        return Promise.reject(error);
    }
);

axios.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response.status === 403 || error.response.status === 401) {
            localStorage.removeItem('UserId');
            localStorage.removeItem('UserName');
            localStorage.removeItem('Token');
        } else {
            alert(error.response.data);
        }
    }
);

const App = () => (
    <React.StrictMode>
        <ECommerceContainer />
    </React.StrictMode>
);

var container = document.getElementById('react-container');
render(<App />, container);