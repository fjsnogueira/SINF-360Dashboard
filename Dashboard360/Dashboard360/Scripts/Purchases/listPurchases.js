$(function () {
    $.getScript("Scripts/moment.min.js", function () {
    });
    console.log("listPurchases");
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases",
        success: function (purchases) {
            purchases = JSON.parse(purchases);
            console.log(purchases);
            var temp;
            var purchasesByEntity;

            var purchasesF0001 = 0;
            var purchasesF0002 = 0;
            var purchasesF0003 = 0;
            var purchasesF0004 = 0;
            var purchasesF0005 = 0;
            var purchasesSER = 0;


            //$(".purchase-list").append("<p>");
            for (var i = 0; i < purchases.length; i++) {
                temp = purchases[i];
                console.log(moment(temp.Data).year());
                //$(".purchase-list").append(moment(purchases[i].Data).toDate() + " - " + purchases[i].Entidade + " - (" + purchases[i].TotalMerc +")");
                /*for (var j = 0; j < temp.length; j++) {
                    $(".purchase-list").append("<br>    " + temp[j].DescArtigo + " - " + temp[j].TotalILiquido);
                }
                $(".purchase-list").append("<br><br>");
            }*/
                //$(".purchase-list").append("</p>");
                if (temp.TotalMerc < 0 && moment(temp.Data).year() == 2016) {
                    if (temp.Entidade == "F0001")
                        purchasesF0001 += temp.TotalMerc;
                    else if (temp.Entidade == "F0002")
                        purchasesF0002 += temp.TotalMerc;
                    else if (temp.Entidade == "F0003")
                        purchasesF0003 += temp.TotalMerc;
                    else if (temp.Entidade == "F0004")
                        purchasesF0004 += temp.TotalMerc;
                    else if (temp.Entidade == "F0005")
                        purchasesF0005 += temp.TotalMerc;
                    else if (temp.Entidade == "SER")
                        purchasesSER += temp.TotalMerc;
                }
            }
            console.log("F0001: " + purchasesF0001);
            console.log("F0002: " + purchasesF0002);
            console.log("F0003: " + purchasesF0003);
            console.log("F0004: " + purchasesF0004);
            console.log("F0005: " + purchasesF0005);
            console.log("SER: " + purchasesSER);
        }
    }).fail(function () {
        alert("ERROR: getting purchases list");
    });


});