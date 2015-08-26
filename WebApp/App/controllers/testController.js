/*global app*/

app.controller('testController', function ($scope, $http, $rootScope, $location) {
	"use strict";

	if ($rootScope.testId === null) {
		$location.path('/');
	}

    $scope.answered = false;
	$scope.message = "Initializing test engine...";
    $scope.questions = [];
	$scope.currentQuestion = [];
    $scope.questionIndex = 0;
	$scope.correctCount = 0;
	$scope.correctAnswer = false;
	$scope.score = 0;
	$scope.btnValue = "Next";
	$scope.test = [];


    $scope.message = "Loading questions...";
	$http.get('/api/Test/' + $rootScope.testId)
		.success(function (data, status, headers, config) {
			$scope.test = data;
			$scope.questions = data.questions;
			$scope.nextQuestion();
		}).error(function (data, status, headers, config) {
			$scope.message = "Oops... something went wrong";
		});

    $scope.answer = function () {
        return $scope.correctAnswer ? 'Correct' : 'Incorrect';
    };

    $scope.nextQuestion = function () {
		$scope.answered = false;
		$scope.message = "Loading question...";

		if ($scope.questionIndex < $scope.questions.length) {
			$scope.currentQuestion = $scope.questions[$scope.questionIndex];
			$scope.message = "";
			$scope.questionIndex += 1;
			if ($scope.questionIndex === $scope.questions.length) { $scope.btnValue = "Done"; }
		} else {
			$scope.done();
		}
    };

	$scope.sendAnswer = function (option) {
		$scope.answered = true;
		$scope.correctAnswer = false;

		if (option.isCorrect) {
			$scope.correctCount += 1;
			$scope.correctAnswer = true;
		}
	};

	$scope.done = function () {
		$rootScope.score = ($scope.correctCount / $scope.questions.length) * 100;

		$http.post('/api/testscore/', {
			'score': $rootScope.score,
			'studentId': $rootScope.userId,
			'testId': $rootScope.testId
		}).success(function (data, status, headers, config) {
			$location.path('/report');
		}).error(function (data, status, headers, config) {
			$scope.title = "Oops... something went wrong";
		});

	};
});
