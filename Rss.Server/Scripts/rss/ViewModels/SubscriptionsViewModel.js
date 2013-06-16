
rss.viewModels.SubscriptionsViewModel = function () {
    var self = this;

    // properties
    self.feeds = ko.observableArray([]);
    self.folders = ko.observableArray([]);

    // load
    $.getJSON('/api/folder', function (data) {
        var feeds = $.map(data.feeds, function (item) { return new rss.models.Item(item); });
        var folders = $.map(data.folders, function (item) { return new rss.models.Item(item); });

        self.feeds(feeds);
        self.folders(folders);
        
        $('li.folder > ul').hide();
    });

    // public commands
    self.readItem = function (feedid) {
        var match = ko.utils.arrayFirst(self.feeds(), function (item) { return item.id === feedid; });
        var folderIndex = -1;

        if (!match) {
            for (var i = 0; i < self.folders().length; i++) {
                match = ko.utils.arrayFirst(self.folders()[i].items(), function (item) {
                    return item.id === feedid;
                });

                if (match) {
                    folderIndex = i;
                    break;
                }
            }
        }

        if (match) {
            match.read();
            if (folderIndex > -1) {
                self.folders()[folderIndex].read();
            }
        }
    };

    // events
    $('body').on('click', '.expander', function () {
        $(this).next('li.folder').children('ul').toggle();
        if ($(this).text() == '+') {
            $(this).text('-');
        } else {
            $(this).text('+');
        }
    });

    $('body').on('click', '.item a', function () {
        var data = $(this).data();
        self.readItem(data.feedid);
    });
}