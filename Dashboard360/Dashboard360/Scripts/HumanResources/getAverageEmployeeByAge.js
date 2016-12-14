$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetAverageEmployeeByAge/2016",
        success: function (averageEmployeeByAge) {
            averageEmployeeByAge = JSON.parse(averageEmployeeByAge);
            console.log(averageEmployeeByAge);

            var sum = 0;

            for (i = 0; i < averageEmployeeByAge.length; i++) {

                d = new Date(averageEmployeeByAge[i].dataNascimento.match(/(?:\-\d*)?\d+/g)[0] * 1);

                var ageDifMs = Date.now() - d.getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch

                sum = sum + Math.abs(ageDate.getUTCFullYear() - 1970);
            }

            totalAverage = Math.round((sum / averageEmployeeByAge.length)*10) /10;
            $(".loadingAge").hide();
            $(".averageEmployeeAge").html("<p>" + totalAverage + "</p>");


            for (var i = 0; i < averageEmployeeByAge.length; i++) {

                var s = 0;
                da = new Date(averageEmployeeByAge[i].dataNascimento.match(/(?:\-\d*)?\d+/g)[0] * 1);

                var ageDifMs = Date.now() - da.getTime();
                var ageDate = new Date(ageDifMs); // miliseconds from epoch

                s = Math.abs(ageDate.getUTCFullYear() - 1970);

                $(".total-name-age-employees-modal-body").append("<tr> <td>" + averageEmployeeByAge[i].nome + "</td><td>" + s + "</td><td>");
            }

        }
    }).fail(function () {
        alert("ERROR: getting average of employee by age");
    });


});