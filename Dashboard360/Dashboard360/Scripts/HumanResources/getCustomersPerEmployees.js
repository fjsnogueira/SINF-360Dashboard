$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetCustomersPerEmployees/2016",
        success: function (customersPerEmployees) {

            customersPerEmployees = JSON.parse(customersPerEmployees);
            console.log(customersPerEmployees);

            $(".loadingSalesTotal").hide();

            $(".loadingNumCustomers").remove();
            $(".costumersPerEmployee").append(customersPerEmployees);

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});