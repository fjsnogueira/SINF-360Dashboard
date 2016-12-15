$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTotalPurchasedValue/" + parseInt($('#yearTitle').text()),
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
            sum = Math.abs(sum);
            $(".loadingSalesTotal").hide();

            $(".salesTotalThisYear").append(formatPrice(Math.floor(sum).toString()) +
" <span style='font-size: 20px!important;'>" + sum.toString().split(".")[1].slice(0, 2) + "</span>");
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