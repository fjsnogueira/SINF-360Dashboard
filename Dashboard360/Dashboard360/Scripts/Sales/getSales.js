$(function () {
    $.getScript("Scripts/moment.min.js", function () {
    });

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/2016",
        success: function (sales) {
            sales = JSON.parse(sales);
           // console.log(sales);
            var typesOfSale = [];
            for (var i = 0; i < sales.length; i++) {
                var temp = sales[i];
                if ($.inArray(temp.TipoDoc, typesOfSale) === -1)
                    typesOfSale.push(temp.TipoDoc);
            }
            
            console.log(typesOfSale);

        /*    $(".totalSales").html(
                "<p>" + sales.length + " Total Sales </p>"
                );

            $(".firstSale").html(
                "<p> Entidade: " + sales[0].Entidade + "</p>" +
                "<p> Valor: " + (sales[0].TotalIva + sales[0].TotalMerc) + "€ </p>" +
                "<p> Data:  " + moment(sales[0].Data).toDate() + "</p>"
                );


            // Number of sales this year
            var salesThisYear = 0;
            for (var i = 0; i < sales.length; i++) {
                if ((moment(sales[0].Data).toDate().getYear() + 1900) == 2016) {
                    salesThisYear++;
                }
            }
            $(".salesThisYear").html(
                "<p>" + salesThisYear + "</p>"
                );


            // Total in sales this year
            var totalInSales = 0;
            for (var i = 0; i < sales.length; i++) {
                totalInSales += (sales[i].TotalIva + sales[i].TotalMerc);
            }
            $(".salesTotalThisYear").html(
               "<p>" + totalInSales + "</p>"
              );
            return sales;*/
        }
    }).fail(function () {
        alert("ERROR: getting sales list");
    });


});