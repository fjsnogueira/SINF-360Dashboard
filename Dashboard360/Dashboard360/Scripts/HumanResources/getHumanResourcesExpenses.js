var year = moment().year();

$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetHumanResourcesExpenses/" + +parseInt($('#yearTitle').text()),
        success: function (humanResourcesExpenses) {

            humanResourcesExpenses = JSON.parse(humanResourcesExpenses);
            console.log(humanResourcesExpenses);

            var sum = 0;
            d = new Date(-62135596800000);

            $(".total-human-resources-modal-body").empty();
            $(".total-human-resources-modal-body").append("<th><strong>" +
                "Name" + "</strong></th>" +
                "<th><strong>" +
                "Annual Base Salary (€)" +
                "</strong></th> <th><strong>" +
                "Annual Holiday and Christmas Subsidy (€)" + "</strong></th> <th><strong>" +
                "Annual Food Allowance (€) </strong></th> <th><strong>" + "Annual Social Security Expenses With Employee (€) </strong></th>");

            for (i = 0; i < humanResourcesExpenses.length; i++) {

                data = new Date(humanResourcesExpenses[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                if (data.getTime() == d.getTime()) {
                    $(".total-human-resources-modal-body").append("<tr> <td>" + humanResourcesExpenses[i].nome + "</td><td>" + (humanResourcesExpenses[i].salario * 12) + "</td><td>" + (humanResourcesExpenses[i].salario * 2) + "</td><td>" + (humanResourcesExpenses[i].subsidioAlim * 21 * 12).toFixed(2) + "</td><td>" + (humanResourcesExpenses[i].salario * 14 * 0.2375).toFixed(2) + "</td><td>");
                    sum = sum + (humanResourcesExpenses[i].salario * 14) + (humanResourcesExpenses[i].subsidioAlim * 21 * 12) + (humanResourcesExpenses[i].salario * 0.2375 * 14);
                }
                else {

                    dataDemissao = new Date(humanResourcesExpenses[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                    dataInicial = new Date(parseInt($('#yearTitle').text()), 0, 1, 1, 0, 0, 0);

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
            $(".humanResourcesExpenses").append(formatPrice(Math.floor(sum).toString()) + " <span style='font-size: 20px!important;'>" + sum.toString().split(".")[1].slice(0, 2) + "</span>");

        }
    }).fail(function () {
        alert("ERROR: getting human resources expenses!");
    });
});


function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}

