$(document).ready(function () {
    $('#users').DataTable();
});

$(document).ready(function () {
    $(".js-delete-user").click(function (e) {

        e.preventDefault();
        var link = $(this);
        bootbox.confirm({
            message: "Are you sure you want to remove this user?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'Cancel',
                    className: 'btn-outline-info'
                }
            },
            callback: function () {

                $.ajax({
                    type: "POST",
                    data: { userId: link.attr("data-id") },
                    url: "/admin/remove"
                })
                    .done(function (msg) {
                        link.text("Removed");
                        link.siblings(".badge").hide();

                    }).fail(function (msg) { alert(msg); });
            }
        });

    });

});
