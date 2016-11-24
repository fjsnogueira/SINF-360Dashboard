$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetNumberOfFemaleEmployees",
        success: function (numberOfFemaleEmployees) {
            numberOfFemaleEmployees = JSON.parse(numberOfFemaleEmployees);
            console.log(numberOfFemaleEmployees);

            $(".numberOfFemaleEmployees").html("<p>" + numberOfFemaleEmployees[0].numeroRaparigasEmpregadas + "</p>");
        }
    }).fail(function () {
        alert("ERROR: getting number of female employees");
    });


});