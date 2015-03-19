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

    .factory("dataService", function ($http, $q) {

        var _albums = [];

        var _getAlbums = function () {

            var deferred = $q.defer();

            $http.get("/api/albums")
                .then(function (result) {
                    //success
                    angular.copy(result.data, _albums);
                    deferred.resolve();
                },
                function () {
                    //Error
                    deferred.reject();
                })

            return deferred.promise;
        }

        return {
            albums: _albums,
            getAlbums: _getAlbums
        };
    })

.controller("albumsController", function ($scope, $http, dataService) {
    $scope.data = dataService;

    dataService.getAlbums()
        .then(function () {
            //Success
        },
        function () {
            //error
            alert("error loading albums");
        })

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
                $location.path('/');
            },
            function () {
                //error
                alert("can't save album");
            });
    }
})

.controller("albumDetailsController", function ($scope, $http, $routeParams) {
    var albumId = $routeParams.id;
    console.log(albumId);

   var userData;
    var albumData;
    $scope.user;
    $scope.album;

    var userUrl = "/api/albums/" + albumId + "/user";
    var albumUrl = "/api/albums/" + albumId;

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
        
});