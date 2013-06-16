﻿rss.messenger = new ko.subscribable();

rss.communication = (function ($) {
    var bindSimplePost = function () {
        $('body').on('click', 'input.ajax', function () {
            var data = $(this).data();

            ajaxPostAndGet(data.id, data.postaction, data.getaction, data.getpanel);

            publish(data);
        });
    };

    var bindAjaxLinks = function () {
        $('body').on('click', 'a.ajax', function (e) {
            e.preventDefault();

            var $this = $(this);
            var data = $this.data();
            var action = $this.attr('href');
            var panel = $this.attr('rel');

            ajaxGet(action, panel);

            publish(data);
        });
    };

    var publish = function(data) {
        if (data.command) {
            rss.messenger.notifySubscribers(data.commandargument, data.command);
        }
    };

    var ajaxPostAndGet = function (id, postaction, getaction, getpanel) {
        $.ajax({
            type: "POST",
            url: postaction,
            data: { '': id }
        }).done(function () {
            ajaxGet(getaction, getpanel);
        });
    };

    var ajaxGet = function (action, panel) {
        if (action) {
            $.get(action, {}, function (response) {
                if (panel) {
                    $(panel).hide().html(response).fadeIn();
                } else {
                    // TODO: use options to set defaults
                    $('#panel').html(response);
                }
            });
        }
    };

    // initialize
    $(function () {
        bindSimplePost();
        bindAjaxLinks();
    });

    // expose public stuff here
    return {
        publish : publish
    };
}(jQuery));