rss.commands.feedCommand = function(data) {
    rss.commands.ajaxGet('/home/feed/' + data.commandargument);
    rss.commands.publish(data);
};