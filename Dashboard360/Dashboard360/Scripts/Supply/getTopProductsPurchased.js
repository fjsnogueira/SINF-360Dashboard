$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetTopProductsPurchased",
        success: function (topProductsPurchased) {
            topProductsPurchased = JSON.parse(topProductsPurchased);
            console.log(topProductsPurchased);
            var clientInfoHTML;
            $(".topProductsPurchased").append("<p>");
            for (var i = 0; i < 10; i++) {
                clientInfoHTML = (i + 1) + " - " + topProductsPurchased[i].DescArtigo + " <br>";
                $(".topProductsPurchased").append(clientInfoHTML);
            }
            $(".topProductsPurchased").append("</p>");
        }
    }).fail(function () {
        alert("ERROR: getting top products purchased list");
    });


});