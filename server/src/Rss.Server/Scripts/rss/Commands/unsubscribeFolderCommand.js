rss.commands.unsubscribeFolderCommand = function (data) {
    var args = eval(String.format('({0})', data.commandargument));

    rss.commands.ajaxPostAndGet(args.folderId, '/api/folder/unsubscribe', '/home/');
    rss.commands.publish({ command: data.command, commandargument: args.folderId });
};