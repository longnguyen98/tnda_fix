var path = require('path')
var webpack = require('webpack')
const VueLoaderPlugin = require('vue-loader/lib/plugin')

module.exports = {
    context: __dirname,
    entry: [
        './src/app.js',
    ],
    mode: 'development',
    output: {
        path: path.resolve(__dirname, './dist'),
        filename: "bundle.js",
        publicPath: '/'
    },
    resolve: {
        extensions: ['.js', '.vue', '.json'],
        alias: {
            'vue$': 'vue/dist/vue.js'
        }
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                        sass: 'vue-style-loader!css-loader!sass-loader?indentedSyntax' // <style lang="sass">
                    }
                }
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /node_modules/,
                options: {
                    presets: ['babel-preset-env', 'babel-preset-stage-0'],
                    plugins: ['transform-vue-jsx', 'transform-runtime', 'syntax-dynamic-import']
                }
            },
            {
                test: /\.html$/,
                use: 'file-loader?name=[name].[ext]',
            },
            {
                test: /\.scss$/,
                use: 'style-loader!css-loader!sass-loader'
            },
            {
                test: /\.css$/,
                use: 'style-loader!css-loader'
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2)(\?\S*)?$/,
                use: 'file-loader'
            },
            {
                test: /\.(png|jpe?g|gif|svg)(\?\S*)?$/,
                loader: 'file-loader',
                options: {
                    name: '[name].[ext]?[hash]'
                }
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ],
    optimization: {
        moduleIds: 'named'
    }
}