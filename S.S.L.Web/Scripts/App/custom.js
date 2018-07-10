function postTodoItem() {

    if ($('form').valid()) {
        var data = $('input').val();
        $.ajax({
            type: "POST",
            url: "/custom/todo/create",
            data: { todoItem: data }
        })
            .done(function (todo) {
                $('#todoForm')[0].reset();
                $("#todoSection").prepend('<div class="shadow-sm p-2 mb-3"><p style="display: inline;"><i class="fa fa-square-o  text-danger mr-3"></i>' + todo.Item + '</p><a class="badge badge-light shadow-sm js-update-todo" href="#" data-todo-id="' + todo.TodoId + '">Mark as done</a></div>');
                $('#noTodo').hide(1000);
            });
    }

    return false;
}

$(document).ready(function () {

    $('#todoSection').on("click", ".js-update-todo", function (e) {
        e.preventDefault();
        var link = $(e.target);
        $.ajax({
            type: "POST",
            url: "/custom/todo/update",
            data: { todoId: link.attr("data-todo-id") }
        })
            .done(function (todo) {
                link.parent("div").fadeOut(function () {
                    $(this).remove();
                    $("#todoSection").prepend('<div class="shadow-sm p-2 mb-3"><p style="display: inline;"><i class="fa fa-check-square-o text-success mr-3"></i>' + todo.Item + '</p><span class="badge badge-success shadow-sm ml-2">done</span></div>');
                });
            }).fail(function () {
                alert("failed");
            });
    });
});


