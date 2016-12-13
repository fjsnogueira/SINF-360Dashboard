var year = moment().year();
var productID = $('#product-id').text();




var getTotalSales = function () {
    console.log("===================================");
    console.log("id: " + productID);
    console.log("year : " + year);
    console.log("===================================");

    var salesDocs = ["NC", "ND", "VD", "DV", "AVE"];
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/products/GetSales/" + productID,
        success: function (sales) {
            console.log(sales);
            var totalSales = 0;
            var totalSalesThisYear = 0;
            var totalOrders = 0;
            var ordersCounter = 0;
            var winter = 0;
            var spring = 0;
            var summer = 0;
            var fall = 0;
            var unitPrice = 0;

            for (var i = 0; i < sales.length; i++) {
                var temp = sales[i];
                if (temp.TipoDoc.charAt(0) == 'F' || $.inArray(temp.TipoDoc, salesDocs) !== -1) {
                    if (temp.LinhasDoc.length > 0) {
                        var liquid = temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        $(".sales-modal-body").append("<tr> <td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(liquid.toFixed(2)) + "€ </td></tr>");
                        if (unitPrice == 0) {
                            unitPrice = temp.LinhasDoc[0].PrecoUnitario;
                        }
                        totalSales += liquid;
                        // Get Seasonality
                        var month = moment(temp.Data).month();
                        if (month == 12 || month == 1 || month == 2)
                            winter++;
                        if (month == 3 || month == 4 || month == 5)
                            spring++;
                        if (month == 6 || month == 7 || month == 8)
                            summer++;
                        if (month == 9 || month == 10 || month == 11)
                            fall++;

                        if (moment(temp.Data).year() == 2016) {
                            $(".sales-year-modal-body").append("<tr> <td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(liquid.toFixed(2)) + "€ </td></tr>");
                            totalSalesThisYear += liquid;
                        }
                    }
                } else if (temp.TipoDoc == "ECL" && moment(temp.Data).year() == 2016) {
                    ordersCounter++;
                    var liq = temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                    totalOrders += liq;
                    $(".sales-year-modal-body").append("<tr> <td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(liq.toFixed(2)) + "€ </td></tr>");
                }
            }

            // Total Sales
            $(".totalSales").append(formatPrice(Math.floor(totalSales).toString()) +
               " <span style='font-size: 20px!important;'>" + totalSales.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotal").hide();
            console.log("Total Sales: " + totalSales);

            // Total Sales This Year
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
            $(".numberOrders").append(formatPrice(Math.floor(ordersCounter).toString()));
            $(".loadingNumberOrders").hide();
            console.log("Number Orders: " + ordersCounter);

            // Number of Orders
            $(".unitPrice").append(formatPrice(Math.floor(unitPrice).toString()));
            $(".loadingUnitPrice").hide();
            console.log("Unit Price: " + unitPrice);

            // Seasonality 
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



var getTopBuyers = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/products/" + productID + "/GetTopBuyers/" + year,
        success: function (topBuyers) {
            topBuyers = JSON.parse(topBuyers);
            console.log("topbuyers" + topBuyers);
            var productData = [];
            var len = topBuyers.length;
            if (len >= 10)
                len = 10;

            for (var i = 0; i < len; i++) {
                if (topBuyers[i].TotalCompras > 0) {
                    console.log(topBuyers[i]);
                    var temp = { name: topBuyers[i].Nome, sale: topBuyers[i].TotalCompras.toFixed(2) };
                    productData.push(temp);
                }
            }

            productData = shuffle(productData);
            $(".loadingTopBuyers").remove();
            Morris.Bar({
                element: 'top-buyers',
                data: productData,
                xkey: 'name',
                ykeys: ['sale'],
                labels: ['Value [€]']
            });
        }



    }).fail(function () {
        console.log("ERROR: getting top buyers");
    });
}

var getInventory = function () {

}

$(function () {
 //   getTotalSales();
    getTopBuyers();
    // getInventory();
});



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