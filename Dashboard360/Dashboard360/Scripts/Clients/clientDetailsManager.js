var year = moment().year();
var clientID = $('#client-id').text();

var getAllSales = function () {
    var salesDocs = ["NC", "ND", "VD", "DV", "AVE"];
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/" + clientID + "/totalSalesByClient/" + clientID,
        success: function (sales) {
            sales = JSON.parse(sales);
            console.log(sales);
            var totalEver = 0;
            var totalThisYear = 0;
            var totalSales = sales.length;
            var totalSalesThisYear = 0;
            var totalOrders = 0;
            var winter = 0;
            var summer = 0;
            var spring = 0;
            var fall = 0;
            for (var i = 0; i < sales.length; i++) {
                var temp = sales[i];
                if (temp.TipoDoc.charAt(0) == 'F' || $.inArray(temp.TipoDoc, salesDocs) !== -1) {
                    var liquid = 0;
                    for (var j = 0; j < temp.LinhasDoc.length; j++) {
                        liquid += temp.LinhasDoc[j].TotalLiquido;
                        if (temp.LinhasDoc[j].TotalLiquido > 0)
                            $(".sales-modal-body").append("<tr> <td>" + temp.LinhasDoc[j].DescArtigo + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(temp.LinhasDoc[j].TotalLiquido.toFixed(2)) + "€ </td></tr>");
                    }
                    var month = moment(temp.Data).month();
                    if (month == 12 || month == 1 || month == 2)
                        winter++;
                    if (month == 3 || month == 4 || month == 5)
                        spring++;
                    if (month == 6 || month == 7 || month == 8)
                        summer++;
                    if (month == 9 || month == 10 || month == 11)
                        fall++;
                    // Modal Total Sales

                    totalEver += liquid;
                    if (moment(temp.Data).year() == year) {
                        totalThisYear += liquid;
                        totalSalesThisYear++;
                        // Modal Year Sales
                        for (var k = 0; k < temp.LinhasDoc.length; k++) {
                            if (temp.LinhasDoc[k].TotalLiquido > 0)
                                $(".sales-year-modal-body").append("<tr> <td>" + temp.LinhasDoc[k].DescArtigo + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(temp.LinhasDoc[k].TotalLiquido.toFixed(2)) + "€ </td></tr>");
                        }

                    }

                }
                if (temp.TipoDoc === "ECL") {
                    var ecl = 0;
                    for (var j = 0; j < temp.LinhasDoc.length; j++) {
                        ecl += temp.LinhasDoc[j].TotalLiquido;
                        if (temp.LinhasDoc[j].TotalLiquido > 0)
                            $(".orders-modal-body").append("<tr> <td>" + temp.LinhasDoc[j].DescArtigo + "</td> <td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(temp.LinhasDoc[j].TotalLiquido.toFixed(2)) + "€ </td></tr>");
                    }
                    totalOrders += ecl;
                }
            }

            $(".totalSales").append(formatPrice(Math.floor(totalEver).toString()) +
               " <span style='font-size: 20px!important;'>" + totalEver.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotal").hide();

            $(".totalSalesThisYear").append(formatPrice(Math.floor(totalThisYear).toString()) +
               " <span style='font-size: 20px!important;'>" + totalThisYear.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotalThisYear").hide();

            var avg = totalEver / totalSales;
            $(".averageSale").append(formatPrice(Math.floor(avg).toString()) +
               " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingAverageSale").hide();

            var avgYear = totalThisYear / totalSales
            $(".averageSaleThisYear").append(formatPrice(Math.floor(avgYear).toString()) +
               " <span style='font-size: 20px!important;'>" + avgYear.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingAverageSaleThisYear").hide();

            $(".totalOrders").append(formatPrice(Math.floor(totalOrders).toString()) +
               " <span style='font-size: 20px!important;'>" + totalOrders.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingTotalOrders").hide();


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
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
}



var getTopProducts = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Products/topProductsByClient/" + clientID,
        success: function (products) {
            products = JSON.parse(products);
            //   console.log(products);
            var productData = [];
            var descriptions = [];
            for (var i = 0; i < products.length; i++) {
                if (products[i].VolumeVendas > 0) {
                    var temp = { prod: products[i].CodArtigo, sale: products[i].VolumeVendas.toFixed(2) };
                    productData.push(temp);
                    descriptions.push(products[i].DescArtigo);
                }

            }

            productData = shuffle(productData);
            $(".loadingTopProducts").remove()
            Morris.Bar({
                element: 'top-products',
                data: productData,
                xkey: 'prod',
                ykeys: ['sale'],
                hoverCallback: function (index, options, content) {
                    console.log(content.prod);
                    content += descriptions[index];
                    return (content);
                },
                labels: ['Value [€]']
            });
        }

    }).fail(function () {
        console.log("ERROR: getting top products");
    });
}

$(function () {
    getAllSales();
    getTopProducts();
});

// Utilities

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

// Utilities
$.getScript("/Scripts/moment.min.js", function () {
});


function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}

String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}