$(function () {
    $.getScript("/Scripts/moment.min.js", function () {
    });

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
            var typesOfPurchase = [];
            var total = 0;
            //$(".purchase-list").append("<p>");
            for (var i = 0; i < purchases.length; i++) {

                temp = purchases[i];
                //$(".purchase-list").append(moment(purchases[i].Data).toDate() + " - " + purchases[i].Entidade + " - (" + purchases[i].TotalMerc +")");
                /*for (var j = 0; j < temp.length; j++) {
                    $(".purchase-list").append("<br>    " + temp[j].DescArtigo + " - " + temp[j].TotalILiquido);
                }
                $(".purchase-list").append("<br><br>");
            }*/
                //$(".purchase-list").append("</p>");


                if (moment(temp.Data).year() == 2016) {
                    if ($.inArray(temp.TipoDoc, typesOfPurchase) === -1)
                        typesOfPurchase.push(temp.TipoDoc);

                    if (temp.Entidade == "F0002")
                        console.log(temp);
                    if (temp.Entidade == "F0001")
                        purchasesF0001 += Math.abs(temp.TotalMerc) + Math.abs(temp.TotalOutros) - Math.abs(temp.TotalDesc);
                    else if (temp.Entidade == "F0002") {
                        if (temp.tipoDoc !== "COT")
                            purchasesF0002 += Math.abs(temp.TotalMerc) + Math.abs(temp.TotalOutros) - Math.abs(temp.TotalDesc);
                    }
                    else if (temp.Entidade == "F0003")
                        purchasesF0003 += Math.abs(temp.TotalMerc) + Math.abs(temp.TotalOutros) - Math.abs(temp.TotalDesc);
                    else if (temp.Entidade == "F0004")
                        purchasesF0004 += temp.TotalMerc + temp.TotalOutros - temp.TotalDesc + temp.TotalDespesasAdicionais;
                    else if (temp.Entidade == "F0005")
                        purchasesF0005 += Math.abs(temp.TotalMerc) + Math.abs(temp.TotalOutros) - Math.abs(temp.TotalDesc);
                    else if (temp.Entidade == "SER")
                        purchasesSER += Math.abs(temp.TotalMerc) + Math.abs(temp.TotalOutros) - Math.abs(temp.TotalDesc);
                    //console.log(temp.Entidade + ": " + temp.TotalMerc);
                }/* else if (moment(temp.Data).year() == 2016 ){
                    if (temp.Entidade == "F0001")
                        purchasesF0001 -= temp.TotalMerc + temp.TotalIva;
                    else if (temp.Entidade == "F0002")
                        purchasesF0002 -= temp.TotalMerc + temp.TotalIva;
                    else if (temp.Entidade == "F0003")
                        purchasesF0003 -= temp.TotalMerc + temp.TotalIva;
                    else if (temp.Entidade == "F0004")
                        purchasesF0004 -= temp.TotalMerc + temp.TotalIva;
                    else if (temp.Entidade == "F0005")
                        purchasesF0005 -= temp.TotalMerc + temp.TotalIva;
                    else if (temp.Entidade == "SER")
                        purchasesSER -= temp.TotalMerc + temp.TotalIva;
                }*/
            }
            console.log("F0001: " + purchasesF0001);
            console.log("F0002: " + purchasesF0002);
            console.log("F0003: " + purchasesF0003);
            console.log("F0004: " + purchasesF0004);
            console.log("F0005: " + purchasesF0005);
            console.log("SER: " + purchasesSER);
            console.log(typesOfPurchase);
        }
    }).fail(function () {
        alert("ERROR: getting purchases list");
    });


});