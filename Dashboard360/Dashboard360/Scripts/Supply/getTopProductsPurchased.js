// Formatting price
function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}
String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTopProductsPurchased",
        success: function (topProductsPurchased) {

            topProductsPurchased = JSON.parse(topProductsPurchased);

            console.log(topProductsPurchased);

            for (var i = 0; i < 10; i++) {
                console.log(topProductsPurchased[i].VolumeVendas.toFixed(2));
                $(".top-products-modal-body").append("<tr> <td>" + topProductsPurchased[i].CodArtigo + "</td><td>" + topProductsPurchased[i].DescArtigo + "</td><td>" + topProductsPurchased[i].QuantidadeVendida + "</td><td>" + formatPrice(topProductsPurchased[i].VolumeVendas.toFixed(2)) + " €</td>");
            }

            var productData = [];
            for (var i = 0; i < 10; i++) {
                var temp = { prod: topProductsPurchased[i].CodArtigo, sale: topProductsPurchased[i].VolumeVendas.toFixed(2) };
                productData.push(temp);
            }

            function shuffle(array) {
                var currentIndex = array.length, temporaryValue, randomIndex;

                // While there remain elements to shuffle...
                while (0 !== currentIndex) {

                    // Pick a remaining element...
                    randomIndex = Math.floor(Math.random() * currentIndex);
                    currentIndex -= 1;

                    // And swap it with the current element.
                    temporaryValue = array[currentIndex];
                    array[currentIndex] = array[randomIndex];
                    array[randomIndex] = temporaryValue;
                }

                return array;
            }

            productData = shuffle(productData);
            $(".loadingTopProducts").remove()
            Morris.Bar({
                element: 'bar-example',
                data: productData,
                xkey: 'prod',
                ykeys: ['sale'],
                labels: ['Value [€]']
            });



        }

    }).fail(function () {
        console.log("ERROR: getting top 10 products purchased");
    });


});