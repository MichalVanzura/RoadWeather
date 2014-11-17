roadWeatherApp.factory('markerFactory', function () {
    return {
        getMarker: function (maps, mark, id) {
            var image = {
                url: mark.Image.Url,
                size: new maps.Size(50, 50),
                origin: new maps.Point(0, 0),
                anchor: new maps.Point(0, 50)
            };

            var marker = {
                id: id,
                coords: {
                    latitude: mark.Location.Latitude,
                    longitude: mark.Location.Longitude
                },
                icon: image,
                message: {
                    title: mark.Title,
                }

            };

            return marker;
        }
    }
});

roadWeatherApp.factory('location', function () {
    return {
        getLocation: function (map, maps, markers) {
            navigator.geolocation.getCurrentPosition(function (pos) {
                map.control.refresh({ latitude: pos.coords.latitude, longitude: pos.coords.longitude });

                var icon = 'http://maps.google.com/mapfiles/kml/pal3/icon28.png';
                var geomarker = {
                    id: 0,
                    coords: {
                        latitude: pos.coords.latitude,
                        longitude: pos.coords.longitude
                    },
                    icon: icon
                };
                markers.push(geomarker);
            });

        }
    }
});

roadWeatherApp.factory('getMessage', function () {
    return {
        parseDirectionResult: function (directionResponse, dateTime) {
            var myRoute = directionResponse.routes[0];
            var duration = 0;

            var locations = new Array();
            for (var i = 0; i < myRoute.overview_path.length; i++) {
                locations.push(
                    {
                        'Longitude': myRoute.overview_path[i].lng(),
                        'Latitude': myRoute.overview_path[i].lat(),
                    }
                );
            }
            $.each(myRoute.legs, function (index, value) {
                duration += myRoute.legs[index].duration.value;
            });

            var message = {
                "locations": locations,
                "startDateTime": dateTime,
                "duration": duration
            };

            return message;
        }
    }
});

roadWeatherApp.factory('getWeatherMarkers', ['$http',
    function ($http) {
        return {
            getWeatherMarkers: function (message) {
                return $http({
                    method: "post",
                    url: "/api/weather",
                    data: message
                });
            }
        };
    }
]);