const path = require('path')
const { CleanWebpackPlugin } = require('clean-webpack-plugin')
const nodeExternals = require('webpack-node-externals');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const WebpackBundleAnalyzer = require('webpack-bundle-analyzer');
const CompressionPlugin = require("compression-webpack-plugin");


module.exports = {
    entry: path.join(__dirname, 'assets/src/components/index.js'),
    devtool: false,
    plugins: [
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin(),
        new CompressionPlugin(),
        //new WebpackBundleAnalyzer.BundleAnalyzerPlugin(),
    ],
    module: {
        rules: [
            {
                test: /\.(js|jsx|ts|tsx)$/,
                exclude: /node_modules/,
                use: [{
                    loader: 'babel-loader',
                    options:
                    {
                        presets: ['@babel/preset-env', '@babel/react']
                    }
                }]
            },
            {
                test: /\.(css|scss)$/,
                use: [
                    "style-loader",
                    "css-loader"
                ]
            }
        ]
    },
    output: {
        filename: 'ecommerce.js',
        path: path.resolve(__dirname, 'wwwroot/assets/dist/'),
        libraryTarget: 'commonjs',
        clean: true,
    },
    performance: {
        hints: false,
        maxEntrypointSize: 512000,
        maxAssetSize: 512000
    }
}