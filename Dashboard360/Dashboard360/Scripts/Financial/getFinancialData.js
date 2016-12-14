var getAverageSalesPrice = function (year) {
    console.log("getAverageSalesPrice");

    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);
            var totalSales = 0;
            for (var i = 0; i < sales.length; i++) {
                totalSales += sales[i].TotalMerc;
            }
            $(".averageSalesPrice").append("<p> " + (totalSales / sales.length).toFixed(2) + "€ </p>");
        }
    }).fail(function () {
        alert("ERROR: getting sales list");
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

// Chekc if year is valid
var validYear = function (year) {
    var control = false;
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues/" + year,
        success: function (salesValues) {
            console.log(salesValues);
            if (salesValues != 0) {
                control = true;
            }
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting validYear");
    });
    return control;
};

//Controlling Functions

var getValues = function (year) {
    getAverageSalesPrice(year);
};

var updateValues = function (newYear) {
    hideValues();
    getValues(newYear);
}

$("#previousYear").click(function () {
    var year = parseInt($('#yearTitle').text());
    year--;
    var response = validYear(year);
    console.log("res: " + response);
    if (validYear(year) == false) {
        alert("There are no registers for " + year);
        return;
    }
    hideValues();
    showLoading();
    $("#yearTitle").html(year + " ");
    updateValues(year);
});

var hideValues = function () {
    $(".averageSalesPrice").html("");
    // Clear Modals
    $(".sales-modal-body").html("");
    $(".total-clients-modal-body").html("");
    $(".top-products-modal-body").html("");
    $(".active-clients-modal-body").html("");
    $(".expenses-modal-body").html("");
}

var showLoading = function ()
{
    $(".loadingAverageSalesPrice").show();
}

$("#nextYear").click(function () {
    var year = parseInt($('#yearTitle').text());
    year++;
    var response = validYear(year);
    console.log("res: " + response);
    if (validYear(year) == false) {
        alert("There are no registers for " + year);
        return;
    }
    hideValues();
    showLoading();
    $("#yearTitle").html(year + " ");
    updateValues(year);
});

getValues(parseInt($('#yearTitle').text()));
