var rss = rss || {};

rss.postModel = (function ($) {
    // bind simple post actions
    var bindSimple = function () {
        $('.post-mode-simple').filter(':button').bind('click', function () {
            var id = $(this).data('id');
            var action = $(this).data('action');
            var redirectAction = $(this).data('redirectaction');

            console.log(id, action, redirectAction);

            $.ajax({
                type: "POST",
                url: action,
                data: { '': id }
            }).done(function () {
                console.log(redirectAction);

                if (redirectAction) {
                    window.location.replace(redirectAction);
                }
            });
        });
    };

    return {
        bindSimple: bindSimple

    };
}(jQuery));



$(function () {
    rss.postModel.bindSimple();
});


