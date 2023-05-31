
var ObjectAlert = new Object();

ObjectAlert.AlertView = function (type, message) {

    $("#divAlertJavaScript").html("");

    var classTypeAlert = "";

    if (type == 1) {
        classTypeAlert = "alert alert-success";
    }
    else if (type == 2) {
        classTypeAlert = "alert alert-warning";
    }
    else if (type == 3) {
        classTypeAlert = "alert alert-danger";
    }

    var divAlert = $("<div>", { class: classTypeAlert });
    divAlert.append('<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>');
    divAlert.append(' <strong>' + message + '</strong>');

    $("#divAlertJavaScript").html(divAlert);

    window.setTimeout(function () {
        $(".alert").fadeTo(1500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 5000);
}
