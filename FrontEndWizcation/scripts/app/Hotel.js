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

angular.module("Wizcation").controller("HotelDetail", function ($http, $scope, $translate, $stateParams) {

  
    $http.get("api/moduleHotel/ListHotel/" + $stateParams.ID).then(function (response) {
        $scope.hotel = response.data.hotels;
        $scope.HotelName = $scope.hotel[0].HotelName;
       
    });
});