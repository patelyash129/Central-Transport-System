$(document).ready(function () {  
    $("#emp").on("click", function (event) {
        OpenForm('/Employee/CarRequest', 'Truck Request');
    });
    $("#Reject").on("click", function (event) {
        OpenForm('/Account/AdminReject', 'Reject');
    });
    $("#dash").on("click", function (event) {
        OpenForm('/Employee/Dashboard', 'Dashboard');
    });
    $("#Admindash").on("click", function (event) {
        OpenForm('/Admin/DashBoard', 'DashBoard');
    });
    $("#AdminHistory").on("click", function (event) {
        OpenFormGet('/Admin/History', 'History');
    });
    $("#AddEmp").on("click", function (event) {
        OpenForm('/Admin/AddEmployee', 'Add Employee');
    });
    $("#AddDep").on("click", function (event) {
        OpenForm('/Admin/AddDepartment', 'Add Department');
    });
    $("#AddVeh").on("click", function (event) {
        OpenForm('/Admin/AddVehicle', 'Add Vehicle');
    });
    $("#Profile").on("click", function (event) {
        OpenFormGet('/Employee/profile', 'Profile');
    });
    $("#Truck").on("click", function (event) {
        OpenFormGet('/Admin/TruckAllRequest', 'Truck Request');
    });
    $("#CarRequest").on("click", function (event) {
        OpenForm('/Employee/CarRequest', 'Car Request');
    });
    $("#ShowRequest").on("click", function (event) {
        OpenFormGet('/Employee/getinfo', 'Request');
    });
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

    $("#Login").on("click", function (event) {
        event.preventDefault();  // This should come first

        OpenForm('/Account/Login', 'Login');
        console.log("Clicked");
    });
 

    $("#Allrequest").on("click", function (event) {
        event.preventDefault();  // This should come first
        OpenFormGet('/Admin/AllRequest', 'Car Request');
        console.log("Clicked");
    });
    $("#TruckReq").on("click", function (event) {
        OpenForm('/Employee/Truck','Truck Request');
    });
    
    $("#Signup").on("click", function (event) {
        OpenForm('/Account/Signup','Signup');
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

