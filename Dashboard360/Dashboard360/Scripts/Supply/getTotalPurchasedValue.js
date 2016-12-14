$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTotalPurchasedValue",
        success: function (totalPurchasedValue) {

            totalPurchasedValue = JSON.parse(totalPurchasedValue);
            console.log(totalPurchasedValue);

            var typesOfPurchase = ['VNC', 'VND', 'VVD', 'VFA', 'VFG', 'VFI', 'VFM', 'VFO', 'VFP', 'VFR'];

            var sum = 0;

            for (i = 0; i < totalPurchasedValue.length; i++) {
                if ($.inArray(totalPurchasedValue[i].TipoDoc, typesOfPurchase) !== -1) {
                    sum = sum + (totalPurchasedValue[i].TotalMerc + totalPurchasedValue[i].TotalOutros - totalPurchasedValue[i].TotalDesc);
                }
            }

            $(".loadingSalesTotal").hide();
            $(".salesTotalThisYear").append(Math.abs(sum.toFixed(2)));
        }
    }).fail(function () {
        alert("ERROR: getting total purchased value!!");
    });


});