$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetAverageEmployeeByAge",
        success: function (averageEmployeeByAge) {
            averageEmployeeByAge = JSON.parse(averageEmployeeByAge);
            console.log(averageEmployeeByAge);

            $(".averageEmployeeAge").html("<p>" + averageEmployeeByAge + "</p>");
        }
    }).fail(function () {
        alert("ERROR: getting average of employee by age");
    });


});