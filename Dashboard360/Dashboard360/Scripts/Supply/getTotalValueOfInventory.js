$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTotalValueOfInventory",
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
            $(".totalValueOfInventory").html(sum.toFixed(1));
        }
    }).fail(function () {
        alert("ERROR: getting total value of inventory");
    });


});