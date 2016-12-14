$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetRevenuePerEmployee/2016",
        success: function (revenuePerEmployee) {

            revenuePerEmployee = JSON.parse(revenuePerEmployee);
            console.log(revenuePerEmployee);

            $(".loadingRevEmployee").hide();
            $(".revenuePerEmployee").append(formatPrice(Math.floor(revenuePerEmployee).toString()) + " <span style='font-size: 20px!important;'>" + revenuePerEmployee.toString().split(".")[1].slice(0, 2) + "</span>");

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});


String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}
