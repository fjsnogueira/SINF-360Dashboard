$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetAveragePurchasedValue",
        success: function (averagePurchasedValue) {

            averagePurchasedValue = JSON.parse(averagePurchasedValue);
            console.log(averagePurchasedValue);

            var typesOfPurchase = ['VNC', 'VND', 'VVD', 'VFA', 'VFG', 'VFI', 'VFM', 'VFO', 'VFP', 'VFR'];

            var sum = 0;

            for (i = 0; i < averagePurchasedValue.length; i++) {
                if ($.inArray(averagePurchasedValue[i].TipoDoc, typesOfPurchase) !== -1) {
                    sum = sum + (averagePurchasedValue[i].TotalMerc + averagePurchasedValue[i].TotalOutros - averagePurchasedValue[i].TotalDesc);
                }
            }

            var ret = Math.abs(sum) / averagePurchasedValue.length;


            $(".loadingAvgExpense").hide();
            $(".avgExpenseCustomer").append(ret.toFixed(2));
        }
    }).fail(function () {
        alert("ERROR: getting total purchased value!!");
    });


});