$(function () {
    getTop10Clients();
    getTop10ProductsSold();
    getExpenses();
    getTotalTurnover();
    getHRExpenses();
    getAvgSalePrice();
    getSalesRevenueGrowth();
});

var getSalesRevenueGrowth = function () {
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/2016",
        success: function (sales) {
            sales = JSON.parse(sales);
            var totalSales = 0;
            for (var i = 0; i < sales.length; i++) {
                totalSales += sales[i].TotalMerc;
            }
            $.ajax({
                dataType: "json",
                url: "http://localhost:49751/api/sales/2015",
                success: function (sales2) {
                    sales2 = JSON.parse(sales2);
                    var totalSales1 = 0;
                    for (var i = 0; i < sales2.length; i++) {
                        totalSales1 += sales2[i].TotalMerc;
                    }
                    if (totalSales1 == 0)
                        $(".salesRevenueGrowth").append("<p> - </p>");
                    else {
                        var total = 100 * (totalSales / totalSales1 - 1);
                        $(".salesRevenueGrowth").append(total.toFixed(2) + " % ");
                    }
                    $(".loadingSalesRevenueGrowth").hide();
                }
            }).fail(function () {
              //  alert("ERROR: getting sales list");
                $(".loadingSalesRevenueGrowth").hide();
            });
            setTimeout(function () {
                defer.resolve();
            }, 5000);
        }
    }).fail(function () {
     //   alert("ERROR: getting sales list");
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
}


var getAvgSalePrice = function () {
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/2016",
        success: function (sales) {
            sales = JSON.parse(sales);
            var totalSales = 0;
            for (var i = 0; i < sales.length; i++) {
                totalSales += sales[i].TotalMerc;

            }

            var avgSale = (totalSales / sales.length);
            $(".averageSalesPrice").append(formatPrice(Math.abs(Math.floor(avgSale)).toString()) + " <span style='font-size: 20px!important;'>" + avgSale.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingAverageSalesPrice").hide();
        }
    }).fail(function () {
        //alert("ERROR: getting sales list");
        $(".loadingAverageSalesPrice").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;

}


var getHRExpenses = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetHumanResourcesExpenses/2016",
        success: function (humanResourcesExpenses) {

            humanResourcesExpenses = JSON.parse(humanResourcesExpenses);
            console.log(humanResourcesExpenses);

            var sum = 0;
            d = new Date(-62135596800000);

            $(".total-human-resources-modal-body").empty();
            $(".total-human-resources-modal-body").append("<th><strong>" +
                "Name" + "</strong></th>" +
                "<th><strong>" +
                "Annual Base Salary (€)" +
                "</strong></th> <th><strong>" +
                "Annual Holiday and Christmas Subsidy (€)" + "</strong></th> <th><strong>" +
                "Annual Food Allowance (€) </strong></th> <th><strong>" + "Annual Social Security Expenses With Employee (€) </strong></th>");

            for (i = 0; i < humanResourcesExpenses.length; i++) {

                data = new Date(humanResourcesExpenses[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                if (data.getTime() == d.getTime()) {
                    $(".total-human-resources-modal-body").append("<tr> <td>" + humanResourcesExpenses[i].nome + "</td><td>" + (humanResourcesExpenses[i].salario * 12) + "</td><td>" + (humanResourcesExpenses[i].salario * 2) + "</td><td>" + (humanResourcesExpenses[i].subsidioAlim * 21 * 12).toFixed(2) + "</td><td>" + (humanResourcesExpenses[i].salario * 14 * 0.2375).toFixed(2) + "</td><td>");
                    sum = sum + (humanResourcesExpenses[i].salario * 14) + (humanResourcesExpenses[i].subsidioAlim * 21 * 12) + (humanResourcesExpenses[i].salario * 0.2375 * 14);
                }
                else {

                    dataDemissao = new Date(humanResourcesExpenses[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                    dataInicial = new Date(parseInt($('#yearTitle').text()), 0, 1, 1, 0, 0, 0);

                    console.log(dataDemissao);
                    console.log(dataInicial);

                    function monthDiff(d1, d2) {
                        var months;
                        months = (d2.getFullYear() - d1.getFullYear()) * 12;
                        months -= d1.getMonth() + 1;
                        months += d2.getMonth();
                        return months <= 0 ? 0 : months;
                    }

                    var ret = monthDiff(dataInicial, dataDemissao);
                    console.log(ret);

                    $(".total-human-resources-modal-body").append("<tr> <td>" + humanResourcesExpenses[i].nome + "</td><td>" + (humanResourcesExpenses[i].salario * ret) + "</td><td>" + 0 + "</td><td>" + (humanResourcesExpenses[i].subsidioAlim * 21 * ret).toFixed(2) + "</td><td>" + (humanResourcesExpenses[i].salario * ret * 0.2375).toFixed(2) + "</td><td>");
                    sum = sum + (humanResourcesExpenses[i].salario * ret) + (humanResourcesExpenses[i].subsidioAlim * 21 * ret) + (humanResourcesExpenses[i].salario * 0.2375 * ret);
                }


            }

            $(".loadingHR").hide();
            $(".humanResourcesExpenses").append(formatPrice(Math.floor(sum).toString()) + " <span style='font-size: 20px!important;'>" + sum.toString().split(".")[1].slice(0, 2) + "</span>");

        }
    }).fail(function () {
       // alert("ERROR: getting human resources expenses!");
    });
}

var getTotalTurnover = function () {
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/2016",
        success: function (sales) {
            sales = JSON.parse(sales);
            var totalSales = 0;
            for (var i = 0; i < sales.length; i++) {
                totalSales += sales[i].TotalMerc;
            }
            $.ajax(
            {
                dataType: "json",
                url: "http://localhost:49751/api/balance/2015",
                success: function (balance) {
                    balance = JSON.parse(balance);
                    var totalAssets = 0;
                    for (i in balance) {
                        totalAssets += balance[i].Mes00DB + balance[i].Mes01DB + balance[i].Mes02DB + balance[i].Mes03DB + balance[i].Mes04DB
                            + balance[i].Mes05DB + balance[i].Mes06DB + balance[i].Mes07DB + balance[i].Mes08DB + balance[i].Mes09DB + balance[i].Mes10DB
                            + balance[i].Mes11DB + balance[i].Mes12DB + balance[i].Mes13DB + balance[i].Mes14DB + balance[i].Mes15DB;
                    }
                    if (totalAssets == 0)
                        $(".totalTurnover").append("<p> - </p>");

                    var turnover = (totalSales / totalAssets).toFixed(2);
                    $(".totalTurnover").append(formatPrice(Math.abs(Math.floor(turnover)).toString()) + " <span style='font-size: 20px!important;'>" + turnover.toString().split(".")[1].slice(0, 2) + "</span>");

                    $(".loadingTotalTurnover").hide();
                }
            });
        }
    }).fail(function () {
      //  alert("ERROR: getting sales list");
        $(".loadingTotalTurnover").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

function getTop10Clients() {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetTop10Clients/2016",
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
            $(".loadingTopCustomers").hide();
            $("#top-clients").show();
            Morris.Donut({
                element: 'top-clients',
                data: clientData
            }).on('click', function (i, row) {
                window.location = "http://localhost:49751/Clients/ShowDetails/" + row.label;
            });
        }
    }).fail(function () {
        console.log("ERROR: getting top 10 clients");
    });
}

function getTop10ProductsSold() {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Products/GetTop10ProductsSold/2016",
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
            $(".loadingTopProducts1").hide()
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
            }).on('click', function (i, row) {
                window.location = "http://localhost:49751/Products/ShowDetails/" + row.prod;
            });;
        }

    }).fail(function () {
        console.log("ERROR: getting top 10 products sold");
    });
}


var typesOfPurchase = ['VNC', 'VND', 'VVD', 'VFA', 'VFG', 'VFI', 'VFM', 'VFO', 'VFP', 'VFR'];
function getExpenses() {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases/2016",
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
            // Total Expenses
            totalExpenses = purchasesF0001 + purchasesF0002 + purchasesF0003 + purchasesF0004 + purchasesF0005 + purchasesSER;
            $(".loadingTotalPurchases").hide();
            $(".totalPurchases").append(formatPrice(Math.abs(Math.floor(totalExpenses)).toString()) + " <span style='font-size: 20px!important;'>" + totalExpenses.toString().split(".")[1].slice(0, 2) + "</span>");

            // Avg Expense
            $.ajax({
                dataType: "json",
                url: "http://localhost:49751/api/Clients/",
                success: function (customers) {
                    customers = JSON.parse(customers);
                    totalClients = customers.length;
                    var avgExpense = totalExpenses / totalClients;
                    $(".loadingAvgExpense").hide();
                    $(".avgExpenseCustomer").append(formatPrice(Math.abs(Math.floor(avgExpense)).toString()) + " <span style='font-size: 20px!important;'>" + avgExpense.toString().split(".")[1].slice(0, 2) + "</span>");
                }
            }).fail(function () {
                console.log("ERROR: getting total clients");
            });
            $(".expenses-modal-body").append("<tr><td>" + "TOTAL" + "</td><td>" + "    " + "</td><td style='text-align:right'>" + formatPrice(totalExpenses.toFixed(2).toString()) + " €</td>");



        }

    }).fail(function () {
       // alert("ERROR: getting purchases list");
    });
}


// Utilities

$.getScript("/Scripts/moment.min.js", function () {
});


String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
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