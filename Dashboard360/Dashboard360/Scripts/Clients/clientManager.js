// HTTP Requests


// Get Total Sales for Year 'year'
var totalSales;
var getTotalSales = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues/" + year,
        success: function (salesValues) {
            totalSales = salesValues;
            $(".salesTotalThisYear").append(formatPrice(Math.floor(salesValues).toString()) +
" <span style='font-size: 20px!important;'>" + salesValues.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotal").hide();
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
};

var getSalesList = function (year) {
    var salesDocs = ["NC", "ND", "VD", "DV", "AVE"];
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);
            for (var i = 0; i < sales.length; i++) {
                var temp = sales[i];
                if (temp.TipoDoc.charAt(0) == 'F' || $.inArray(temp.TipoDoc, salesDocs) !== -1) {
                    var liquid = temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                    $(".sales-modal-body").append("<tr> <td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(liquid.toFixed(2)) + "€ </td></tr>");
                }
            }
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
}

// Get Total Clients
var totalClients;
var clientsYear;
var getTotalClients = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/",
        success: function (customers) {
            customers = JSON.parse(customers);
            totalClients = customers.length;
            clientsYear = 0;
            for (var i = 0; i < customers.length; i++) {
                $(".total-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td> <a href='localhost:49751/Clients/ShowDetails/" + customers[i].CodCliente + "' target='_blank'>" + customers[i].CodCliente + "</a></td><td>" + customers[i].NomeCliente + "</td><td>");
                if (moment(customers[i].DataCriacao).year() == year) {
                    clientsYear++;
                }
            }

            if (clientsYear == 0) {
                clientsYear = totalClients;
            }

            $(".loadingNumCustomers").hide();
            $(".numberOfCustomers").append(clientsYear);

            var avg = totalSales / clientsYear;
            $(".loadingAvgSalePerCustomer").hide();
            $(".avgSalePerCustomer").append(formatPrice(Math.floor(avg).toString()) +
                " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");
            getTotalExpenses(year);
        }
    }).fail(function () {
        console.log("ERROR: getting total clients");
    });

}

// Total Active Clients in Year 'year'
var totalActiveClients;
var getTotalActiveClients = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetNumActiveClients/" + year,
        success: function (numActiveCustomers) {
            totalActiveClients = numActiveCustomers;
            $(".loadingActiveCustomers").hide();
            $(".activeClients").append(numActiveCustomers);
            var avg = totalSales / totalActiveClients;
            $(".loadingAvgSalePerActiveCustomer").hide();
            $(".avgSalePerActiveCustomer").append(formatPrice(Math.floor(avg).toString()) +
                " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");
        }
    }).fail(function () {
        console.log("ERROR: getting total active");
    });
    return;
}

// Get Top 10 products for Year 'year'
var top10Products = function (year) {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Products/GetTop10ProductsSold/" + year,
        success: function (topProductsSold) {
            
            topProductsSold = JSON.parse(topProductsSold);
            console.log(topProductsSold);
            for (var i = 0; i < 10; i++) {
                $(".top-products-modal-body").append("<tr><td>" + topProductsSold[i].CodArtigo + "</td><td>" + topProductsSold[i].DescArtigo + "</td><td>" + topProductsSold[i].QuantidadeVendida + "</td><td>" + formatPrice(topProductsSold[i].VolumeVendas.toFixed(2)) + " €</td>");
            }

            var productData = [];
            var descriptions = []
            for (var i = 0; i < 10; i++) {
                var temp = { prod: topProductsSold[i].CodArtigo, sale: topProductsSold[i].VolumeVendas.toFixed(2) };
                productData.push(temp);
                descriptions.push(topProductsSold[i].DescArtigo);
            }

            productData = shuffle(productData);
            $(".loadingTopProducts").hide()
            Morris.Bar({
                element: 'top-products',
                data: productData,
                hoverCallback: function (index, options, content) {
                    console.log(content.prod);
                    content += descriptions[index];

                    return (content);
                },
                xkey: 'prod',
                ykeys: [/*'desc', */'sale'],
                labels: ['Value [€]']
            });
        }

    }).fail(function () {
        console.log("ERROR: getting top 10 products sold");
    });
};

// Get Total Expenses in Year 'year'
var typesOfPurchase = ['VNC', 'VND', 'VVD', 'VFA', 'VFG', 'VFI', 'VFM', 'VFO', 'VFP', 'VFR'];
var getTotalExpenses = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases/" + year,
        success: function (purchases) {
            purchases = JSON.parse(purchases);
            var purchasesF0001 = 0;
            var purchasesF0002 = 0;
            var purchasesF0003 = 0;
            var purchasesF0004 = 0;
            var purchasesF0005 = 0;
            var purchasesSER = 0;
            var totalExpenses = 0;
            for (var i = 0; i < purchases.length; i++) {
                var temp = purchases[i];
                if (moment(temp.Data).year() == 2016) {
                    if ($.inArray(temp.TipoDoc, typesOfPurchase) !== -1) {
                        var salesVolume = temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        if (temp.Entidade == "F0001")
                            purchasesF0001 += salesVolume;
                        else if (temp.Entidade == "F0002")
                            purchasesF0002 += salesVolume;
                        else if (temp.Entidade == "F0003")
                            purchasesF0003 += salesVolume;
                        else if (temp.Entidade == "F0004")
                            purchasesF0004 += salesVolume;
                        else if (temp.Entidade == "F0005")
                            purchasesF0005 += salesVolume;
                        else if (temp.Entidade == "SER")
                            purchasesSER += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        $(".expenses-modal-body").append("<tr><td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align:right'>" + formatPrice(salesVolume.toFixed(2).toString()) + " €</td>");
                    }
                }
            }

            totalExpenses = purchasesF0001 + purchasesF0002 + purchasesF0003 + purchasesF0004 + purchasesF0005 + purchasesSER;
            $(".expenses-modal-body").append("<tr><td>" + "TOTAL" + "</td><td>" + "    " + "</td><td style='text-align:right'>" + formatPrice(totalExpenses.toFixed(2).toString()) + " €</td>");
            var avgExpense = totalExpenses / totalClients;
            $(".loadingAvgExpense").hide();
            $(".avgExpenseCustomer").append(formatPrice(Math.abs(Math.floor(avgExpense)).toString()) + " <span style='font-size: 20px!important;'>" + totalExpenses.toString().split(".")[1].slice(0, 2) + "</span>");
            console.log("F0001: " + purchasesF0001);
            console.log("F0002: " + purchasesF0002);
            console.log("F0003: " + purchasesF0003);
            console.log("F0004: " + purchasesF0004);
            console.log("F0005: " + purchasesF0005);
            console.log("SER: " + purchasesSER);

        }
    }).fail(function () {
        alert("ERROR: getting purchases list");
    });

};

// Get Top 10 Clients in Year 'year'
var getTopClients = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetTop10Clients/" + year,
        success: function (topClients) {
            topClients = JSON.parse(topClients);

            for (var i = 0; i < topClients.length; i++) {
                $(".active-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + topClients[i].CodCliente + "</td><td>" + topClients[i].NumeroCompras + "</td><td style='text-align:right;'>" + formatPrice(topClients[i].TotalCompras.toFixed(2)) + " €</td>");
            }

            // treat array      
            if (topClients.length > 10) {
                topClients.slice(0, 9);
            }

            // Get total sales and data for chart

            var clientData = [];
            for (var i = 0; i < topClients.length; i++) {
                clientData.push({ label: topClients[i].CodCliente, value: topClients[i].TotalCompras.toFixed(2) });
                console.log({ label: topClients[i].CodCliente, value: topClients[i].TotalCompras.toFixed(2) });
            }
            $(".loadingTopClients").hide();
            $("#top-clients").show();
            Morris.Donut({
                element: 'top-clients',
                data: clientData
            });
        }
    }).fail(function () {
        console.log("ERROR: getting top 10 clients");
    });
};


// Utilities
$.getScript("/Scripts/moment.min.js", function () {
});


String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}



var hideValues = function () {
    $(".avgExpenseCustomer").html("");
    $(".salesTotalThisYear").html("");
    $(".avgSalePerActiveCustomer").html("");
    $(".activeClients").html("");
    $(".avgSalePerCustomer").html("");
    $(".numberOfCustomers").html("");
    $("#top-clients").html("");
    $("#top-products").html("");
    // Clear Modals
    $(".sales-modal-body").html("");
    $(".total-clients-modal-body").html("");
    $(".top-products-modal-body").html("");
    $(".active-clients-modal-body").html("");
    $(".expenses-modal-body").html("");

}


var showLoading = function () {
    $(".loadingNumCustomers").show();
    $(".loadingAvgSalePerCustomer").show();
    $(".loadingActiveCustomers").show();
    $(".loadingAvgSalePerActiveCustomer").show();
    $(".loadingSalesTotal").show();
    $(".loadingAvgExpense").show();
    $(".loadingTopClients").show();
    $(".loadingTopProducts").show();
}

function shuffle(array) {
    var currentIndex = array.length, temporaryValue, randomIndex;

    // While there remain elements to shuffle...
    while (0 !== currentIndex) {

        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }
    return array;
}


// Chekc if year is valid
var validYear = function (year) {
    var control = false;
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues/" + year,
        success: function (salesValues) {
            console.log(salesValues);
            if (salesValues != 0) {
                control = true;
            }
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting validYear");
    });
    return control;

};

//Controlling Functions

var getValues = function (year) {
    getTotalClients(year);
    getSalesList(year);
    getTotalSales(year);
    
    getTotalActiveClients(year);
    
    getTopClients(year);
    top10Products(year);
};

var updateValues = function (newYear) {
    hideValues();
    getValues(newYear);
}

$("#previousYear").click(function () {
    var year = parseInt($('#yearTitle').text());
    year--;
    var response = validYear(year);
    console.log("res: " + response);
    if (validYear(year) == false) {
        alert("There are no registers for " + year);
        return;
    }
    hideValues();
    showLoading();
    $("#yearTitle").html(year + " ");
    updateValues(year);
});

$("#nextYear").click(function () {
    var year = parseInt($('#yearTitle').text());
    year++;
    var response = validYear(year);
    console.log("res: " + response);
    if (validYear(year) == false) {
        alert("There are no registers for " + year);
        return;
    }
    hideValues();
    showLoading();
    $("#yearTitle").html(year + " ");
    updateValues(year);
});

getValues(parseInt($('#yearTitle').text()));
