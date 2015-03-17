//home-index.js
angular.module("VinylWoofApp", ["ngRoute"])

.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            controller: "albumsController",
            templateUrl: "/templates/homeView.html"
        })
        .when("/AddRecord", {
            controller: "newAlbumController",
            templateUrl: "/templates/newRecord.html"
        })
        .otherwise({ redirectTo: "/" });
})

.controller("albumsController", function ($scope, $http) {
    $scope.name = "Adam Sheaffer";
    $scope.dataCount = 0;
    $scope.data = [];

    $http.get("/api/albums")
        .then(function (result) {
            //success
            console.log(result);
            angular.copy(result.data, $scope.data);
        },
        function () {
            alert("error")
        })

})

.controller("newAlbumController", function ($scope, $http) {
    $scope.newAlbum = {};
});