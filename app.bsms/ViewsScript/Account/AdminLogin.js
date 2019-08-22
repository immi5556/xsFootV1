$(document).ready(function () {
    $("#adminLogin").show();
    $("#loginContainer").show();
    $("#adminForgotPin").hide();
    $("#forgotContainer").hide();

    $("#forgotAdminForm").hide();
    $("#loginadmin").show();
});
$('#btnForgotPanel').click(function (e) {
    e.preventDefault();

    $("#adminLogin").hide();
    $("#loginContainer").hide();
    $("#adminForgotPin").show();
    $("#forgotContainer").show();

    $("#forgotAdminForm").show();
    $("#loginadmin").hide();
});
$('#btnLoginPanel').click(function (e) {
    e.preventDefault();

    $("#adminLogin").show();
    $("#loginContainer").show();
    $("#adminForgotPin").hide();
    $("#forgotContainer").hide();

    $("#forgotAdminForm").hide();
    $("#loginadmin").show();
});
$('#btnLogin').click(function (e) {
    e.preventDefault();

    $.ajax({
        url: '/Home/AdminLogins',
        type: 'POST',
        cache: false,
        data: $("#loginadmin").serialize(),
        success: function (data) {
            if (data.Result == "OK") {
                window.location.href = "/Dashboard/Admin";
            }
            else if (data.Result == "INVALID") {
                $("#errormsg").html(data.Message);
            }
            else {
                $("#errormsg").html(data.Message);
            }

        },
        error: function (errorThrown) {
            $("#errormsg").html("<span style='color:red'>'" + data + "'</span>");
        }
    });

});
$('#btnForgot').click(function (e) {
    e.preventDefault();

    if ($('#adminIcNo').val() == "" || $('#adminEmail').val() == "") {
        toastr["warning"]("Invalid IC No/Email Address");
        return false;
    }

    $.ajax({
        url: '/Home/ResetAdminPIN',
        type: 'POST',
        cache: false,
        data: $("#forgotAdminForm").serialize(),
        success: function (data) {
            if (data.Status == "OK") {
                toastr["success"](data.Message);
                $('#adminIcNo').val('');
                $('#adminEmail').val('');

                $("#adminLogin").show();
                $("#loginContainer").show();
                $("#adminForgotPin").hide();
                $("#forgotContainer").hide();

                $("#forgotAdminForm").hide();
                $("#loginadmin").show();
            }
            else if (data.Status == "INVALID") {
                toastr["warning"](data.Message);
            }
            else {
                toastr["error"](data.Message);
            }

        },
        error: function (errorThrown) {
        }
    });

});