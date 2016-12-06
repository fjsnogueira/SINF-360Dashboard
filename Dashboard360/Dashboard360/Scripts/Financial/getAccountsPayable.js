var getAccountsPayable = function () {
    console.log("getAccountsPayable");

    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/payables",
        success: function (pending) {
            pending = JSON.parse(pending);
            console.log(pending)
            if (pending.length == 0) {
                $(".accountsPayable").append("<p>No entries found.</p>");
            }
            else {
                var total = 0;
                for (i in pending) {
                    console.log(i);
                    total += pending[i].PendingValue;
                }
                $(".accountsPayable").append("<p>Total accounts payable: " + total.toFixed(2) + " €</p>");
           }
        }
    }).fail(function () {
        alert("ERROR: getting accounts payable");
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

getAccountsPayable();