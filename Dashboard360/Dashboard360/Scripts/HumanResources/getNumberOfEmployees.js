﻿var year = moment().year();


$(function () {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/GetNumberOfEmployees/" + +parseInt($('#yearTitle').text()),
        success: function (numberOfEmployees) {
            
            numberOfEmployees = JSON.parse(numberOfEmployees);
            console.log(numberOfEmployees);

            var totaEmployees = numberOfEmployees.length;

            $(".total-employees-modal-body").empty();
            $(".total-employees-modal-body").append("<strong>" + "Name" + "</strong>");

            for (var i = 0; i < numberOfEmployees.length; i++) {
                
                $(".total-employees-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + numberOfEmployees[i].nome + "</td><td>");
            }

            $(".loadingNumEmployees").hide();
            $(".numberOfEmployees").append(totaEmployees);

        }
    }).fail(function () {
        alert("ERROR: getting number of employees");
    });
});

