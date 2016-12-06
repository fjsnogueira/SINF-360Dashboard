
var totalClients;


var getTotalClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/clients",
        success: function (clients) {
            clients = JSON.parse(clients);
            totalClients = clients.length;
            //clients
            $(".numberOfCustomers").html(totalClients + " total active clients");
        }
    }).fail(function () {
        alert("ERROR: getting client list");
    });
};

var getTotalSales = function () {
    console.log("getTotalSales");

    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");
    
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales",
        success: function (sales) {
            sales = JSON.parse(sales);
            console.log(sales);
            var totalSales = 0;
            // Total in sales this year
            for (var i = 0; i < sales.length; i++) {
                totalSales += (sales[i].TotalIva + sales[i].TotalMerc);
            }
            console.log("totalSales: " + totalSales);
            $(".avgSalePerCustomer").append("<p> " + (totalSales / totalClients).toFixed(2) + "€ </p>");

        }
    }).fail(function () {
        alert("ERROR: getting sales list");
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};


getTotalClients().then(getTotalSales);