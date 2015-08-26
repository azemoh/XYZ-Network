/*global app*/
app.controller('homeController', function ($scope, $http, $rootScope, $location) {
	"use strict";

	$http.get('/api/Test/').success(function (data, status, headers, config) {
		$scope.tests = data;
	}).error(function (data, status, headers, config) {
		$scope.message = "Oops... something went wrong";
	});

	$scope.taketest = function (test) {
		$rootScope.testId = test.testId;
		$location.path('/rules');
	};
});

app.controller('rulesController', function ($scope, $rootScope, $http, $location) {
	"use strict";
	if ($rootScope.testId === null) {
		$location.path('/');
	}

	$scope.test = [];

	$http.get('/api/Test/' + $rootScope.testId).success(function (data, status, headers, config) {
		$scope.test = data;
	}).error(function (data, status, headers, config) {
		$scope.message = "Oops... something went wrong";
	});

	$scope.begin = function () {
		$location.path('/test');
	};
});

app.controller('reportController', function ($scope, $rootScope, $http, $location) {
	"use strict";
	if ($rootScope.testId === null) {
		$location.path('/');
	}
	$scope.score = $rootScope.score;
});
