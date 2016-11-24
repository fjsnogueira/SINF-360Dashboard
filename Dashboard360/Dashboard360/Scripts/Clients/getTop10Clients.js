$(function () {
    $.ajax({
    dataType: "json",
    url: "http://localhost:49751/api/Clients/GetTop10Clients",
    success: function (topClients) {
        topClients = JSON.parse(topClients);
        $(".topTenCustomers").append("<p>");
        // treat array      
        var len;
        if (topClients.length > 10)
            len = 10;
        else len = topClients.length;
        // Get info
        var clientInfoHTML;
        for (var i = 0; i < len; i++) {
            clientInfoHTML = topClients[i].CodCliente + " - " + topClients[i].NumeroCompras + " - " + topClients[i].TotalCompras.toFixed(2) + "€ <br>";
            $(".topTenCustomers").append(clientInfoHTML);
        }
        $(".topTenCustomers").append("</p>");

    }
}).fail(function () {
    console.log("ERROR: getting top 10 clients");
});
    
});