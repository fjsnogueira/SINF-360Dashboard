var getAverageSalesPrice = function (year) {

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
            $(".averageSalesPrice").append("<p> " + (totalSales / sales.length).toFixed(2) + " </p>");
            $(".loadingAverageSalesPrice").hide();
        }
    }).fail(function () {
        alert("ERROR: getting sales list");
        $(".loadingAverageSalesPrice").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

var getAccountsPayable = function (year) {

    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/payables/" + year,
        success: function (pending) {
            pending = JSON.parse(pending);
            if (pending.length == 0) {
                $(".accountsPayable").append("<p>No entries found.</p>");
            }
            else {
                var total = 0;
                for (i in pending) {
                    total += pending[i].PendingValue;
                }
                $(".accountsPayable").append("<p>" + total.toFixed(2) + "</p>");
                $(".loadingAccountsPayable").hide();
            }
        }
    }).fail(function () {
        alert("ERROR: getting accounts payable");
        $(".loadingAccountsPayable").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

var getAccountsReceivable = function (year)
{
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/receivables/" + year,
        success: function (pending) {
            pending = JSON.parse(pending);
            if (pending.length == 0) {
                $(".accountsReceivable").append("<p>No entries found.</p>");
            }
            else {
                var total = 0;
                for (i in pending) {
                    total += pending[i].PendingValue;
                }
                $(".accountsReceivable").append("<p>" + total.toFixed(2) + "</p>");
                $(".loadingAccountsReceivable").hide();
            }
        }
    }).fail(function () {
        alert("ERROR: getting accounts receivable");
        $(".loadingAccountsReceivable").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

var getFlow = function (year) {
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/flow/" + year,
        success: function (flow) {
            flow = JSON.parse(flow);
            if (flow.length == 0) {
                $(".cashFlow").append("<p>No entries found.</p>");
            }
            else {
                var total = 0;
                for (i in flow) {
                    total += flow[i].Mes00;
                    total += flow[i].Mes01 + flow[i].Mes02 + flow[i].Mes03 + flow[i].Mes04 + flow[i].Mes05 + flow[i].Mes06 + flow[i].Mes07
                        + flow[i].Mes08 + flow[i].Mes09 + flow[i].Mes10 + flow[i].Mes11 + flow[i].Mes12 + flow[i].Mes13 + flow[i].Mes14 + flow[i].Mes15;
                }
                $(".cashFlow").append("<p>" + total.toFixed(2) + "</p>");
                $(".loadingCashFlow").hide();
            }
        }
    }).fail(function () {
        alert("ERROR: getting cash flow");
        $(".loadingCashFlow").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

var getSalesRevenueGrowth = function (year) {

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
            $.ajax({
                dataType: "json",
                url: "http://localhost:49751/api/sales/" + (year - 1),
                success: function (sales2) {
                    sales2 = JSON.parse(sales2);
                    var totalSales1 = 0;
                    for (var i = 0; i < sales2.length; i++) {
                        totalSales1 += sales2[i].TotalMerc;
                    }
                    if (totalSales1 == 0)
                        $(".salesRevenueGrowth").append("<p> - </p>");
                    else
                        $(".salesRevenueGrowth").append("<p> " + 100 * (totalSales / totalSales1 - 1).toFixed(2) + "% </p>");
                    $(".loadingSalesRevenueGrowth").hide();
                }
            }).fail(function () {
                alert("ERROR: getting sales list");
                $(".loadingSalesRevenueGrowth").hide();
            });
            setTimeout(function () {
                defer.resolve();
            }, 5000);
        }
    }).fail(function () {
        alert("ERROR: getting sales list");
        $(".loadingSalesRevenueGrowth").hide();
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

var getBalance = function (year) {
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/balance/" + year,
        success: function (balance) {
            balance = JSON.parse(balance);
            if (balance.length == 0) {
                $(".totalAssets").append("<p>No entries found.</p>");
                $(".totalLiabilities").append("<p>No entries found.</p>");
            }
            else {
                var totalAssets = 0;
                var totalLiabilities = 0;
                for (i in balance) {
                    totalLiabilities += balance[i].Mes00CR + balance[i].Mes01CR + balance[i].Mes02CR + balance[i].Mes03CR + balance[i].Mes04CR
                         + balance[i].Mes05CR + balance[i].Mes06CR + balance[i].Mes07CR + balance[i].Mes08CR + balance[i].Mes09CR + balance[i].Mes10CR
                         + balance[i].Mes11CR + balance[i].Mes12CR + balance[i].Mes13CR + balance[i].Mes14CR + balance[i].Mes15CR;
                    totalAssets += balance[i].Mes00DB + balance[i].Mes01DB + balance[i].Mes02DB + balance[i].Mes03DB + balance[i].Mes04DB
                         + balance[i].Mes05DB + balance[i].Mes06DB + balance[i].Mes07DB + balance[i].Mes08DB + balance[i].Mes09DB + balance[i].Mes10DB
                         + balance[i].Mes11DB + balance[i].Mes12DB + balance[i].Mes13DB + balance[i].Mes14DB + balance[i].Mes15DB;
                }
                $(".totalAssets").append("<p>" + totalAssets.toFixed(2) + "</p>");
                $(".totalLiabilities").append("<p>" + totalLiabilities.toFixed(2) + "</p>");
                $(".loadingTotalAssets").hide();
                $(".loadingTotalLiabilities").hide();
            }
        }
    }).fail(function () {
        alert("ERROR: getting cash flow");
        $(".loadingTotalAssets").hide();
        $(".loadingTotalLiabilities").hide();
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
    getAccountsPayable(year);
    getAccountsReceivable(year);
    getFlow(year);
    getBalance(year);
    getSalesRevenueGrowth(year);
};

var updateValues = function (newYear) {
    hideValues();
    getValues(newYear);
}

function checkYear()
{
    if (parseInt($("#yearTitle").text()) == 2016)
        $("#nextYear").hide();
    else
        $("#nextYear").show();

    if (parseInt($("#yearTitle").text()) == 2015)
        $("#previousYear").hide();
    else
        $("#previousYear").show();
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
    checkYear();
});

var hideValues = function () {
    $(".averageSalesPrice").html("");
    $(".accountsPayable").html("");
    $(".accountsReceivable").html("");
    $(".cashFlow").html("");
    $(".totalAssets").html("");
    $(".totalLiabilities").html("");
    $(".salesRevenueGrowth").html("");
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
    $(".loadingAccountsPayable").show();
    $(".loadingAccountsReceivable").show();
    $(".loadingCashFlow").show();
    $(".loadingTotalAssets").show();
    $(".loadingTotalLiabilities").show();
    $(".loadingSalesRevenueGrowth").show();
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
    checkYear();
});

getValues(parseInt($('#yearTitle').text()));

$(document).ready(checkYear);