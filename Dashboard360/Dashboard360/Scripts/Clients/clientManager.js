// HTTP Requests


// Get Total Sales for Year 'year'
var totalSales;
var getTotalSales = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/SalesValues/" + year,
        success: function (salesValues) {
            totalSales = salesValues;
            $(".salesTotalThisYear").append(formatPrice(Math.floor(salesValues).toString()) +
" <span style='font-size: 20px!important;'>" + salesValues.toString().split(".")[1].slice(0, 2) + "</span>");
            $(".loadingSalesTotal").hide();
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
};

var getSalesList = function (year) {
    var salesDocs = ["NC", "ND", "VD", "DV", "AVE"];
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/sales/" + year,
        success: function (sales) {
            sales = JSON.parse(sales);
            for (var i = 0; i < sales.length; i++) {
                var temp = sales[i];
                if (temp.TipoDoc.charAt(0) == 'F' || $.inArray(temp.TipoDoc, salesDocs) !== -1) {
                    var liquid = temp.TotalMerc + temp.TotalOutros - temp.TotalDesc;
                    $(".sales-modal-body").append("<tr> <td>" + temp.Entidade + "</td><td>" + moment.utc(temp.Data).format('LLL') + "</td><td style='text-align: right;'>" + formatPrice(liquid.toFixed(2)) + "€ </td></tr>");
                }
            }
        },
        async: false
    }).fail(function () {
        console.log("ERROR: getting total sales value");
    });
}

// Get Total Clients
var totalClients;
var getTotalClients = function () {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/",
        success: function (customers) {
            customers = JSON.parse(customers);
            totalClients = customers.length;
            for (var i = 0; i < customers.length; i++) {
                $(".total-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td> <a href='localhost:49751/Clients/ShowDetails/" + customers[i].CodCliente +  "' target='_blank'>" + customers[i].CodCliente + "</a></td><td>" + customers[i].NomeCliente + "</td><td>");
            }

            $(".loadingNumCustomers").hide();
            $(".numberOfCustomers").append(customers.length);

            var avg = totalSales / totalClients;
            $(".loadingAvgSalePerCustomer").hide();
            $(".avgSalePerCustomer").append(formatPrice(Math.floor(avg).toString()) +
                " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");

        }
    }).fail(function () {
        console.log("ERROR: getting total clients");
    });

}

// Total Active Clients in Year 'year'
var totalActiveClients;
var getTotalActiveClients = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetNumActiveClients/" + year,
        success: function (numActiveCustomers) {
            totalActiveClients = numActiveCustomers;
            $(".loadingActiveCustomers").hide();
            $(".activeClients").append(numActiveCustomers);
            var avg = totalSales / totalActiveClients;
            $(".loadingAvgSalePerActiveCustomer").hide();
            $(".avgSalePerActiveCustomer").append(formatPrice(Math.floor(avg).toString()) +
                " <span style='font-size: 20px!important;'>" + avg.toString().split(".")[1].slice(0, 2) + "</span>");
        }
    }).fail(function () {
        console.log("ERROR: getting total active");
    });
    return;
}

// Get Top 10 products for Year 'year'
var top10Products = function (year) {

    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Products/GetTop10ProductsSold/" + year,
        success: function (topProductsSold) {
            topProductsSold = JSON.parse(topProductsSold);

            for (var i = 0; i < 10; i++) {
                $(".top-products-modal-body").append("<tr><td>" + topProductsSold[i].CodArtigo + "</td><td>" + topProductsSold[i].DescArtigo + "</td><td>" + topProductsSold[i].QuantidadeVendida + "</td><td>" + formatPrice(topProductsSold[i].VolumeVendas.toFixed(2)) + " €</td>");
            }

            var productData = [];
            for (var i = 0; i < 10; i++) {
                var temp = { prod: topProductsSold[i].CodArtigo/*, desc: topProductsSold[i].DescArtigo*/, sale: topProductsSold[i].VolumeVendas.toFixed(2) };
                productData.push(temp);
            }

            productData = shuffle(productData);
            $(".loadingTopProducts").hide()
            Morris.Bar({
                element: 'top-products',
                data: productData,
                xkey: 'prod',
                ykeys: ['sale'],
                labels: ['Value [€]'/*, ' '*/]
            });
        }

    }).fail(function () {
        console.log("ERROR: getting top 10 products sold");
    });
};

// Get Total Expenses in Year 'year'
var typesOfPurchase = ['VNC', 'VND', 'VVD', 'VFA', 'VFG', 'VFI', 'VFM', 'VFO', 'VFP', 'VFR'];
var getTotalExpenses = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/purchases",
        success: function (purchases) {
            var totalExpenses = 0;
            purchases = JSON.parse(purchases);

            for (var i = 0; i < purchases.length; i++) {
                if (moment(purchases[i].Data).year() == year) {
                    if ($.inArray(purchases[i].TipoDoc, typesOfPurchase) !== -1) {
                        console.log(purchases[i].TipoDoc + " - " + purchases[i].TotalMerc);
                        totalExpenses += Math.abs(purchases[i].TotalMerc) + Math.abs(purchases[i].TotalOutros) - Math.abs(purchases[i].TotalDesc);
                    }
                }
            }
            console.log("total expenses: " + totalExpenses);
            var avgExpenseCustomer = Math.abs(totalExpenses) / totalClients;
            $(".loadingAvgExpense").hide();
            $(".avgExpenseCustomer").append(formatPrice(Math.floor(avgExpenseCustomer).toString()) +
               " <span style='font-size: 20px!important;'>" + avgExpenseCustomer.toString().split(".")[1].slice(0, 2) + "</span>");

            totalExpensesFinal = totalExpenses;

        }
    }).fail(function () {
        console.log("ERROR: getting purchases list");
    });
};

// Get Top 10 Clients in Year 'year'
var getTopClients = function (year) {
    $.ajax({
        dataType: "json",
        url: "http://localhost:49751/api/Clients/GetTop10Clients/" + year,
        success: function (topClients) {
            topClients = JSON.parse(topClients);

            for (var i = 0; i < topClients.length; i++) {
                $(".active-clients-modal-body").append("<tr> <td>" + (i + 1) + "</td><td>" + topClients[i].CodCliente + "</td><td>" + topClients[i].NumeroCompras + "</td><td style='text-align:right;'>" + formatPrice(topClients[i].TotalCompras.toFixed(2)) + " €</td>");
            }

            // treat array      
            if (topClients.length > 10) {
                topClients.slice(0, 9);
            }

            // Get total sales and data for chart
            $(".loadingTopClients").hide()
            $("#top-clients").show();

            var graphLabels = [];
            var graphValues = [];
            var graphColors = ["#FF6384", "#36A2EB", "#FFCE56", "#9370DB", "#FF6384", "#36A2EB", "#FFCE56", "#9370DB", "#FF6384", "#36A2EB" ];
            for (var i = 0; i < topClients.length; i++) {
                if (topClients[i].TotalCompras.toFixed(2) > 0) {
                    graphLabels.push(topClients[i].CodCliente);
                    graphValues.push(topClients[i].TotalCompras.toFixed(2));
                }
            }

            graphColors.slice(0, graphValues.length);
            console.log("labels: " + graphLabels);
            console.log("values: " + graphValues);
            console.log("color: " + graphColors);


            var data = {
                labels: graphLabels,
                datasets: [
                    {
                        data: graphValues,
                        backgroundColor: graphColors,
                        hoverBackgroundColor: graphColors
                    }]
            };
            var ctx = $("#top-clients");
            var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: data,
                options: {
                    scales: {

                    }
                }
            });

            
            ////////

            console.log("pintou");
        }
    }).fail(function () {
        console.log("ERROR: getting top 10 clients");
    });
};


// Utilities
$.getScript("/Scripts/moment.min.js", function () {
});


function formatPrice(price) {
    return price.reverse().replace(/((?:\d{2})\d)/g, '$1 ').reverse();
}

String.prototype.reverse = function () {
    return this.split('').reverse().join('');
}

var hideValues = function () {
    $(".avgExpenseCustomer").html("");
    $(".salesTotalThisYear").html("");
    $(".avgSalePerActiveCustomer").html("");
    $(".activeClients").html("");
    $(".avgSalePerCustomer").html("");
    $(".numberOfCustomers").html("");
    $("#top-clients").html("");
    $("#top-products").html("");
    // Clear Modals
    $(".sales-modal-body").html("");
    $(".total-clients-modal-body").html("");
    $(".top-products-modal-body").html("");
    $(".active-clients-modal-body").html("");

}


var showLoading = function () {
    $(".loadingNumCustomers").show();
    $(".loadingAvgSalePerCustomer").show();
    $(".loadingActiveCustomers").show();
    $(".loadingAvgSalePerActiveCustomer").show();
    $(".loadingSalesTotal").show();
    $(".loadingAvgExpense").show();
    $(".loadingTopClients").show();
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

//Controlling Functions

var getValues = function (year) {
    /*  var data = {
          labels: ["January", "February", "March",
                    "April", "May", "June",
                    "July", "Agost", "September",
                    "October", "November", "December"],
          datasets: [
              {
                  fillColor: "rgba(252,233,79,0.5)",
                  strokeColor: "rgba(82,75,25,1)",
                  pointColor: "rgba(166,152,51,1)",
                  pointStrokeColor: "#fff",
                  data: [65, 68, 75,
                          81, 95, 105,
                          130, 120, 105,
                          90, 75, 70]
              }
          ]
      }
  
  
      var options = {
          //Boolean - If we show the scale above the chart data			
          scaleOverlay: false,
  
          //Boolean - If we want to override with a hard coded scale
          scaleOverride: true,
  
          //** Required if scaleOverride is true **
          //Number - The number of steps in a hard coded scale
          scaleSteps: 14,
          //Number - The value jump in the hard coded scale
          scaleStepWidth: 5,
          //Number - The scale starting value
          scaleStartValue: 55,
          //String - Colour of the scale line	
          scaleLineColor: "rgba(20,20,20,.7)",
  
          //Number - Pixel width of the scale line	
          scaleLineWidth: 1,
  
          //Boolean - Whether to show labels on the scale	
          scaleShowLabels: true,
  
          //Interpolated JS string - can access value
          scaleLabel: "<%=value%>",
  
          //String - Scale label font declaration for the scale label
          scaleFontFamily: "'Arial'",
  
          //Number - Scale label font size in pixels	
          scaleFontSize: 12,
  
          //String - Scale label font weight style	
          scaleFontStyle: "normal",
  
          //String - Scale label font colour	
          scaleFontColor: "#666",
  
          ///Boolean - Whether grid lines are shown across the chart
          scaleShowGridLines: true,
  
          //String - Colour of the grid lines
          scaleGridLineColor: "rgba(0,0,0,.3)",
  
          //Number - Width of the grid lines
          scaleGridLineWidth: 1,
  
          //Boolean - Whether the line is curved between points
          bezierCurve: true,
  
          //Boolean - Whether to show a dot for each point
          pointDot: true,
  
          //Number - Radius of each point dot in pixels
          pointDotRadius: 5,
  
          //Number - Pixel width of point dot stroke
          pointDotStrokeWidth: 1,
  
          //Boolean - Whether to show a stroke for datasets
          datasetStroke: true,
  
          //Number - Pixel width of dataset stroke
          datasetStrokeWidth: 2,
  
          //Boolean - Whether to fill the dataset with a colour
          datasetFill: true,
  
          //Boolean - Whether to animate the chart
          animation: false,
  
          //Number - Number of animation steps
          animationSteps: 60,
  
          //String - Animation easing effect
          animationEasing: "easeOutQuart",
  
          //Function - Fires when the animation is complete
          onAnimationComplete: null
      };
  
  
      //Get context with jQuery - using jQuery's .get() method.
      var ctx = $("#myChart").get(0).getContext("2d");
  
  
      new Chart(ctx).Line(data, options);*/

    getSalesList(year);
    getTotalSales(year);
    getTotalClients();
    getTotalActiveClients(year);
    getTotalExpenses(year);
    getTopClients(year);
    top10Products(year);
};

var updateValues = function (newYear) {
    hideValues();
    getValues(newYear);
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
    updateValues(year);
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
    updateValues(year);
});

getValues(parseInt($('#yearTitle').text()));
