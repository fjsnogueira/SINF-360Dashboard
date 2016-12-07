$(document).ready(function () {
    var top10ClientsDataSet = [];
    var plotOptions = {};

    // Formatting price
    function formatPrice(price) {
        return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
    }
    String.prototype.reverse = function () {
        return this.split('').reverse().join('');
    }

    $(function () {
        $.ajax({
            dataType: "json",
            url: "http://localhost:49751/api/Clients/GetTop10Clients/2016",
            success: function (topClients) {
                topClients = JSON.parse(topClients);

                for (var i = 0; i < topClients.length; i++) {
                    $(".active-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + topClients[i].CodCliente + "</td><td>" + topClients[i].NumeroCompras + "</td><td>" + formatPrice(topClients[i].TotalCompras.toFixed(2)) + " €</td>");
                }

                // treat array      
                if (topClients.length > 10) {
                    topClients.slice(0, 9);
                }

                // Get total sales and data for chart
                var clientData = [];

                for (var i = 0; i < topClients.length; i++) {
                    var codCliente = topClients[i].CodCliente;
                    var temp = { label: codCliente, value: topClients[i].TotalCompras.toFixed(2) };
                    clientData.push(temp);
                }
                $(".loadingTopClients").remove()
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



