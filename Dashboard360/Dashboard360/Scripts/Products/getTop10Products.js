$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Products/GetTop10ProductsSold/2016",
        success: function (topProductsSold) {
            topProductsSold = JSON.parse(topProductsSold);
            //console.log(topProductsSold);
            var productInfoHTML;
            $(".topTenProductsSold").append("<p>");
            for (var i = 0; i < 10; i++) {
                productInfoHTML = (i + 1) + " - " + topProductsSold[i].CodArtigo + " - " + /*topProductsSold[i].DescArtigo + " - "*/ +topProductsSold[i].QuantidadeVendida + " - " + topProductsSold[i].VolumeVendas.toFixed(2) + "€ <br>";
                $(".topTenProductsSold").append(productInfoHTML);
            }
            $(".topTenProductsSold").append("</p>");

            var productData = [];
            for (var i = 0; i < 10; i++) {
                var temp = { prod: topProductsSold[i].CodArtigo, sale: topProductsSold[i].VolumeVendas.toFixed(2) };
                productData.push(temp);
            }
            console.log(productData);

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

            Morris.Bar({
                element: 'bar-example',
                data: productData,
                xkey: 'prod',
                ykeys: ['sale'],
                labels: ['Value [€]']
            });

        

        }

    }).fail(function () {
        console.log("ERROR: getting top 10 products sold");
    });


});