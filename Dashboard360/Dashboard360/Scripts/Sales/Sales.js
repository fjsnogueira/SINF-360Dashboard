$.getScript("/Scripts/moment.min.js", function () {
});
var prepare = function () {
    getTotalSales();
    getTotalClients();
    getTotalActiveClients();
};

var tiposDocVendas = ["FA", "FAI", "FI", "NC", "ND", "VD"];
var totais = [0, 0, 0, 0, 0, 0]
var typesOfDocs = []
var getSalesList = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Sales/2016",
        success: function (sales) {
            sales = JSON.parse(sales);
            console.log(sales);
            var totalVendas = 0;
            var TotalEncomendas = 0;
            for (var i = 0; i < sales.length; i++) {
                if (moment(sales[i].Data).year() == 2016) {
                    if ($.inArray(sales[i].TipoDoc, typesOfDocs) === -1)
                        typesOfDocs.push(sales[i].TipoDoc);
                    if (sales[i].TipoDoc !== "GR" && sales[i].TipoDoc !== "CBA") {
                        var index = $.inArray(sales[i].TipoDoc, typesOfDocs);
                        var value = totais[index] + sales[i].TotalMerc - sales[i].TotalDesc;
                        totais[index] = value;
                        totalVendas += (sales[i].TotalMerc - sales[i].TotalDesc);
                    }

                }
            }

            var sum = 0;
            for (var i = 0; i < totais.length; i++) {
                sum += totais[i];
            }
        }
    }).fail(function () {
        console.log("ERROR: getting total clients");
    });
}

// Get Total Clients
var totalClients;
var getTotalClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/",
        success: function (customers) {
            customers = JSON.parse(customers);
            totalClients = customers.length;
            for (var i = 0; i < customers.length; i++) {
                console.log("append");
                $(".total-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + customers[i].CodCliente + "</td><td>" + customers[i].NomeCliente + "</td><td>");
            }

            $(".numberOfCustomers").append(customers.length);
            var avg = totalSales / totalClients;
            $(".avgSalePerCustomer").append(formatPrice(Math.floor(avg).toString()) +
                " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");
        }
    }).fail(function () {
        console.log("ERROR: getting total clients");
    });

}

// Get Active Clients
var totalActiveClients;
var getTotalActiveClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetNumActiveClients/2016",
        success: function (numActiveCustomers) {
            totalActiveClients = numActiveCustomers;
            $(".activeClients").append(numActiveCustomers);
            var avg = totalSales / totalActiveClients;
            $(".avgSalePerActiveCustomer").append(formatPrice(Math.floor(avg).toString()) +
                " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");
        }
    }).fail(function () {
        console.log("ERROR: getting total active");
    });
    return;
}

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}

// Need to extend String prototype for convinience
String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

// Get Total Sales
var totalSales;
var getTotalSales = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues/2016",
        success: function (salesValues) {
            console.log(salesValues);
            totalSales = salesValues;
            $(".salesTotalThisYear").append(formatPrice(Math.floor(salesValues).toString()) +
                " <span style='font-size: 20px!important;'>" + salesValues.toString().split(".")[1].slice(0, 2) + "</span>");
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
};


// Get Avg Expense Per Customer

var getTotalExpenses = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases",
        success: function (purchases) {
            var totalExpenses = 0;
            purchases = JSON.parse(purchases);
            var temp;
            for (var i = 0; i < purchases.length; i++) {
                if (moment(purchases[i].Data).year() == 2016)
                    totalExpenses += (purchases[i].TotalMerc + purchases[i].TotalIva);
            }
            var avgExpenseCustomer = Math.abs(totalExpenses) / totalClients;
            console.log("avgExpense: " + avgExpenseCustomer);
            $(".avgExpenseCustomer").append(formatPrice(Math.floor(avgExpenseCustomer).toString()) +
               " <span style='font-size: 20px!important;'>" + avgExpenseCustomer.toString().split(".")[1].slice(0, 2) + "</span> €");

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
