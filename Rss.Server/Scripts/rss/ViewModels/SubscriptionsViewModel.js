
rss.viewModels.SubscriptionsViewModel = function () {
    var self = this;

    // data
    self.items = ko.observableArray([
        new rss.models.Item(1, 'dev', 123),
        new rss.models.Item(2, 'test', 12),
        new rss.models.Item(3, 'again', 6)
    ]);

    // commands
    self.incrementCount = function () {
        console.log(self.items()[0]);
        self.items()[0].count(456);
    };


}