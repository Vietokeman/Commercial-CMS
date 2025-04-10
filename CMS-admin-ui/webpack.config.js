const path = require('path');

module.exports = {
    entry: './src/main.ts', // Điểm vào của ứng dụng
    output: {
        filename: 'bundle.js', // Tên tệp đầu ra
        path: path.resolve(__dirname, 'dist'), // Thư mục đầu ra
    },
    resolve: {
        extensions: ['.ts', '.js'], // Các phần mở rộng mà Webpack sẽ xử lý
        alias: {
            '@api': path.resolve(__dirname, 'src/app/api/'),
            '@auth': path.resolve(__dirname, 'src/app/views/auth/'),
            '@containers': path.resolve(__dirname, 'src/app/containers/'),
            '@shared': path.resolve(__dirname, 'src/app/shared/'),
            '@views': path.resolve(__dirname, 'src/app/views/'),
            '@components': path.resolve(__dirname, 'src/app/components/'),
            '@environments': path.resolve(__dirname, 'src/app/environments/'),
            '@assets': path.resolve(__dirname, 'src/app/assets/'),
            '@scss': path.resolve(__dirname, 'src/app/scss/')
        }
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: 'ts-loader', // Sử dụng ts-loader để biên dịch TypeScript
                exclude: /node_modules/
            }
        ]
    },
    mode: 'development' // Chế độ phát triển
};
