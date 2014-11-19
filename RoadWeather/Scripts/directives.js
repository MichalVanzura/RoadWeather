roadWeatherApp.directive('datetimepicker', function () {
    return {
        restrict: 'A',
        link: function (scope, elem, attrs) {
            $(elem).datetimepicker(scope.$eval(attrs.datetimepicker));
        }
    }
});

roadWeatherApp.directive('viewMarker', function () {
    return {
        restrict: 'AE',
        scope:
                {
                    markers: "=",
                },

        templateUrl: '/Asset/ViewMarker.html'
    }
});