(function() {
    'use strict';

    angular
        .module('app', [
            'ngRoute',
            'ngAnimate',        // animations
            'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)
            'ui.bootstrap',
            'ui.router',
            'angular-click-outside',
            'LocalStorageModule',
            'toastr',

            'common',           // common functions, logger, spinner
            'common.bootstrap'  // bootstrap dialog wrapper functions
            
        ])
        .config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('');

            $stateProvider
                .state('app', {
                    url: '/app',
                    templateUrl: '/js/layout/shell.html'
                })
                .state('app.dashboard', {
                    url: '/dashboard', 
                    templateUrl: '/templates/dashboard.html',
                    controller: 'dashboardCtrl as dashboard'
                })
                .state('app.payments', {
                    url: '/payments', 
                    templateUrl: '/js/payments/payments.html',
                    controller: 'paymentsCtrl as payments'
                })
                .state('app.messages', {
                    url: '/messages', 
                    templateUrl: '/js/messages/messages.html',
                    controller: 'messagesCtrl as messages'
                })
                .state('app.hostels', {
                    url: '/hostels', 
                    templateUrl: '/js/hostels/hostels.html',
                    controller: 'hostelsCtrl as hostels'
                })
                .state('app.profile', {
                    url: '/profile', 
                    templateUrl: '/js/profile/profile.html',
                    controller: 'profileCtrl as profile'
                })
                .state('travelerDash', { 
                    url: '/travelerDash', 
                    templateUrl: '/templates/travelerDash.html', 
                    controller: 'travelerDashController as travelerDash' 
                })
                .state('listings', {
                    url: '/listings?city?checkin?checkout?numGuest', 
                    templateUrl: '/templates/listings.html', 
                    controller:'listingsController as listings' 
                })
                .state('landing', { 
                    url: '', 
                    templateUrl: '/templates/landing.html', 
                    controller: 'LandingController as landing' 
                });
        }])
        .value('apiUrl', 'http://localhost:59822/api/');
})();
