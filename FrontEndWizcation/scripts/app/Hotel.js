angular.module("Wizcation").controller("Hotel", function ($http, $scope, $translate) {
    $scope.header = "";
    
    $http.get("api/moduleHotel/ListHotelTop2/0").then(function (response) {
        $scope.header = response.data;

        $scope.ListHotel = $scope.header.hotels;
    });
    $http.get("api/moduleHotel/ListHotel/0").then(function (response) {
        $scope.acomodationtype = response.data.acomodationtype;
    });
});
