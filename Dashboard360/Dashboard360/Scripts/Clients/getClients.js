$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/clients",
        success: function (clients) {
            clients = JSON.parse(clients);
            $(".numberOfCustomers").html(
                "<p>" + clients.length + " Active Customers </p>"
                )}
    }).fail(function () {
        alert("ERROR: getting client list");
    });

    
});