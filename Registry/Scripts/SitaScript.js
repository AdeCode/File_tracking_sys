function UsernameCheck() {
    $('#status').html("Checking...");
    $.post("@Url.Action("CheckUsernameAvailability","User")",
{
    username: $("#userId").val()
},
function (data) {
    if (data === 0) {
        $("#status").html('<font color="Green">Username is available!. you ca use it</font>');
        $("#userId").css("border-color", "Green");
    }
    else {
        $("#status").html('<font color="Green">This usermane is taken, try another one</font>');
        $("#userId").css("border-color", "Red");
    }
}
        );
}