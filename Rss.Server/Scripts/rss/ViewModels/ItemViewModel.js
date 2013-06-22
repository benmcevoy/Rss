
rss.viewModels.ItemViewModel = function (data) {
    var self = this;

    // properties    
    self.id = data.id;
    self.name = data.name;
    self.unreadClass = ko.observable(data.unreadClass);
    self.itemCount = ko.observable(data.itemCount);
    self.items = ko.observableArray([]);

    if (data.feeds) {
        var feeds = $.map(data.feeds, function (item) { return new rss.viewModels.ItemViewModel(item); });
        self.items(feeds);
    }
    
    // commands
    self.read = function() {
        var count = self.itemCount();

        count--;

        if (count <= 0) {
            count = 0;
            self.unreadClass('read');
        }

        self.itemCount(count);
    };
}