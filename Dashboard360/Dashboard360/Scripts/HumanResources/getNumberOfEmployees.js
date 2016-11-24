$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetNumberOfEmployees",
        success: function (numberOfEmployees) {
            numberOfEmployees = JSON.parse(numberOfEmployees);
            console.log(numberOfEmployees);

            $(".numberOfEmployees").html("<p>" + numberOfEmployees[0].numeroEmpregados + "</p>");
        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });


});