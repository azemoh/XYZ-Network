/*global angular*/

var app = angular.module('TestApp', ['ngRoute'])
	.run(function ($rootScope, $http) {
		$rootScope.testId = null;
		$rootScope.userId = document.getElementById('userid').value;
		$rootScope.score = null;
	})
    .config(function ($routeProvider, $locationProvider) {
		$routeProvider
			.when('/', {
				templateUrl: '/App/views/index.html',
				controller: 'homeController'
			})
			.when('/rules', {
				templateUrl: '/App/views/rules.html',
				controller: 'rulesController'
			})
			.when('/test', {
				templateUrl: '/App/views/test.html',
				controller: 'testController'
			})
			.when('/report', {
				templateUrl: '/App/views/report.html',
				controller: 'reportController'
			})
			.otherwise({ redirectTo: '/' });

		$locationProvider.html5Mode(true);
	});
