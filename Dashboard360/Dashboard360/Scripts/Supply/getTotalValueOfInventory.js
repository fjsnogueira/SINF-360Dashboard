$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTotalValueOfInventory",
        success: function (totalValueOfInventory) {
            totalValueOfInventory = JSON.parse(totalValueOfInventory);
            console.log(totalValueOfInventory);
            $(".totalValueOfInventory").html("<p>" + totalValueOfInventory[0].Valor + "€ </p>" );
        }
    }).fail(function () {
        alert("ERROR: getting total value of inventory");
    });


});