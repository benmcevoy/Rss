
rss.viewModels.SubscriptionsViewModel = function () {
    var self = this;

    // properties
    self.feeds = ko.observableArray([]);
    self.folders = ko.observableArray([]);

    // subscriptions
    rss.messenger.subscribe(function (id) { self.readItem(id); }, self, "readItem");
    rss.messenger.subscribe(function (id) { self.readFeed(id); }, self, "readFeed");
    rss.messenger.subscribe(function (id) { self.readFolder(id); }, self, "readFolder");
    rss.messenger.subscribe(function (id) { self.unsubscribeFeed(id); }, self, "unsubscribeFeed");
    rss.messenger.subscribe(function (id) { self.unsubscribeFolder(id); }, self, "unsubscribeFolder");

    // load
    $.getJSON('/api/folder', function (data) {
        var feeds = $.map(data.feeds, function (item) { return new rss.viewModels.ItemViewModel(item); });
        var folders = $.map(data.folders, function (item) { return new rss.viewModels.ItemViewModel(item); });

        self.feeds(feeds);
        self.folders(folders);

        $('li.folder > ul').hide();
    });

    // private methods
    var findFeed = function (feedid) {
        var feed = ko.utils.arrayFirst(self.feeds(), function (item) { return item.id === feedid; });
        var folderIndex = -1;

        if (!feed) {
            for (var i = 0; i < self.folders().length; i++) {
                feed = ko.utils.arrayFirst(self.folders()[i].items(), function (item) {
                    return item.id === feedid;
                });

                if (feed) {
                    folderIndex = i;
                    break;
                }
            }
        }

        return { feed: feed, folderIndex: folderIndex };
    };

    var findFolder = function (folderid) {
        var folder = ko.utils.arrayFirst(self.folders(), function (item) {
            return item.id === folderid;
        });

        return folder;
    };

    // public commands
    self.readItem = function (feedid) {
        var match = findFeed(feedid);

        if (match.feed) {

            match.feed.read();

            if (match.folderIndex > -1) {
                self.folders()[match.folderIndex].read();
            }
        }
    };

    self.readFeed = function (feedid) {
        var match = findFeed(feedid);

        // TODO: remove items from array
        if (match.feed) {
            var count = match.feed.itemCount();

            match.feed.itemCount(0);
            match.feed.unreadClass('read');

            if (match.folderIndex > -1) {
                var folder = self.folders()[match.folderIndex];
                var folderCount = folder.itemCount();
                var result = folderCount - count;

                if (result <= 0) {
                    result = 0;
                    folder.unreadClass('read');
                }

                folder.itemCount(result);
            }

            rss.commands.publish({
                command: 'notify',
                commandargument: String.format('{0} marked as all read', match.feed.name)
            });
        }
    };

    self.readFolder = function (folderid) {
        var folder = findFolder(folderid);
        
        for (var i = 0; i < folder.items().length; i++) {
            self.readFeed(folder.items()[i].id);
        }
        
        rss.commands.publish({
            command: 'notify',
            commandargument: String.format('{0} folder marked as all read', folder.name)
        });
    };

    self.unsubscribeFeed = function(feedId) {
        var match = findFeed(feedId);

        if (match.feed) {
            // update data
            self.feeds.remove(match.feed);
            // TODO: update UI, ko should be doing this?
            $('#' + match.feed.id).parent().remove();

            rss.commands.publish({
                command: 'notify',
                commandargument: String.format('unsubscribed from {0} feed', match.feed.name)
            });
        }
    };
    
    self.unsubscribeFolder = function (folderid) {
        var folder = findFolder(folderid);
        
        if (folder) {
            // update data
            self.folders.remove(folder);
            // TODO: update UI, ko should be doing this?
            $('#' + folder.id).parent().remove();

            rss.commands.publish({
                command: 'notify',
                commandargument: String.format('unsubscribed from {0} folder', folder.name)
            });
        }
    };

    // events
    $('body').on('click', '.expander', function () {
        $(this).next('li.folder').children('ul').toggle();
        if ($(this).text() == '-') {
            $(this).text('+');
        } else {
            $(this).text('-');
        }
    });
}