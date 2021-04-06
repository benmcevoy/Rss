const config = {
    api: {
        subscription: {
            get: "http://localhost:53950/v1/subscription"
        },
        folder: {
            get: "http://localhost:53950/v1/content/folder"
        },
        feed: {
            get: "http://localhost:53950/v1/content/feed"
        },
        item: {
            read: "http://localhost:53950/v1/content/read",
        },
        stream: {
            get: "http://localhost:53950/v1/content/stream"
        }
    }
};

export default config;