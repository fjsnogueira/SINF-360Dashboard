$(function () {
    $.getScript("Scripts/moment.min.js", function () {
    });

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales",
        success: function (sales) {
            sales = JSON.parse(sales);
            console.log(sales);
            $(".totalSales").html("<p>" + sales.length + " Total Sales </p>");

            // Number of sales this year
            var salesThisYear = 0;
            for (var i = 0; i < sales.length; i++) {
                if (moment(sales[i].Data).year() == 2016)
                    salesThisYear++;
            }
            $(".salesThisYear").html("<p>" + salesThisYear + "</p>");

            // Total in sales this year
            var totalInSales = 0;
            for (var i = 0; i < sales.length; i++) {
                if (moment(sales[i].Data).year() == 2016) {
                   // var tipoDoc = sales[i].TipoDoc;
                    //console.log(moment(sales[i].Data).year() + " - " + tipoDoc + " - " + sales[i].TotalMerc);
                    // if (tipoDoc == 'FA' || tipoDoc == 'ND' || tipoDoc == 'NC' || tipoDoc == 'VD' /*|| tipoDoc == 'ORC' || tipoDoc == 'GR'*/) {
                    //      console.log(tipoDoc + " - " + sales[i].TotalMerc);
                    totalInSales += (sales[i].TotalMerc /*+ sales[i].TotalIva*/);
                    // }
                }
            }
            $(".salesTotalThisYear").html("<p>" + totalInSales.toFixed(2) + "€</p>");

        }
    }).fail(function () {
        alert("ERROR: getting sales list");
    });


});