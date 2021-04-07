const config = {
    api: {
        subscription: {
            get: "http://api.rss.local/v1/subscription"
        },
        folder: {
            get: "http://api.rss.local/v1/content/folder"
        },
        feed: {
            get: "http://api.rss.local/v1/content/feed"
        },
        item: {
            read: "http://api.rss.local/v1/content/read",
        },
        stream: {
            get: "http://api.rss.local/v1/content/stream"
        }
    }
};

export default config;