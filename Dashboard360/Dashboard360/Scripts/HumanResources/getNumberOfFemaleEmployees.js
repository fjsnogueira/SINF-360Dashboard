var year = moment().year();

$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetNumberOfFemaleEmployees/" + +parseInt($('#yearTitle').text()),
        success: function (numberOfFemaleEmployees) {
            numberOfFemaleEmployees = JSON.parse(numberOfFemaleEmployees);
            console.log(numberOfFemaleEmployees);

            totalFemalesInCompany = [];

            for (var y = 0; y < numberOfFemaleEmployees.length; y++) {
                if (numberOfFemaleEmployees[y].sexo == 1) {
                    totalFemalesInCompany.push(numberOfFemaleEmployees[y]);
                }
            }

            $(".total-female-employees-modal-body").empty();
            $(".total-female-employees-modal-body").append("<th><strong> Name</strong></th>");


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