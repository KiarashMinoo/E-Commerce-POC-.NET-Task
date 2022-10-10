import React from 'react';
import { ComboGrid, GridColumn, Form, FormField, TextBox, LinkButton, NumberBox } from 'rc-easyui';
import axios from 'axios';
import * as configs from '../configs';

import './style.scss';

export class Buy extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            model: {
                productId: '',
                quantity: ''
            },
            products: [{
                productId: '',
                thumbnail: '',
                name: '',
                availableQuantity: 0,
                price: 0
            }]
        }
    }

    componentDidMount() {
        this.bindProducts();
    }

    bindProducts() {
        axios.
            get(configs.Product).
            then(response => {
                this.setState({
                    products: [{
                        productId: '',
                        thumbnail: '',
                        name: '',
                        availableQuantity: 0,
                        price: 0
                    }]
                });

                let products = []

                response.data.forEach(product => {
                    products.push({
                        productId: product.id,
                        thumbnail: product.image,
                        name: product.name,
                        availableQuantity: product.quantity,
                        price: product.price
                    });
                });

                this.setState({ products: products });
            });
    }

    handleChange(field, value) {
        let model = Object.assign({}, this.state.model);
        model[field] = value;
        this.setState({ model: model });
    }

    handleSubmit() {
        axios.
            post(configs.Receipt, this.state.model).
            then(() => {
                this.bindProducts();
            });
    }

    render() {
        return (
            <div>
                <h2>Buy</h2>
                <Form
                    style={{ maxWidth: 500 }}
                    model={this.state.model}
                    labelWidth={120}
                    labelAlign="right"
                    onChange={this.handleChange.bind(this)}
                >
                    <FormField name="productId" label="Product:">
                        <ComboGrid
                            valueField="productId"
                            textField="name"
                            data={this.state.products}
                        >
                            <GridColumn field="thumbnail" title="Image" align="center" width={96}
                                render={({ row }) => (
                                    <div>
                                        <img src={row.thumbnail} alt={row.name} width="96"></img>
                                    </div>
                                )}
                            />
                            <GridColumn field="name" title="Name"></GridColumn>
                            <GridColumn field="availableQuantity" title="Quantity"></GridColumn>
                            <GridColumn field="price" title="Price"></GridColumn>
                        </ComboGrid>
                    </FormField>
                    <FormField name="quantity" label="Quantity:">
                        <NumberBox></NumberBox>
                    </FormField>
                    <FormField>
                        <LinkButton onClick={this.handleSubmit.bind(this)}>Buy</LinkButton>
                    </FormField>
                </Form>
            </div>
        );
    }
}
