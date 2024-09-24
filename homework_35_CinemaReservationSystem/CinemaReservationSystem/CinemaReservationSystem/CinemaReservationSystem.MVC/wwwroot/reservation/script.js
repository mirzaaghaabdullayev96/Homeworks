function onLoaderFunc() {
    $(".seatStructure *").prop("disabled", true);
    $(".displayerBoxes *").prop("disabled", true);
    $(".booked-already").prop("disabled", true);

}
function takeData() {
    if (($("#Numseats").val().length == 0)) {
        alert("Please Enter Number of Seats");
    }
    else {
        $(".inputForm *").prop("disabled", true);
        $(".seatStructure *").prop("disabled", false);
        $(".booked-already").prop("disabled", true);
        document.getElementById("notification").innerHTML = "<b style='margin-bottom:0px;background:yellow;'>Please Select your Seats</b>";
    }
}


function updateTextArea() {

    if ($("input:checked").length == ($("#Numseats").val())) {
        $(".seatStructure *").prop("disabled", true);

        var allNameVals = [];
        var allNumberVals = [];
        var allSeatsVals = [];


        allNameVals.push($("#Username").val());
        allNumberVals.push($("#Numseats").val());
        $('#seatsBlock :checked').each(function () {
            allSeatsVals.push($(this).val());
        });


        setCookie("SelectedSeats", allSeatsVals.join(", "), 7);

        window.location.href = 'http://localhost:7228/ticketreservation/reservation/reserve';

    }
    else {
        alert("Please select " + ($("#Numseats").val()) + " seats")
    }
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + encodeURIComponent(value || "") + expires + "; path=/";
}


function myFunction() {
    alert($("input:checked").length);
}

/*
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
*/


$(":checkbox").click(function () {
    if ($("input:checked").length == ($("#Numseats").val())) {
        $(":checkbox").prop('disabled', true);
        $(':checked').prop('disabled', false);
    }
    else {
        $(":checkbox").prop('disabled', false);
    }
});


