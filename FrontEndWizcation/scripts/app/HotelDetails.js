angular.module("Wizcation").controller("HotelDetail", function ($http, $scope, $translate, $stateParams) {


    $http.get("api/moduleHotel/ListHotel/" + $stateParams.ID).then(function (response) {
        $scope.hotel = response.data.hotels;
        $scope.HotelName = $scope.hotel[0].HotelName;

    });
});