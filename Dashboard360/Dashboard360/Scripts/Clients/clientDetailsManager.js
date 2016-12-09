var year = moment().year();
var clientID = $('#client-id').text();

var getAllSales = function () {
    var salesDocs = ["NC", "ND", "VD", "DV", "AVE"];
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/{clientID}/totalSalesByClient/" + clientID,
        success: function (sales) {
            sales = JSON.parse(sales);
            console.log(sales);
            var totalEver = 0;
            var totalThisYear = 0;
            var totalSales = sales.length;
            var totalSalesThisYear = 0;
            var totalOrders = 0;
            for (var i = 0; i < sales.length; i++) {
                var temp = sales[i];
                if (temp.TipoDoc.charAt(0) == 'F' || $.inArray(temp.TipoDoc, salesDocs) !== -1) {
                    var liquid = temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                    // Modal
                    totalEver += liquid;
                    if (moment(temp.Data).year() == year) {
                        totalThisYear += liquid;
                        totalSalesThisYear++;
                        // Modal
                    }
                    //  $(".sales-modal-body").append("<tr> <td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(liquid.toFixed(2)) + "€ </td></tr
                }
                if (temp.TipoDoc == "ECL") {
                    totalOrders += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                }
            }
            $(".totalSales").append(totalEver);
            $(".totalSalesThisYear").append(totalThisYear);
            $(".averageSale").append(totalEver / totalSales);
            $(".averageSaleThisYear").append(totalThisYear / totalSales);
            $(".totalOrders").append(totalOrders);


        }
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
}



var getTopProducts = function () {

}

$(function () {
    getAllSales();
    getTopProducts(year);
});

