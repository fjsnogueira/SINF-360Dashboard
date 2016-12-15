$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTopSuppliers/" + parseInt($('#yearTitle').text()),
        success: function (topSuppliers) {
            topSuppliers = JSON.parse(topSuppliers);
            console.log(topSuppliers);
            // Get Top Suppliers
            var productData = [];
            var len = topSuppliers.length;
            if (len >= 10)
                len = 10;

            for (var i = 0; i < len; i++) {
                var temp = { label: topSuppliers[i].CodCliente, value: Math.abs(topSuppliers[i].TotalCompras.toFixed(2)) };
                productData.push(temp);
            }


            $(".loadingTopClients").hide();
            Morris.Donut({
                element: 'top-suppliers',
                data: productData
            })
        }
    }).fail(function () {
        console.log("ERROR: getting top buyers");
    });
});

