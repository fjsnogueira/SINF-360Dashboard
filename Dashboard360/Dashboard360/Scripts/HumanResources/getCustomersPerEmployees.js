$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetCustomersPerEmployees/2016",
        success: function (customersPerEmployees) {

            customersPerEmployees = JSON.parse(customersPerEmployees);
            console.log(customersPerEmployees);

            $(".loadingCustomersEmployee").hide();
            $(".costumersPerEmployee").append(customersPerEmployees.toFixed(2));

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});