$(function () {



    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetTop10Clients",
        success: function (topClients) {
            topClients = JSON.parse(topClients);
            console.log(topClients);
            var clientInfoHTML;
            $(".topTenCustomers").append("<p>");
            for (var i = 0; i < 10; i++) {
                clientInfoHTML = (i + 1) + " - " + topClients[i].Nome + " - " + topClients[i].NumeroCompras + " - " + topClients[i].TotalCompras.toFixed(2) + "€ <br>";
                $(".topTenCustomers").append(clientInfoHTML);
            }
            $(".topTenCustomers").append("</p>");
        }

    }).fail(function () {
        console.log("ERROR: getting top 10 clients");
    });


});