$(function () {
    getTop10Clients();
    getTop10ProductsSold();
    getExpenses();
});


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
            });
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
        alert("ERROR: getting purchases list");
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