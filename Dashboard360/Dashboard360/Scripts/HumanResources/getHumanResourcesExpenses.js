$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetHumanResourcesExpenses/2015",
        success: function (humanResourcesExpenses) {

            humanResourcesExpenses = JSON.parse(humanResourcesExpenses);
            console.log(humanResourcesExpenses);

            var sum = 0;
            d = new Date(-62135596800000);

            for (i = 0; i < humanResourcesExpenses.length; i++) {

                data = new Date(humanResourcesExpenses[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                if (data.getTime() == d.getTime()) {
                    $(".total-human-resources-modal-body").append("<tr> <td>" + humanResourcesExpenses[i].nome + "</td><td>" + (humanResourcesExpenses[i].salario * 12) + "</td><td>" + (humanResourcesExpenses[i].salario * 2) + "</td><td>" + (humanResourcesExpenses[i].subsidioAlim * 21 * 12).toFixed(2) + "</td><td>" + (humanResourcesExpenses[i].salario * 14 * 0.2375).toFixed(2) + "</td><td>");
                    sum = sum + (humanResourcesExpenses[i].salario * 14) + (humanResourcesExpenses[i].subsidioAlim * 21 * 12) + (humanResourcesExpenses[i].salario * 0.2375 * 14);
                }
                else {

                    dataDemissao = new Date(humanResourcesExpenses[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                    var year = parseInt($(".header-year").text());

                    dataInicial = new Date(year, 0, 1, 1, 0, 0, 0);

                    console.log(dataDemissao);
                    console.log(dataInicial);

                    function monthDiff(d1, d2) {
                        var months;
                        months = (d2.getFullYear() - d1.getFullYear()) * 12;
                        months -= d1.getMonth() + 1;
                        months += d2.getMonth();
                        return months <= 0 ? 0 : months;
                    }
                    
                    var ret = monthDiff(dataInicial, dataDemissao);
                    console.log(ret);

                    $(".total-human-resources-modal-body").append("<tr> <td>" + humanResourcesExpenses[i].nome + "</td><td>" + (humanResourcesExpenses[i].salario * ret) + "</td><td>" + 0 + "</td><td>" + (humanResourcesExpenses[i].subsidioAlim * 21 * ret).toFixed(2) + "</td><td>" + (humanResourcesExpenses[i].salario * ret * 0.2375).toFixed(2) + "</td><td>");
                    sum = sum + (humanResourcesExpenses[i].salario * ret) + (humanResourcesExpenses[i].subsidioAlim * 21 * ret) + (humanResourcesExpenses[i].salario * 0.2375 * ret);
                }

                
            }

            $(".loadingSalesTotal").hide();
            $(".humanResourcesExpenses").html("<p>" + sum + "</p>");

        }
    }).fail(function () {
        alert("ERROR: getting human resources expenses!");
    });
});