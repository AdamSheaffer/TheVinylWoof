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
        .when("/Dashboard", {
            controller: "dashboardController",
            templateUrl: "templates/dashboard.html"
        })
        .otherwise({ redirectTo: "/" });
})

.controller("albumsController", function ($scope, $http) {
    $scope.searchParams = { title: "", genre: "" };
    $scope.data = [];

    $http.get("/api/albums")
        .then(function (result) {
            //success
            console.log(result);
            angular.copy(result.data, $scope.data);
        },
        function () {
            alert("error")
        });

    $scope.searchAlbums = function () {
        $http.get("api/albums?genre=" + $scope.searchParams.genre + "&searchString=" + $scope.searchParams.title)
            .then(function (result) {
                //success
                $scope.data = result.data;
            }),
            function () {
                alert("error searching albums");
            }
    }

})

.controller("newAlbumController", function ($scope, $http, $location) {
    $scope.newAlbum = {};

    //TODO add functionality to get album art

    $scope.save = function () {
        $http.post("api/albums", $scope.newAlbum)
            .then(function (result) {
                //Success
                var newAlbum = result.data;
                console.log(newAlbum);
                $location.path("/");
            },
            function () {
                //error
                alert("can't save album");
            });
    }
})

.controller("albumDetailsController", function ($scope, $http, $routeParams, $location) {
    var albumId = $routeParams.id;
    console.log(albumId);

    var userData;
    var albumData;
    var userUrl = "/api/albums/" + albumId + "/user";
    var albumUrl = "/api/albums/" + albumId;
    $scope.swapSuccess = false;
    $scope.user;
    $scope.album;
    $scope.currentUser;
    $scope.swap = function () {
        $http.post(albumUrl, albumId)
            .then(function (result) {
                //success
                $scope.swapSuccess = true;
            },
            function () {
                //error
                alert("There was a problem swapping album");
            });
    }

    $http.get(userUrl)
        .then(function (result) {
            //success
            userData = result.data;
            $scope.user = userData[0];
            console.log($scope.user);
        });

    $http.get(albumUrl)
        .then(function (result) {
            //success
            albumData = result.data;
            $scope.album = albumData[0];
            console.log($scope.album);
        });

    $http.get("api/users/currentUser")
        .then(function (result) {
            //success
            $scope.currentUser = result.data;
            console.log(result.data);
            debugger;
        },
        function () {
            //error
            console.log("getting current user failed");
        });
})

.controller("dashboardController", function ($scope, $http) {
    $scope.currentUser;
    $scope.albumsBought;
    $scope.albumsGiven;

    //TODO abstract to auth Factory
    $http.get("api/users/currentUser")
        .then(function (result) {
            //success
            $scope.currentUser = result.data[0];
            console.log(result.data);
            var id = $scope.currentUser.id;
            getAlbumsBought(id);
            getAlbumsGiven(id);
        },
        function () {
            //error
            console.log("getting current user failed");
        });
    
    function getAlbumsBought(userId) {
        $http.get("api/users/" + userId + "/albums?albumSet=bought")
        .then(function (result) {
            //success
            $scope.albumsBought = result.data;
        },
        function () {
            //error
            console.log("getting bought albums failed");
        });
    }

    //TODO refactor these similar functions

    function getAlbumsGiven(userId) {
        $http.get("api/users/" + userId + "/albums?albumSet=sold")
        .then(function (result) {
            //success
            $scope.albumsGiven = result.data;
            debugger;
        },
        function () {
            //error
            console.log("getting bought albums failed");
        });
    }

});