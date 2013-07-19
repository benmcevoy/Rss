
rss.notification.Notifier = function(panel) {
    var self = this;

    $(panel).hide();

    // subscriptions
    rss.messenger.subscribe(function(message) { showMessage(message); }, self, "notify");
    
    var showMessage = function(message) {
        $(panel)
            .text(message)
            .show()
            .delay(3000)
            .fadeOut('slow');
    };
};