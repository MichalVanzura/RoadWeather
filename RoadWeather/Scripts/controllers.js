roadWeatherApp.controller('MapController', ['$scope', 'GoogleMapApi'.ns(), 'location', 'getMessage', 'getWeatherMarkers', 'markerFactory',
    function ($scope, GoogleMapApi, location, getMessage, getWeatherMarkers, markerFactory) {

        $scope.dateTime;

        if ($scope.dateTime != null)
        {
            $scope.label = $scope.dateTime.toLocaleString();
        }
        else
        {
            $scope.label = new Date().toLocaleString();
        }
        

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


        $scope.showDate = function () {
            $scope.label = $scope.dateTime.toLocaleString();
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
                    }
                    else if (!request.origin || !request.destination) {
                        alert('Please fill in the route origin and destination');
                        $scope.loading = false;
                    }
                    else if (status === maps.DirectionsStatus.ZERO_RESULTS) {
                            alert('Submitted route is not available. Please note some travel modes might not be available in select countries/regions');
                            $scope.loading = false;
                    }
                    else if (status === maps.DirectionsStatus.NOT_FOUND) {
                        alert('Some of the submitted destinations could not be found. Please check the input values.');
                        $scope.loading = false;
                    }
                    else if (status === maps.DirectionsStatus.MAX_WAYPOINTS_EXCEEDED) {
                        alert('Too many waypoints were submitted. Please try to use at most 8 plus origin and destination values.');
                        $scope.loading = false;
                    }
                    else if (status === maps.DirectionsStatus.INVALID_REQUEST) {
                        alert('Request was invalid. Please check the input values.');
                        $scope.loading = false;
                    }
                    else if (status === maps.DirectionsStatus.OVER_QUERY_LIMIT) {
                        alert('Request could not be processed. Please hold on a moment and try again.');
                        $scope.loading = false;
                    }
                    else if (status === maps.DirectionsStatus.REQUEST_DENIED) {
                        alert('Request could not be processed.');
                        $scope.loading = false;
                    }
                    else {
                        alert('Computing route was unsuccesful. Please hold on a moment and try again.');
                        $scope.loading = false;
                    }
                });
            }
        })
    }]);