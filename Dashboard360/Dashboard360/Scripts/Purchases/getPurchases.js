$(function () {
    $.getScript("Scripts/moment.min.js", function () {
    });

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases",
        success: function (purchases) {
            purchases = JSON.parse(purchases);
            console.log(purchases);
            var totalPurchases = 0;
            var temp;
            for (var i = 0; i < purchases.length; i++) {
                if (purchases[i].TotalMerc > 0)
                    totalPurchases -= purchases[i].TotalMerc;
                else 
                    totalPurchases += purchases[i].TotalMerc;
                /*for (var j = 0; j < temp.length; j++) {
                    totalPurchases += temp[j].
                }*/
            }
            console.log("total purchases: " + totalPurchases);
        }
    }).fail(function () {
        alert("ERROR: getting sales list");
    });


});