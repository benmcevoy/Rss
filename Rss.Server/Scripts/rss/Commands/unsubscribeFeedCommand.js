rss.commands.unsubscribeFeedCommand = function(data) {
    rss.commands.ajaxPostAndGet(data.commandargument, '/api/feed/unsubscribe');
    rss.commands.publish(data);
};