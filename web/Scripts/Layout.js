$(document).ready(function () {  

    $("#Approved").on("click", function (event) {
        event.preventDefault();  // This should come first
        OpenFormGet('/Employee/getProved', 'Approved List');
        console.log("Clicked");

    });
    $("#Rejected").on("click", function (event) {
        event.preventDefault();  // This should come first
        OpenFormGet('/Employee/getRejected', 'Rejected List');
        console.log("Clicked");

    });

    $("#AdminApprove").on("click", function (event) {
        event.preventDefault();  // This should come first
        OpenFormGet('/Admin/getAdminProved', 'Approved List');
        console.log("Clicked");

    });
    $("#AdminReject").on("click", function (event) {
        event.preventDefault();  // This should come first
        OpenFormGet('/Admin/getAdminRejected', 'Rejected List');
        console.log("Clicked");

    });
});

$(document).ajaxStart(function () {
    $('#dynamicContainer').css('filter', 'blur (8px)');
    $('.loader').show();
})
    .ajaxComplete(function () {
        $('.loader').hide();
        $('#dynamicContainer').css('filter', 'blur (0px)');

    });

function OpenForm(Action, Title) {
    $.ajax({
        url: Action,
        dataType: 'html',
        traditional: true,
        type: 'POST',
        success: function (content) {
            $("#dynamicContainer").html(content);
            $("#h1Title").html(Title);
            $("#liTitle").html(Title);

        }
    });
}
function OpenFormGet(Action, Title) {
    $.ajax({
        url: Action,
        dataType: 'html',
        traditional: true,
        type: 'GET',
        success: function (content) {
            $("#dynamicContainer").html(content);
            $("#h1Title").html(Title);
            $("#liTitle").html(Title);

        }
    });
}
