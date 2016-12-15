// Utilities
$.getScript("/Scripts/moment.min.js", function () {
});


String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}



var hideValues = function () {

    $(".numberOfEmployees").html("");
    $(".humanResourcesExpenses").html("");
    $(".revenuePerEmployee").html("");
    $(".costumersPerEmployee").html("");
    $(".averageEmplTimeComp").html("");
    $(".numberOfFemaleEmployees").html("");
    $(".averageEmployeeAge").html("");
 
}


var showLoading = function () {
    $(".loadingNumEmployees").show();
    $(".loadingSalesTotal").show();
    $(".loadingRevEmployee").show();
    $(".loadingCustomersEmployee").show();
    $(".loadingEmploymentTime").show();
    $(".loadingFemale").show();
    $(".loadingAge").show();
    $(".loadingTopProducts").show();
}

function shuffle(array) {
    var currentIndex = array.length, temporaryValue, randomIndex;

    // While there remain elements to shuffle...
    while (0 !== currentIndex) {

        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;

        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
    }
    return array;
}


// Chekc if year is valid
var validYear = function (year) {
    var control = false;
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues/" + year,
        success: function (salesValues) {
            console.log(salesValues);
            if (salesValues != 0) {
                control = true;
            }
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting validYear");
    });
    return control;

};

var updateValues = function () {
    hideValues();
    getValues();
}

$("#previousYear").click(function () {
    var year = parseInt($('#yearTitle').text());
    year--;
    var response = validYear(year);
    console.log("res: " + response);
    if (validYear(year) == false) {
        alert("There are no registers for " + year);
        return;
    }
    hideValues();
    showLoading();
    $("#yearTitle").html(year + " ");
    updateValues();
    checkYear();
});

$("#nextYear").click(function () {
    var year = parseInt($('#yearTitle').text());
    year++;
    var response = validYear(year);
    console.log("res: " + response);
    if (validYear(year) == false) {
        alert("There are no registers for " + year);
        return;
    }
    hideValues();
    showLoading();
    $("#yearTitle").html(year + " ");
    updateValues();
    checkYear();
});

var getValues = function () {

    $.getScript("/Scripts/HumanResources/getNumberOfEmployees.js");
    $.getScript("/Scripts/HumanResources/getNumberOfFemaleEmployees.js");
    $.getScript("/Scripts/HumanResources/getAverageEmployeeByAge.js");
    $.getScript("/Scripts/HumanResources/getHumanResourcesExpenses.js");
    $.getScript("/Scripts/HumanResources/getCustomersPerEmployees.js");
    $.getScript("/Scripts/HumanResources/getAverageEmplyomentInCompany.js");
    $.getScript("/Scripts/HumanResources/getRevenuePerEmployee.js");

};

function checkYear() {

    var currentYear = parseInt($("#yearTitle").text());

    if (currentYear == 2016) {
        $("#nextYear").hide();
        $("#previousYear").show();
    } else if (currentYear == 2015) {
        $("#previousYear").hide();
        $("#nextYear").show();
    }

};

$(document).ready(function () {
    checkYear();
});











