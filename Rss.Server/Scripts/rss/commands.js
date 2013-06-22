rss.messenger = new ko.subscribable();

rss.commands.invoke = function (data) {
    if (!data.command) return;

    var commandToInvoke = rss.commands[data.command + 'Command'];

    if (commandToInvoke) {
        commandToInvoke(data);
    }
};

rss.commands.bind = function () {
    $('body').on('click', 'input.ajax', function () {
        rss.commands.invoke($(this).data());
    });

    $('body').on('click', 'a.ajax', function (e) {
        e.preventDefault();
        rss.commands.invoke($(this).data());
    });
};

rss.commands.publish = function (data) {
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
            $('#panel').html(response);
        });
    }
};

