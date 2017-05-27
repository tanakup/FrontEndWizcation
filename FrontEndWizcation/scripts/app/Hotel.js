angular.module("Wizcation").controller("Hotel", function ($http, $scope, $translate) {
    $scope.header = "";
    
    $http.get("api/moduleHotel/ListHotelTop2/0").then(function (response) {
        $scope.header = response.data;
        $scope.ListDeals= $scope.header.hotels;
    });
    $http.get("api/moduleHotel/ListHotel/0").then(function (response) {
        $scope.ListHotel = response.data.hotels;
        $scope.totalItems = $scope.ListHotel.length;
    });

    $http.get(" api/ModuleHotel/ListAcomodation").then(function (response) {
        $scope.acomodationtype = response.data.acomodationtype;
        $scope.amentities = response.data.amentities;
        $scope.facilities = response.data.facilities;
    });
   
    $scope.viewby = 9;
   
    $scope.currentPage = 1;
    $scope.itemsPerPage = $scope.viewby;
   // $scope.maxSize = 0; //Number of pager buttons to show

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };

    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1; //reset to first paghe
    }
    
    $scope.lookup = 1;
});
