rss.commands.readItemCommand = function (data) {
    var args = eval('(' + data.commandargument + ')');
    rss.commands.ajaxGet('/home/item/' + args.itemId);
    rss.commands.publish({ command: data.command, commandargument: args.feedId });
};