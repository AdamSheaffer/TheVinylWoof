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

    $scope.showPreview = false;

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

    $scope.findCoverArt = function() {
        var artist = $scope.newAlbum.artist;
        var title = $scope.newAlbum.title;
        var url = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=f03d3d46d27b37ed1a48317b3bddcf9a&artist=" + artist + "&album=" + title + "&format=json";
        $http.get(url)
            .then(function (result) {
                //success
                var x = result.data;
                debugger;
                if (x.message === "Artist not found" || x.album.image[3]['#text'] === "") {
                    useDefaultArt();
                }
                else {
                    $scope.newAlbum.coverLocation = x.album.image[4]['#text'];
                    $scope.showPreview = true;
                } 
            },
            function () {
                //error
                debugger;
                useDefaultArt();
            });

        function useDefaultArt() {
            debugger;
            $scope.newAlbum.coverLocation = "../Images/NoPhotoImg.png"
            $scope.showPreview = true;
        }
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
    $scope.relatedAlbums = [];
    $scope.albumCount;
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

    function getRelatedAlbums(userId) {
        $http.get("api/users/" + userId + "/albums")
            .then(function (result) {
                $scope.relatedAlbums = result.data;
                $scope.albumCount = $scope.relatedAlbums.length;
            },
            function () {
                //error
                console.log("problem getting user's other albums");
            });
    }

    $http.get(userUrl)
        .then(function (result) {
            //success
            userData = result.data;
            $scope.user = userData[0];
            getRelatedAlbums($scope.user.id);
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
            var userId = $scope.currentUser[0].id;
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
    $scope.albumsSold;
    $scope.buyer;

    //TODO abstract to auth Factory
    $http.get("api/users/currentUser")
        .then(function (result) {
            //success
            $scope.currentUser = result.data[0];
            console.log(result.data);
            var id = $scope.currentUser.id;
            getAlbumsBought(id);
            getAlbumsGiven(id);
            getAlbumsSold(id);
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

    function getAlbumsSold(userId) {
        $http.get("api/users/" + userId + "/albums?albumSet=sold")
        .then(function (result) {
            //success
            $scope.albumsSold = result.data;
        },
        function () {
            //error
            console.log("error getting albums sold");
        });
    }

    //TODO refactor these similar functions

    function getAlbumsGiven(userId) {
        $http.get("api/users/" + userId + "/albums?albumSet=selling")
        .then(function (result) {
            //success
            $scope.albumsGiven = result.data;
        },
        function () {
            //error
            console.log("getting bought albums failed");
        });
    }

    $scope.getBuyerInfo = function(userId) {
        $http.get("api/users/" + userId)
        .then(function (result) {
            $scope.buyer = result.data[0];
            console.log($scope.buyer);
            debugger;
        },
        function () {
            //error
            alert("failed");
        });
        
    }

});