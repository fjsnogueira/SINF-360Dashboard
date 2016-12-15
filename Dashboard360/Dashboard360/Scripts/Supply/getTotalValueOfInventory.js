$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTotalValueOfInventory/" + parseInt($('#yearTitle').text()),
        success: function (totalValueOfInventory) {

            totalValueOfInventory = JSON.parse(totalValueOfInventory);
            console.log(totalValueOfInventory);

            sum = 0;

            for (i = 0; i < totalValueOfInventory.length; i++) {
                
                if (totalValueOfInventory[i].stockAtual >= 0) {
                    sum = sum + (totalValueOfInventory[i].stockAtual * totalValueOfInventory[i].PrecoMedio);
                }
            }

            $(".loadingAvgSalePerCustomer").hide();
            $(".totalValueOfInventory").append(formatPrice(Math.floor(sum).toString()) +
" <span style='font-size: 20px!important;'>" + sum.toString().split(".")[1].slice(0, 2) + "</span>");
        }
    }).fail(function () {
        alert("ERROR: getting total value of inventory");
    });


    String.prototype.reverse = function () {
        return this.split('').reverse().join('');
    }

    function formatPrice(price) {
        return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
    }

});