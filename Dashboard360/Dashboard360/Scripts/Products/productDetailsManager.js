var year = moment().year();
var productID = $('#product-id').text();

console.log("===================================");
console.log("id: " + productID);
console.log("year : " + year);
console.log("===================================");




var getTopBuyers = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/products/" + productID + "/GetTopBuyers/" + year,
        success: function (topBuyers) {
            topBuyers = JSON.parse(topBuyers);

            // Get Total Sales This Year
            var totalSalesThisYear = 0;
            for (var i = 0; i < topBuyers.length; i++) {
                totalSalesThisYear += topBuyers[i].TotalCompras;
            }



            // Get Top Buyers
            var productData = [];
            var len = topBuyers.length;
            if (len >= 10)
                len = 10;

            for (var i = 0; i < len; i++) {
                if (topBuyers[i].TotalCompras > 0) {
                    var temp = { name: topBuyers[i].Nome, cod: topBuyers[i].CodCliente, sale: topBuyers[i].TotalCompras.toFixed(2) };
                    productData.push(temp);
                }
            }



            productData = shuffle(productData);
            $(".loadingTopBuyers").hide();
            Morris.Bar({
                element: 'top-buyers',
                data: productData,
                xkey: 'name',
                ykeys: ['sale'],
                labels: ['Value [€]']
            }).on('click', function (i, row) {
                window.location = "http://localhost:49751/Clients/ShowDetails/" + row.cod;
            });
        }



    }).fail(function () {
        console.log("ERROR: getting top buyers");
    });
}

var getInventory = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/products/GetStock/" + productID,
        success: function (stock) {
            $(".inventory").append(Math.abs(stock));
            $(".loadingInventory").hide();
        }

    }).fail(function () {
        console.log("ERROR: getting product stock");
    });
}

$(function () {
    getTopBuyers();
    getInventory();
    getValues();
});


var getValues = function () {
    var salesDocs = ["NC", "ND", "VD", "DV", "AVE"];
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/products/GetSales/" + productID,
        success: function (sales) {
            console.log(sales);
            // Variable Declaration
            var totalSales = 0;
            var totalSalesThisYear = 0;
            var totalOrders = 0;
            var numOrders = 0;
            var unitPrice = 0;
            var winter = 0;
            var spring = 0;
            var summer = 0;
            var fall = 0;

            for (var i = 0; i < sales.length; i++) {

                if (sales[i].TipoDoc.charAt(0) === 'F' || $.inArray(sales[i].TipoDoc, salesDocs) !== -1) {
                    var linha = sales[i].LinhasDoc;
                    if (linha.length > 0) {
                        for (var j = 0; j < linha.length; j++) {
                            // Total Sales
                            totalSales += linha[j].TotalLiquido;
                            $(".sales-modal-body").append("<tr> <td> <a href='localhost:49751/Clients/ShowDetails/" + sales[i].Entidade + "' target='_blank'>" + sales[i].Entidade + "</a> </td><td>" + moment.utc(sales[i].Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(linha[j].TotalLiquido.toFixed(2)) + "€ </td></tr>");
                            // Total Sales This Year
                            if (moment(sales[i].Data).year() == 2016) {
                                totalSalesThisYear += linha[j].TotalLiquido;
                                $(".sales-year-modal-body").append("<tr> <td> <a href='localhost:49751/Clients/ShowDetails/" + sales[i].Entidade + "' target='_blank'>" + sales[i].Entidade + "</a></td><td>" + moment.utc(sales[i].Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(linha[j].TotalLiquido.toFixed(2)) + "€ </td></tr>");
                                // Get Seasonality
                                var month = moment(sales[i].Data).month();
                                if (month == 12 || month == 1 || month == 2)
                                    winter++;
                                if (month == 3 || month == 4 || month == 5)
                                    spring++;
                                if (month == 6 || month == 7 || month == 8)
                                    summer++;
                                if (month == 9 || month == 10 || month == 11)
                                    fall++;

                            }
                            //  Unit Price
                            if (unitPrice == 0) {
                                unitPrice = linha[j].PrecoUnitario;
                            }
                        }
                    }
                    // Orders
                } else if (sales[i].TipoDoc === "ECL" && moment(sales[i].Data).year() == 2016) {
                    var linhaEnc = sales[i].LinhasDoc;
                    if (linhaEnc.length > 0) {
                        for (var k = 0; k < linhaEnc.length; k++) {
                            // Total Orders
                            totalOrders += linhaEnc[k].TotalLiquido;
                            // Num Orders
                            numOrders += linhaEnc[k].Quantidade;
                            $(".orders-modal-body").append("<tr> <td> <a href='localhost:49751/Clients/ShowDetails/" + sales[i].Entidade + "' target='_blank'>" + sales[i].Entidade + "</a></td><td>" + moment.utc(sales[i].Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(linhaEnc[k].TotalLiquido.toFixed(2)) + "€ </td></tr>");
                        }

                    }

                }
            }

            // Send to Page
            // Total Sales
            console.log("Total Sales: " + totalSales);
            $(".totalSales").append(formatPrice(Math.floor(totalSales).toString()) +
               " <span style='font-size: 20px!important;'>" + totalSales.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotal").hide();

            $(".totalSalesThisYear").append(formatPrice(Math.floor(totalSalesThisYear).toString()) +
           " <span style='font-size: 20px!important;'>" + totalSalesThisYear.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotalThisYear").hide();
            console.log("Total Sales This Year: " + totalSalesThisYear);

            // Total Orders
            $(".totalOrders").append(formatPrice(Math.floor(totalOrders).toString()) +
               " <span style='font-size: 20px!important;'>" + totalOrders.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingTotalOrders").hide();
            console.log("Total Orders: " + totalOrders);

            // Number of Orders
            $(".numberOrders").append(formatPrice(Math.floor(numOrders).toString()));
            $(".loadingNumberOrders").hide();
            console.log("Number Orders: " + numOrders);

            // Number of Orders
            $(".unitPrice").append(formatPrice(Math.floor(unitPrice).toString()));
            $(".loadingUnitPrice").hide();
            console.log("Unit Price: " + unitPrice);

            drawGraph(winter, spring, summer, fall);

        }
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
}


// Seasonality
var drawGraph = function (winter, spring, summer, fall) {
    var seasonData = [{ label: "Winter", data: winter }, { label: "Spring", data: spring }, { label: "Summer", data: summer }, { label: "Fall", data: fall }];

    $(".loadingSeasonality").hide();
    $("#seasonality").show();

    var data = {
        labels: [
            "Winter",
            "Spring",
            "Summer",
            "Fall"
        ],
        datasets: [
            {
                data: [winter, spring, summer, fall],
                backgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56",
                    "#9370DB"
                ],
                hoverBackgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56",
                    "#9370DB"
                ]
            }]
    };
    var ctx = $("#seasonality");
    var myPieChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            scales: {

            }
        }
    });
}


// Utilities

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}

String.prototype.reverse = function () {
    return this.split('').reverse().join('');
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