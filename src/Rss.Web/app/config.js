const config = {
    api: {
        folder: {
            getRoot: "http://api.rss.local/api/Folder/Get",
            get: "http://api.rss.local/api/Folder/Get"
        },
        feed: {
            getAll: "http://api.rss.local/api/feed/All",
            get: "http://api.rss.local/api/feed/Get",
        },
        item: {
            read:  "http://api.rss.local/api/item/read",
        }
    }
};

export default config;