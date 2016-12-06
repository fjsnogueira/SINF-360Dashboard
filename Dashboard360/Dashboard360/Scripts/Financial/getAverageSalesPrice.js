var getTotalSales = function () {
    console.log("getAverageSalesPrice");

    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales",
        success: function (sales) {
            sales = JSON.parse(sales);
            var totalSales = 0;
            for (var i = 0; i < sales.length; i++) {
                totalSales += (sales[i].TotalIva + sales[i].TotalMerc);
            }
            $(".averageSalesPrice").append("<p> " + (totalSales / sales.length).toFixed(2) + "€ </p>");
        }
    }).fail(function () {
        alert("ERROR: getting sales list");
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

getTotalSales();