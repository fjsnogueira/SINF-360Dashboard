$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetNumberOfFemaleEmployees/2016",
        success: function (numberOfFemaleEmployees) {
            numberOfFemaleEmployees = JSON.parse(numberOfFemaleEmployees);
            console.log(numberOfFemaleEmployees);

            totalFemalesInCompany = [];

            for (var y = 0; y < numberOfFemaleEmployees.length; y++) {
                if (numberOfFemaleEmployees[y].sexo == 1) {
                    totalFemalesInCompany.push(numberOfFemaleEmployees[y]);
                }
            }

         


            for (var i = 0; i < totalFemalesInCompany.length; i++) {
                $(".total-female-employees-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + totalFemalesInCompany[i].nome + "</td><td>");
            }

            $(".loadingFemale").hide();
            $(".numberOfFemaleEmployees").append(totalFemalesInCompany.length);





        }
    }).fail(function () {
        alert("ERROR: getting number of female employees");
    });


});