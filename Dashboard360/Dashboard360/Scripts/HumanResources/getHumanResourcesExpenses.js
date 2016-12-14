$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetHumanResourcesExpenses/2016",
        success: function (humanResourcesExpenses) {

            humanResourcesExpenses = JSON.parse(humanResourcesExpenses);
            console.log(humanResourcesExpenses);

            var sum = 0;

            for (i = 0; i < humanResourcesExpenses.length; i++) {

                $(".total-human-resources-modal-body").append("<tr> <td>" + humanResourcesExpenses[i].nome + "</td><td>" + (humanResourcesExpenses[i].salario * 12) + "</td><td>" + (humanResourcesExpenses[i].salario * 2) + "</td><td>" + (humanResourcesExpenses[i].subsidioAlim * 21 * 12).toFixed(2) + "</td><td>" + (humanResourcesExpenses[i].salario * 14 * 0.2375).toFixed(2) + "</td><td>");
                
                sum = sum + (humanResourcesExpenses[i].salario * 14) + (humanResourcesExpenses[i].subsidioAlim * 21 * 12) + (humanResourcesExpenses[i].salario * 0.2375 * 14);
            }

            $(".loadingSalesTotal").hide();
            $(".humanResourcesExpenses").html("<p>" + sum + "</p>");

        }
    }).fail(function () {
        alert("ERROR: getting human resources expenses!");
    });
});