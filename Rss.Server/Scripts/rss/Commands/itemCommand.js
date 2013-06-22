rss.commands.itemCommand = function(data) {
    rss.commands.ajaxGet('/home/item/' + data.commandargument);
    rss.commands.publish(data);
};