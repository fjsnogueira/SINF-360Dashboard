$(document).ready(function () {
    // Client View
    $("#top-clients").hide();
    $(".loadingSalesTotal").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAvgExpense").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingNumCustomers").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingActiveCustomers").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAvgSalePerCustomer").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAvgSalePerActiveCustomer").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingTopClients").append("<img src='/Content/images/loading.gif' width='403' height='342'>");
    $(".loadingTopProducts").append("<img src='/Content/images/loading.gif' width='342' height='342'>");

    // Client Details
    $("#seasonality").hide();
    $(".loadingSeasonality").append("<img src='/Content/images/loading.gif' width='342' height='342'>");
    $(".loadingSalesTotalThisYear").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAverageSale").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAverageSaleThisYear").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingTotalOrders").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");

    // Product Details
    $(".loadingInventory").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingNumberOrders").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingUnitPrice").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingTopBuyers").append("<img src='/Content/images/loading.gif' width='342' height='342'>");
    
    //Financial
    $(".loadingAverageSalesPrice").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAccountsReceivable").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingAccountsPayable").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
    $(".loadingCashFlow").append("<img src='/Content/images/loading.gif' width='50px' height='50px'>");
});