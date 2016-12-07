function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}

// Need to extend String prototype for convinience
String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

$(function () {
    $.getScript("/Scripts/moment.min.js", function () {
    });
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/clients",
        success: function (clients) {
            clients = JSON.parse(clients);
            totalClients = clients.length;

            $.ajax({
                dataType: "json",
                url: "http://localhost:49751/api/purchases",
                success: function (purchases) {
                    var totalExpenses = 0;
                    
                    purchases = JSON.parse(purchases);
                    console.log(purchases);
                    var temp;
                    for (var i = 0; i < purchases.length; i++) {
                      //  if (moment(purchases[i].Data).year() == 2016)
                        //     totalExpenses += (purchases[i].TotalMerc + purchases[i].TotalIva);
                     //   console.log("purcha")
                    }
                //    var avgExpenseCustomer = Math.abs(totalExpenses) / totalClients;
           //         console.log("avgExpense: " + avgExpenseCustomer);
           //         $(".avgExpenseCustomer").append(formatPrice(Math.floor(avgExpenseCustomer).toString()) +
          //             " <span style='font-size: 20px!important;'>" + avgExpenseCustomer.toString().split(".")[1].slice(0, 2) + "</span>");
            //        totalExpensesFinal = totalExpenses;
                }
            }).fail(function () {
                alert("ERROR: getting purchases list");
            });
        }
    }).fail(function () {
        alert("ERROR: getting client list");
    });


});

