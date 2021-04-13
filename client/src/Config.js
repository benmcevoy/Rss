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
            read: "http://api.rss.local/v1/content/read"
        },
        stream: {
            get: "http://api.rss.local/v1/content/stream"
        },
        refresh: "http://api.rss.local/v1/content/refresh",
        markAsRead: "http://api.rss.local/v1/content/markasread",
        subscribe: "http://api.rss.local/v1/subscription/subscribe",
        unsubscribe: "http://api.rss.local/v1/subscription/unsubscribe",
    }
};

export default config;