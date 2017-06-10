angular.module("Wizcation").controller("HotelDetail", function ($http, $scope, $translate, $stateParams) {


    $http.get("api/moduleHotel/ListHotel/" + $stateParams.ID).then(function (response) {
        $scope.hotel = response.data.hotels;
        $scope.HotelName = $scope.hotel[0].HotelName;

    });

    
    $http.get("api/ModuleHotel/ViewDetailHotel/" + $stateParams.ID + "?Lang=en" + $stateParams.ID).then(function (response) {
        $scope.slides = response.data.slidesImage;
        $scope.roomslist = response.data.roomslist;
        $scope.hotel_detail = response.data.hotel_detail;
        $scope.amentities_mapping = response.data.amentities_mapping;
    }); 


});

