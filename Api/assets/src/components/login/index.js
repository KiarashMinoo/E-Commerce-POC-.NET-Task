import React from 'react';
import { Form, FormField, TextBox, LinkButton, PasswordBox } from 'rc-easyui';
import axios from 'axios';
import * as configs from '../configs';

import './style.scss';

export class Login extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            model: {
                userName: '',
                password: ''
            }
        }
    }

    handleChange(field, value) {
        let model = Object.assign({}, this.state.model);
        model[field] = value;
        this.setState({ model: model });
    }

    handleSubmit() {
        const loginEndpoint = `${configs.Credential}login`;
        axios.
            post(loginEndpoint, this.state.model).
            then(response => {
                localStorage.setItem('UserId', response.data.id);
                localStorage.setItem('UserName', response.data.userName);
                localStorage.setItem('Token', response.data.token);
            });
    }

    render() {
        return (
            <div>
                <h2>Login</h2>
                <Form
                    style={{ maxWidth: 500 }}
                    model={this.state.model}
                    labelWidth={120}
                    labelAlign="right"
                    onChange={this.handleChange.bind(this)}
                >
                    <FormField name="userName" label="User:">
                        <TextBox></TextBox>
                    </FormField>
                    <FormField name="password" label="Password:">
                        <PasswordBox></PasswordBox>
                    </FormField>
                    <FormField>
                        <LinkButton onClick={this.handleSubmit.bind(this)}>Login</LinkButton>
                    </FormField>
                </Form>
            </div>
        );
    }
}
