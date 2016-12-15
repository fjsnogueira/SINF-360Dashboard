var getAccountsReceivable = function () {

    var defer = $.Deferred();

    $.getScript("Scripts/moment.min.js");

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/receivables",
        success: function (pending) {
            pending = JSON.parse(pending);
            console.log(pending);
            if (pending.length == 0) {
                $(".accountsReceivable").append("<p>No entries found.</p>");
            }
            else {
                var total = 0;
                for (i in pending) {
                    total += pending[i].PendingValue;
                }
                $(".accountsReceivable").append("<p>Total accounts receivable: " + total.toFixed(2) + " €</p>");
            }

        }
    }).fail(function () {
        alert("ERROR: getting accounts receivable");
    });
    setTimeout(function () {
        defer.resolve();
    }, 5000);
    return defer;
};

getAccountsReceivable();