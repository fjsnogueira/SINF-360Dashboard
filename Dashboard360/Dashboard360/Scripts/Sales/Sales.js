$.getScript("Scripts/moment.min.js", function () {
});
var prepare = function () {
    getTotalSales();
    getTotalClients();
    getTotalActiveClients();
};

// Get Total Clients
var totalClients;
var getTotalClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetNumClients",
        success: function (numCustomers) {
            totalClients = numCustomers;
            $(".numberOfCustomers").append("<p> " + numCustomers + " total customers </p>");
            var avg = totalSales / totalClients;
            $(".avgSalePerCustomer").append(avg.toFixed(2) + " €");
        }
    }).fail(function () {
        console.log("ERROR: getting total clients");
    });
    return;

}

// Get Active Clients
var totalActiveClients;
var getTotalActiveClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetNumActiveClients",
        success: function (numActiveCustomers) {
            totalActiveClients = numActiveCustomers;
            $(".activeClients").append("<p> " + numActiveCustomers + " active customers </p>");
            var avg = totalSales / totalActiveClients;
            $(".avgSalePerActiveCustomer").append(avg.toFixed(2) + " €");
        }
    }).fail(function () {
        console.log("ERROR: getting total active");
    });
    return;
}

// Get Total Sales
var totalSales;
var getTotalSales = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues",
        success: function (salesValues) {
            $(".salesTotalThisYear").append("<p> " + salesValues.toFixed(2) + "€ </p>");
            totalSales = salesValues.toFixed(2);

        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
    return;
};


// Get Avg Expense Per Customer

var getTotalExpenses = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases",
        success: function (purchases) {
            var totalExpenses = 0;
            purchases = JSON.parse(purchases);
            console.log(purchases);
            var temp;
            for (var i = 0; i < purchases.length; i++) {
                console.log(moment(purchases[i].Data).year());
                if (moment(purchases[i].Data).year() == 2016 && purchases[i].TotalMerc < 0)
                    totalExpenses += (purchases[i].TotalMerc + purchases[i].TotalIva);
            }
            var avgExpenseCustomer = Math.abs(totalExpenses) / totalClients;
            console.log("getTotalExpenses --  totalPurchases: " + totalExpenses + "  totalClients: " + totalClients + "  avg:" + avgExpenseCustomer);
            $(".avgExpenseCustomer").html("<p>" + avgExpenseCustomer.toFixed(2) + " € </p>");
            totalExpensesFinal = totalExpenses;

        }
    }).fail(function () {
        alert("ERROR: getting purchases list");
    });
};

$(function () {
    prepare();
    getTotalExpenses();
});
