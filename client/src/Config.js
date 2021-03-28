const config = {
    api: {
        folder: {
            getRoot: "http://api.rss.local/api/Folder/Get",
            get: "http://api.rss.local/api/v2_folder/Get"
        },
        feed: {
            getAll: "http://api.rss.local/api/feed/All",
            get: "http://api.rss.local/api/v2_feed/Get",
        },
        item: {
            read:  "http://api.rss.local/api/item/read",
        },
        stream: {
            get: 'http://api.rss.local/api/v2_Stream/get'
        }
    }
};

export default config;