var roadWeatherApp = angular.module('roadWeatherApp', ['google-maps'.ns(), 'ngAutocomplete', 'ngRoute','ngResource', 'ui.bootstrap'])
    .config(['GoogleMapApiProvider'.ns(), function (GoogleMapApi) {
        GoogleMapApi.configure({
            //    key: 'your api key',
            v: '3.17',
            libraries: 'places, geometry'
        });
    }]);