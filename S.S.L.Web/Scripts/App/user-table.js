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
                    className: 'btn-outline-danger'
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
                        data: { userId: link.attr("data-id") },
                        url: "/admin/remove"
                    })
                        .done(function (msg) {
                            link.text("Removed");
                            link.siblings(".badge").hide();

                        }).fail(function (msg) { alert(msg); });
                }
            }
        });

    });

});
$(document).ready(function () {
    $(".js-restore-user").click(function (e) {

        e.preventDefault();
        var link = $(this);
        bootbox.confirm({
            message: "Are you sure you want to restore this user?",
            buttons: {
                confirm: {
                    label: 'Yes',
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
                        data: { userId: link.attr("data-id") },
                        url: "/admin/restore"
                    })
                        .done(function (msg) {
                            link.text("Restored");

                        }).fail(function (msg) { alert(msg); });
                }
            }
        });

    });

});

$(".js-change-role").click(function (e) {

    e.preventDefault();
    var link = $(this);

    bootbox.confirm({
        message: "Are you sure you want to perform this operation?",
        buttons: {
            confirm: {
                label: 'Yes',
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
                    data: { userId: link.attr("data-id") },
                    url: "/admin/changerole"
                })
                    .done(function (msg) {

                        link.parent('td').siblings(".js-change").html('<span class="badge badge-dark p-1">Updated</span>');
                        link.hide();

                    }).fail(function (msg) { alert(msg); });
            }
        }
    });
});


$(document).ready(function () {
    $(".js-assign-gym").click(function () {

        $("#json-result").addClass("d-none");
        var link = $(this);

        $.ajax({
            type: "POST",
            url: "/custom/user",
            data: { userId: link.attr("data-id") }

        }).done(function (user) {
            $("#username").text(user.FullName);
            $("#user-gender").text(user.Gender);
            $("#id").val(user.Id);
        });


        $("form").submit(function (e) {
            e.preventDefault();

            var id = $("#id").val();
            var group = $("#group").val();

            $.ajax({
                type: "POST",
                url: "/admin/gym/assign",
                data: { userId: id, group: group },
                success: function (group) {

                    $("#json-result").removeClass("d-none");
                    setTimeout(function () {
                        $("#end").trigger("click");
                    }, 2000);
                    link.parent("td").html('<span class="badge badge-dark p-1">' + group + '</span>');
                },
                error: function (msg) {
                    alert(msg);
                }
            });
        });
    });
});

