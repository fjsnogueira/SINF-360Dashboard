$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetRevenuePerEmployee/2016",
        success: function (revenuePerEmployee) {

            revenuePerEmployee = JSON.parse(revenuePerEmployee);
            console.log(revenuePerEmployee);


            $(".loadingSalesTotal").hide();
            $(".loadingNumCustomers").remove();
            $(".revenuePerEmployee").append(revenuePerEmployee);

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});