angular.module("Wizcation").controller("Hotel", function ($http, $scope) {
    $scope.header = "kup";

    $http.get("api/moduleHotel/ListHotel/0").then(function (response) {
        $scope.header = response.data;

        $scope.ListHotel = $scope.header.hotels;

        console.log(response.data);

    });
});