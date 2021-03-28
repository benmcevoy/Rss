rss.commands.unsubscribeFeedCommand = function(data) {
    var args = eval(String.format('({0})', data.commandargument));

    rss.commands.ajaxPostAndGet(args.feedId, '/api/feed/unsubscribe', '/home/folder/' + args.folderId);
    rss.commands.publish({ command: data.command, commandargument: args.feedId });
};