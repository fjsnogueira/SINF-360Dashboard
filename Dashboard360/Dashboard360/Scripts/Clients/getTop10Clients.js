$(document).ready(function () {
    var top10ClientsDataSet = [];
    var plotOptions = {};

    $(function () {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49751/api/Clients/GetTop10Clients/2016",
            success: function (topClients) {
                topClients = JSON.parse(topClients);

                var clientInfoHTML;
                for (var i = 0; i < topClients.length; i++) {
                    $(".active-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + topClients[i].CodCliente + "</td><td>" + topClients[i].NumeroCompras + "</td>");
                    $(".active-clients-modal-body").append(clientInfoHTML);
                }

                // treat array      
                if (topClients.length > 10) {
                    topClients.slice(0, 9);
                }
                $(".topTenCustomers").append("<p>");
                for (var i = 0; i < topClients.length; i++) {
                    clientInfoHTML = "<a href='/Clients/ShowDetails/" + topClients[i].CodCliente + "'>" + topClients[i].CodCliente + " - " + topClients[i].TotalCompras.toFixed(2) + "€ </a> <br>";
                    $(".topTenCustomers").append(clientInfoHTML);
                }
                $(".topTenCustomers").append("</p>");

                // Get total sales and data for chart
                var clientData = [];

                for (var i = 0; i < topClients.length; i++) {
                    var codCliente = topClients[i].CodCliente;
                    console.log(codCliente);
                    var temp = { label: codCliente, value: topClients[i].TotalCompras.toFixed(2) };
                    clientData.push(temp);
                }

                console.log(clientData);
                Morris.Donut({
                    element: 'top-clients',
                    data: clientData
                });
            }
        }).fail(function () {
            console.log("ERROR: getting top 10 clients");
        });

    });


});



