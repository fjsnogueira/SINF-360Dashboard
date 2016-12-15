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
    console.log("getAccountsPayable");

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

var getAccountsReceivable = function (year) {
    console.log("getAccountsReceivable");

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

var getFlow = function (year)
{
    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/flow/" + year,
        success: function (flow) {
            flow = JSON.parse(flow);
            console.log(flow);
            if (flow.length == 0) {
                $(".cashFlow").append("<p>No entries found.</p>");
            }
            else {
                var total = 0;
                for (i in flow)
                {
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