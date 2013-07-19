rss.commands.refreshFeedCommand = function (data) {
    rss.commands.ajaxPostAndGet(data.commandargument, '/api/feed/Refresh', '/home/feed/' + data.commandargument);
    rss.commands.publish(data);

    rss.commands.publish({
        command: 'notify',
        commandargument: 'feed is being refreshed'
    });
};