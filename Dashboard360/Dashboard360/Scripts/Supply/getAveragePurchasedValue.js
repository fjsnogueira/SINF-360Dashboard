

$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetAveragePurchasedValue/" + parseInt($('#yearTitle').text()),
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
            $(".avgExpenseCustomer").append(formatPrice(Math.floor(ret).toString()) +
" <span style='font-size: 20px!important;'>" + ret.toString().split(".")[1].slice(0, 2) + "</span>");
        }
    }).fail(function () {
        alert("ERROR: getting total purchased value!!");
    });


    String.prototype.reverse = function () {
        return this.split('').reverse().join('');
    }

    function formatPrice(price) {
        return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
    }

});