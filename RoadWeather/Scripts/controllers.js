roadWeatherApp.controller('MapController', ['$scope', 'GoogleMapApi'.ns(), 'location', 'getMessage', 'getWeatherMarkers', 'markerFactory',
    function ($scope, GoogleMapApi, location, getMessage, getWeatherMarkers, markerFactory) {

        $scope.dateTime;

        $scope.markers = [];

        $scope.isCollapsed = false;

        $scope.mode = 'DRIVING';
        $scope.changeMode = function (mode) {
            $scope.mode = mode;
        }

        $scope.waypoints = ['', '']

        $scope.addWaypoint = function () {
            $scope.waypoints.push('');
        }

        $scope.removeWaypoint = function (index) {
            $scope.waypoints.splice(index, 1);
        }

        $scope.windowOptions = [];

        $scope.showWindowClick = function (id) {
            $scope.windowOptions[id].visible = !$scope.windowOptions[id].visible;
        };

        $scope.closeWindowClick = function (id) {
            $scope.windowOptions[id].visible = false;
        };


        GoogleMapApi.then(function (maps) {

            $scope.map = {
                control: {},
                center: { latitude: 49.193321, longitude: 16.606113 },
                zoom: 12
            };

            location.getLocation($scope.map, maps, $scope.markers);

            // instantiate google map objects for directions
            var directionsDisplay = new maps.DirectionsRenderer();
            var directionsService = new maps.DirectionsService();
            var geocoder = new maps.Geocoder();

            // get directions using google maps api
            $scope.getDirections = function () {
                $scope.markers = [];

                $scope.loading = true;

                var waypoints = [];
                if ($scope.waypoints.length >= 2) {
                    var array = $scope.waypoints.slice(1, $scope.waypoints.length - 1)
                    for (i in array) {
                        waypoints.push({ location: array[i] });
                    }
                }

                var request = {
                    origin: $scope.waypoints[0],
                    destination: $scope.waypoints[$scope.waypoints.length - 1],
                    waypoints: waypoints,
                    travelMode: maps.TravelMode[$scope.mode]
                };

                directionsService.route(request, function (response, status) {                                        
                    if (status === maps.DirectionsStatus.OK) {
                        directionsDisplay.setDirections(response);
                        directionsDisplay.setMap($scope.map.control.getGMap());

                        if ($scope.dateTime == null) {
                            $scope.dateTime = new Date();
                        }
                        var message = getMessage.parseDirectionResult(response, $scope.dateTime);
                        getWeatherMarkers.getWeatherMarkers(message).success(
                            function (data) {
                                $.each(data, function (index, mark) {
                                    var marker = markerFactory.getMarker(maps, mark, index + 1);

                                    $scope.windowOptions[marker.id] = {
                                        visible: false
                                    };

                                    $scope.markers.push(marker);
                                });
                                $scope.loading = false;
                            });
                    } else if (status === maps.DirectionsStatus.ZERO_RESULTS) {
                        alert('Submitted route is not available');
                        $scope.loading = false;                 
                    } else {
                        alert('Computing route was unsuccesfull');
                        $scope.loading = false;
                    }
                });
            }
        })
    }]);