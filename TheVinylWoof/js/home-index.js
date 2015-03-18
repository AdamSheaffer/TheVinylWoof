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
        .when("/AlbumDetails/:id", {
            controller: "albumDetailsController",
            templateUrl: "/templates/albumDetails.html"
        })
        .otherwise({ redirectTo: "/" });
})

.controller("albumsController", function ($scope, $http) {
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

    $scope.save = function () {
        $http.post
    }
})

.controller("albumDetailsController", function ($scope, $http, $routeParams) {
    console.log($routeParams);
    alert("hit controller");
});