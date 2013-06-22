$(function () {
    ko.applyBindings(new rss.viewModels.SubscriptionsViewModel());
});

rss.commands.bindCommands();
