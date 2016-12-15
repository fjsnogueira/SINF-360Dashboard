var year = moment().year();

$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetCustomersPerEmployees/" + +parseInt($('#yearTitle').text()),
        success: function (customersPerEmployees) {

            customersPerEmployees = JSON.parse(customersPerEmployees);
            console.log(customersPerEmployees);

            $(".loadingCustomersEmployee").hide();
            $(".costumersPerEmployee").append(formatPrice(Math.floor(customersPerEmployees).toString()) + " <span style='font-size: 20px!important;'>" + customersPerEmployees.toString().split(".")[1].slice(0, 2) + "</span>");

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}
