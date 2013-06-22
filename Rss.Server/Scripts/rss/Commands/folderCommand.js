rss.commands.folderCommand = function(data) {
    rss.commands.ajaxGet('/home/folder/' + data.commandargument);
    rss.commands.publish(data);
};