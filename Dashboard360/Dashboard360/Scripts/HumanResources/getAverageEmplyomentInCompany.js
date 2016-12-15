$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetAverageEmplyomentInCompany",
        success: function (averageEmplyomentInCompany) {

            averageEmplyomentInCompany = JSON.parse(averageEmplyomentInCompany);
            
            var sum = 0;

            $(".total-time-employment-modal-body").empty();
            $(".total-time-employment-modal-body").append("<th><strong>Name</strong></th> <th><strong>Years of work</strong></th>");

            for (i = 0; i < averageEmplyomentInCompany.length; i++) {

                d = new Date(-62135596800000);
                data = new Date(averageEmplyomentInCompany[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);
                
                if (data.getTime() == d.getTime()) {

                    var s = 0;
                    da = new Date(averageEmplyomentInCompany[i].dataAdmissao.match(/(?:\-\d*)?\d+/g)[0] * 1);
                    var ageDifMs = Date.now() - da.getTime();
                    var ageDate = new Date(ageDifMs); // miliseconds from epoch
                    s = Math.abs(ageDate.getUTCFullYear() - 1970);

                    $(".total-time-employment-modal-body").append("<tr> <td>" + averageEmplyomentInCompany[i].nome + "</td><td>" + s + "</td><td>");

                    sum = sum + s;
                }
                else{
                    dataD = new Date(averageEmplyomentInCompany[i].dataDemissao.match(/(?:\-\d*)?\d+/g)[0] * 1);
                    dataA = new Date(averageEmplyomentInCompany[i].dataAdmissao.match(/(?:\-\d*)?\d+/g)[0] * 1);

                    var diff = Math.floor(dataD.getTime() - dataA.getTime());
                    var day = 1000 * 60 * 60 * 24;

                    var days = Math.floor(diff / day);
                    var months = Math.floor(days / 31);
                    var years = Math.floor(months / 12);

                    $(".total-time-employment-modal-body").append("<tr> <td>" + averageEmplyomentInCompany[i].nome + "</td><td>" + years + "</td><td>");

                    sum = sum + years;
                }
               
                
            }

            var final = Math.round((sum / averageEmplyomentInCompany.length)*10)/10;
            

            $(".loadingEmploymentTime").hide();
            $(".averageEmplTimeComp").append(formatPrice(Math.floor(final).toString()) + " <span style='font-size: 20px!important;'>" + final.toString().split(".")[1].slice(0, 2) + "</span>");

        }
    }).fail(function () {
        alert("ERROR: getting average employees times in company");
    });
});

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}
