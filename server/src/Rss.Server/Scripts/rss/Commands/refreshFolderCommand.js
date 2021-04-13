rss.commands.refreshFolderCommand = function (data) {
    rss.commands.ajaxPostAndGet(data.commandargument, '/api/folder/Refresh', '/home/folder/' + data.commandargument);
    rss.commands.publish(data);
    
    rss.commands.publish({
        command: 'notify',
        commandargument: 'folder is being refreshed'
    });
};