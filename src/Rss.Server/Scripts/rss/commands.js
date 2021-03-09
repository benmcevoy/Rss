rss.messenger = new ko.subscribable();

rss.commands.targetPanel = '#panel';

rss.commands.invoke = function (data) {
    if (!data.command) return;

    var commandToInvoke = rss.commands[data.command + 'Command'];

    if (commandToInvoke) {
        commandToInvoke(data);
    }
};

rss.commands.bind = function () {
    $('body').on('click', 'input.command', function () {
        rss.commands.invoke($(this).data());
    });

    $('body').on('click', 'a.command', function (e) {
        e.preventDefault();
        rss.commands.invoke($(this).data());
    });
};

rss.commands.publish = function (data) {
    console.log(data);
    
    if (data.command) {
        rss.messenger.notifySubscribers(data.commandargument, data.command);
    }
};

rss.commands.ajaxPostAndGet = function (id, postaction, getaction) {
    $.ajax({
        type: "POST",
        url: postaction,
        data: { '': id }
    }).done(function () {
        rss.commands.ajaxGet(getaction);
    });
};

rss.commands.ajaxGet = function (action) {
    if (action) {
        $.get(action, {}, function (response) {
            $(rss.commands.targetPanel).html(response);
        });
    }
};

