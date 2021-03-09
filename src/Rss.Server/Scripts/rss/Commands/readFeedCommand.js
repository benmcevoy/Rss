rss.commands.readFeedCommand = function(data) {
    rss.commands.ajaxPostAndGet(data.commandargument, '/api/feed/mark', '/home/feed/' + data.commandargument);
    rss.commands.publish(data);
};