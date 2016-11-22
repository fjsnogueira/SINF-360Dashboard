$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/products",
        success: function (products) {
            products = JSON.parse(products);
            console.log(products);
            for (var i = 0; i < products.length; i++) {
                $(".products-list").append("<p>" + products[i].CodArtigo + " - " + products[i].DescArtigo + "</p>")
            }
        }
    }).fail(function () {
        alert("ERROR: getting products list");
    });


});