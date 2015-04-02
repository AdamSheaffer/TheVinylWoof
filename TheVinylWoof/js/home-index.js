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
        .when("/User/:id", {
            controller: "userController",
            templateUrl: "templates/profile.html"
        })
        .otherwise({ redirectTo: "/" });
})

.factory("albumsFactory", function ($http, $q) {
    var _albums = [];
    var _isInit = false;
    
    var _isReady = function () {
        return _isInit;
    }

    var _getForGrabsAlbums = function () {

        var deferred = $q.defer();

        $http.get("/api/albums")
        .then(function (result) {
            //success
            angular.copy(result.data, _albums);
            _isInit = true;
            deferred.resolve();
        },
        function () {
            deferred.reject();
        });

        return deferred.promise;
    }

    var _addNewRecord = function (newAlbum) {

        var deferred = $q.defer();

        $http.post("api/albums", newAlbum)
            .then(function (result) {
                //Success
                var newlyAddedAlbum = result.data;
                _albums.splice(0, 0, newlyAddedAlbum);
                deferred.resolve(newlyAddedAlbum);
            },
            function () {
                //error
                deferred.reject();
            });

        return deferred.promise;
    }

    return {
        albums: _albums,
        getForGrabsAlbums: _getForGrabsAlbums,
        addNewRecord: _addNewRecord,
        isReady: _isReady
    };
})

.controller("albumsController", function ($scope, albumsFactory, $http) {
    $scope.searchParams = { title: "", genre: "" };
    $scope.data = albumsFactory;

    if(albumsFactory.isReady() === false) {
        albumsFactory.getForGrabsAlbums()
            .then(function () {
                //Success
            },
            function () {
                //Error
                console.log("problem getting albums");
            });
    }

    $scope.searchAlbums = function () {
        $http.get("api/albums?genre=" + $scope.searchParams.genre + "&searchString=" + $scope.searchParams.title)
            .then(function (result) {
                //success
                $scope.data.albums = result.data;
            }),
            function () {
                console.log("error searching albums");
            }
    };

})

.controller("newAlbumController", function ($scope, $http, $location, albumsFactory) {
    $scope.newAlbum = {};

    $scope.showPreview = false;

    $scope.save = function () {
        albumsFactory.addNewRecord($scope.newAlbum)
            .then(function () {
                //Success
                $location.path('/');
            }, function () {
                //Error
                console.log("error adding album")
            })
    }

    $scope.findCoverArt = function () {
        var artist = $scope.newAlbum.artist;
        var title = $scope.newAlbum.title;
        var url = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=f03d3d46d27b37ed1a48317b3bddcf9a&artist=" + artist + "&album=" + title + "&format=json";
        $http.get(url)
            .then(function (result) {
                //success
                var x = result.data;
                if (x.message === "Artist not found" || x.message === "Album not found" || x.album.image[3]['#text'] === "") {
                    useDefaultArt();
                }
                else {
                    $scope.newAlbum.coverLocation = x.album.image[4]['#text'];
                    $scope.showPreview = true;
                }
            },
            function () {
                //error
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
    $scope.notEnoughWoofie = false;

    $scope.swap = function () {
        $http.post(albumUrl, albumId)
            .then(function (result) {
                //success
                $scope.swapSuccess = true;
            },
            function () {
                //error
                $scope.notEnoughWoofie = true;
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
            debugger;
            $scope.user = userData;
            getRelatedAlbums($scope.user.id);
        });

    $http.get(albumUrl)
        .then(function (result) {
            //success
            albumData = result.data;
            $scope.album = albumData[0];
        });

    $http.get("api/users/currentuser")
        .then(function (result) {
            //success
            $scope.currentUser = result.data;
            var userId = $scope.currentUser.id;
        },
        function () {
            //error
            console.log("getting current user failed");
        });
})

.controller("dashboardController", function ($scope, $http, $window) {
    $scope.currentUser;
    $scope.albumsBought;
    $scope.albumsGiven;
    $scope.albumsSold;
    $scope.buyer;

    //TODO abstract to auth Factory
    $http.get("api/users/currentuser")
        .then(function (result) {
            //success
            $scope.currentUser = result.data;
            debugger;
            if (!$scope.currentUser.id) {
                $window.location.href = "Account/Login";
            }
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

    $scope.getBuyerInfo = function (userId) {
        $http.get("api/users/" + userId)
        .then(function (result) {

            $scope.buyer = result.data[0];
        },
        function () {
            //error
            alert("failed");
        });
    }

})

.controller("userController", function ($scope, $http, $routeParams) {

    var userId = $routeParams.id;

    $scope.albumsGiven;
    $scope.user;
    $scope.rating = { userId: userId };
    $scope.ratingSubmitted = false;

    $scope.submitRating = function () {
        var url = "api/users/" + userId;
        $http.post(url, $scope.rating)
            .then(function () {
                $scope.ratingSubmitted = true;
            },
            function () {
                alert("problem submitting rating");
            })
    }

    $http.get('api/users/' + userId + '/albums')
        .then(function (result) {
            //success
            $scope.albumsGiven = result.data;
        },
        function () {
            //error
            alert("problem getting user albums");
        });

    $http.get('api/users/' + userId)
        .then(function (result) {
            //success
            $scope.user = result.data[0];
        },
        function () {
            //error
            alert("problem getting user profile");
        });

});