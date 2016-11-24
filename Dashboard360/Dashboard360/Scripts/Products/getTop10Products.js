$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Products/GetTop10ProductsSold",
        success: function (topProductsSold) {
            topProductsSold = JSON.parse(topProductsSold);
            console.log(topProductsSold);
            var productInfoHTML;
            $(".topTenProductsSold").append("<p>");
            for (var i = 0; i < 10; i++) {
                productInfoHTML = (i + 1) + " - " + topProductsSold[i].CodArtigo + " - " + /*topProductsSold[i].DescArtigo + " - "*/ + topProductsSold[i].QuantidadeVendida + " - " + topProductsSold[i].VolumeVendas.toFixed(2) + "€ <br>";
                $(".topTenProductsSold").append(productInfoHTML);
            }
            $(".topTenProductsSold").append("</p>");
        }
         
    }).fail(function () {
        console.log("ERROR: getting top 10 products sold");
    });


});