$(document).ready(function () {

    $(".js-update-mentee").click(function (e) {

        e.preventDefault();
        var link = $(this);

        bootbox.confirm({
            message: "Confirm to mark this mentee's classes as done",
            buttons: {
                confirm: {
                    label: 'Confirm Done',
                    className: 'btn-outline-gold'
                },
                cancel: {
                    label: 'Cancel',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        data: { menteeId: link.attr("data-id") },
                        url: "/facilitator/mentee/update"
                    })
                        .done(function (msg) {
                            link.text("Done");

                        }).fail(function (msg) { alert(msg); });
                }
            }
        });
    });

});
