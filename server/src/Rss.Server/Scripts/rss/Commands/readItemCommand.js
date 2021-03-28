rss.commands.readItemCommand = function (data) {
    var args = eval(String.format('({0})', data.commandargument));

    rss.commands.ajaxGet('/home/item/' + args.itemId);
    rss.commands.publish({ command: data.command, commandargument: args.feedId });
};