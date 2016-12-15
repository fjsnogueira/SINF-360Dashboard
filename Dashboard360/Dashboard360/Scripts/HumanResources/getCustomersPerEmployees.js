var year = moment().year();

$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetCustomersPerEmployees/" + +parseInt($('#yearTitle').text()),
        success: function (customersPerEmployees) {

            customersPerEmployees = JSON.parse(customersPerEmployees);
            console.log(customersPerEmployees);

            $(".loadingCustomersEmployee").hide();
            $(".costumersPerEmployee").append(customersPerEmployees);

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}
