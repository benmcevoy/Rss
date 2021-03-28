rss.commands.readFolderCommand = function(data) {
    rss.commands.ajaxPostAndGet(data.commandargument, '/api/folder/mark', '/home/folder/' + data.commandargument);
    rss.commands.publish(data);
};