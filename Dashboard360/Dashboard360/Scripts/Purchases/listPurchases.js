$(function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases/2016",
        success: function (purchases) {
            var typesOfPurchase = ['VNC', 'VND', 'VVD', 'VFA', 'VFG', 'VFI', 'VFM', 'VFO', 'VFP', 'VFR'];
            purchases = JSON.parse(purchases);
            var purchasesF0001 = 0;
            var purchasesF0002 = 0;
            var purchasesF0003 = 0;
            var purchasesF0004 = 0;
            var purchasesF0005 = 0;
            var purchasesSER = 0;
            var totalExpenses = 0;

            for (var i = 0; i < purchases.length; i++) {
                var temp = purchases[i];
                if (moment(temp.Data).year() == 2016) {
                    if ($.inArray(temp.TipoDoc, typesOfPurchase) !== -1) {
                        if (temp.Entidade == "F0001")
                            purchasesF0001 += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        else if (temp.Entidade == "F0002")
                            purchasesF0002 += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        else if (temp.Entidade == "F0003")
                            purchasesF0003 += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        else if (temp.Entidade == "F0004")
                            purchasesF0004 += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        else if (temp.Entidade == "F0005")
                            purchasesF0005 += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        else if (temp.Entidade == "SER")
                            purchasesSER += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                        $(".expenses-modal-body").append("<tr><td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td>" + formatPrice(temp.TotalMerc + temp.TotalOutros - temp.TotalDesc) + " €</td>");
                    }
                }
            }

            console.log("F0001: " + purchasesF0001);
            console.log("F0002: " + purchasesF0002);
            console.log("F0003: " + purchasesF0003);
            console.log("F0004: " + purchasesF0004);
            console.log("F0005: " + purchasesF0005);
            console.log("SER: " + purchasesSER);
            totalExpenses = purchasesF0001 + purchasesF0002 + purchasesF0003 + purchasesF0004 + purchasesF0005 + purchasesSER;
            console.log("TotalExpenses: " + totalExpenses);

        }
    }).fail(function () {
        alert("ERROR: getting purchases list");
    });


});