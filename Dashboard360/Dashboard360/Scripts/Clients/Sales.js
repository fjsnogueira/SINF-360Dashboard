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


// Get Active Clients



// Get Total Sales



// Get Avg Expense Per Customer


$(function () {
    prepare();
    getTotalExpenses();
});
