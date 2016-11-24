var totalClients;
var getTotalClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetNumClients",
        success: function (numClients) {
            $(".numberOfCustomers").html(numClients + " total");
        }
    }).fail(function () {
        console.log("ERROR: getting NumClients");
    });
};


// Get Avg Expense Per Customer
var totalExpensesFinal;
var getTotalExpenses = function () {
    var defer = $.Deferred();
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases",
        success: function (purchases) {
            var totalExpenses = 0;
            purchases = JSON.parse(purchases);
            console.log(purchases);
            var temp;
            for (var i = 0; i < purchases.length; i++) {
                if (purchases[i].TotalMerc < 0)
                    totalExpenses += purchases[i].TotalMerc;
            }
            var avgExpenseCustomer = Math.abs(totalExpenses) / totalClients;
            console.log("getTotalExpenses --  totalPurchases: " + totalExpenses + "  totalClients: " + totalClients + "  avg:" + avgExpenseCustomer);
            $(".avgExpenseCustomer").html("<p>" + avgExpenseCustomer.toFixed(2) + " € </p>");
            totalExpensesFinal = totalExpenses;

        }
    }).fail(function () {
        alert("ERROR: getting purchases list");
    });
    setTimeout(function () {
        defer.resolve();
    }, 1000);
    return defer;
};

// Get Avg Sale Per Active Customer
var getAvgSaleActiveCustomer = function () {
    var defer = $.Deferred();
    while (totalSalesFinal === null) {

    }
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetTop10Clients",
        success: function (topClients) {
            topClients = JSON.parse(topClients);
            var activeClients = topClients.length;
            console.log("totalSales: " + totalSalesFinal + " activeClients: " + topClients.length)
            var avgSaleActiveCustomer = totalSalesFinal / topClients.length;
            $(".avgSalePerActiveCustomer").append("<p>" + avgSaleActiveCustomer + "€ </p>");

        }
    }).fail(function () {
        console.log("ERROR: getting top 10 clients");
    });

    setTimeout(function () {
        defer.resolve();
    }, 1000);
    return defer;
}


getTotalClients().then(getTotalSales).then(getTotalExpenses).then(getAvgSaleActiveCustomer);