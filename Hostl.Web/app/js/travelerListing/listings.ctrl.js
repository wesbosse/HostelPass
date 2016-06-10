(function() {
    'use strict';

    angular
        .module('app')
        .controller('listingsController', listingsController);

    listingsController.$inject = ['$stateParams', 'listingsFactory', 'toastr'];

    /* @ngInject */
    function listingsController($stateParams, listingsFactory, toastr) {
        var vm = this;

        //Modal Vars
        vm.address = '';
        vm.city = '';
        vm.description = '';
        vm.hostelName = '';
        vm.image = '';
        vm.price = '';
        vm.state = '';
        vm.zipCode = '';

        vm.getInfo = function(hostel) {
            vm.address = hostel.Address;
            vm.city = hostel.City;
            vm.description = hostel.Description;
            vm.hostelName = hostel.HostelName;
            vm.image = hostel.Image;
            vm.price = hostel.Price;
            vm.state = hostel.State;
            vm.zipCode = hostel.ZipCode;
        };


        vm.city = $stateParams.city;
        vm.checkin = $stateParams.checkin;
        vm.checkout = $stateParams.checkout;
        vm.numGuest = $stateParams.numGuest;
        console.log($stateParams)

        vm.getListingsByCity = getListingsByCity;
        vm.getAllManagedHostels = getAllManagedHostels;
        //CALENDAR VARS
        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);

        vm.title = 'listingsController';
        vm.popup1 = { opened: false };
        vm.popup2 = { opened: false };
        vm.inlineOptions = {
            customClass: getDayClass,
            minDate: tomorrow,
            showWeeks: true
        };
        // vm.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        // vm.format = vm.formats[2];
        vm.altInputFormats = ['M!/d!/yyyy'];
        vm.dateOptions = {
            formatYear: 'yy',
            showWeeks: false,
            // dateDisabled: disabled,
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };
        vm.events = [{
            date: tomorrow,
            status: 'full'
        }, {
            date: afterTomorrow,
            status: 'partially'
        }];

        vm.hostels = [{

            "HostelName": "Sexy Hostel",
            "Address": "123 EZ st",
            "City": "SD",
            "State": "CA",
            "ZipCode": 92264,
            "Image": "..\\img\\a.jpg",
            "Description": "It is good"

        }, {

            "HostelName": "Great Hostel",
            "Address": "123 EZ st",
            "City": "SD",
            "State": "CA",
            "ZipCode": 92264,
            "Image": "..\\img\\a.jpg",
            "Description": "This hostel is amazing"

        }, {

            "HostelName": "Hostile Hostel",
            "Address": "123 EZ st",
            "City": "SD",
            "State": "CA",
            "ZipCode": 92264,
            "Image": "..\\img\\a.jpg",
            "Description": "It is good"

        }, {

            "HostelName": "Hostel",
            "Address": "123 EZ st",
            "City": "SD",
            "State": "CA",
            "ZipCode": 92264,
            "Image": "..\\img\\a.jpg",
            "Description": "It is good"

        }, {

            "HostelName": "Good God, Man",
            "Address": "123 EZ st",
            "City": "SD",
            "State": "CA",
            "ZipCode": 92264,
            "Image": "..\\img\\a.jpg",
            "Description": "It is good"

        }, ];

        vm.destinations = [{
            "city": "San Fransisco",
            "img": "..\\img\\sanfran.jpg"
        }, {
            "city": "New York",
            "img": "..\\img\\newyork.jpg"
        }, {
            "city": "Seattle",
            "img": "..\\img\\seattle.jpg"
        }, {
            "city": "London",
            "img": "..\\img\\london.jpg"
        }, {
            "city": "Rome",
            "img": "..\\img\\rome.jpg"
        }, {
            "city": "Paris",
            "img": "..\\img\\paris.jpg"
        }, {
            "city": "Thailand",
            "img": "..\\img\\thai.jpg"
        }, {
            "city": "Hawaii",
            "img": "..\\img\\hawaii.jpg"
        }, {
            "city": "Moscow",
            "img": "..\\img\\moscow.jpg"
        }, {
            "city": "Quebec",
            "img": "..\\img\\quebec.jpg"
        }, ];


        //CALENDAR FUNCTIONS
        vm.today = function() {
            vm.df = new Date();
            vm.dt = afterTomorrow;
        };

        vm.clear = function() {
            vm.dt = null;
        };

        vm.toggleMin = function() {
            vm.inlineOptions.minDate = vm.inlineOptions.minDate ? null : new Date();
            vm.dateOptions.minDate = vm.inlineOptions.minDate;
        };

        vm.open1 = function() {
            vm.popup1.opened = true;
        };

        vm.open2 = function() {
            vm.popup2.opened = true;
        };

        vm.setDate = function(year, month, day) {
            vm.dt = new Date(year, month, day);
        };

        //CALENDAR TASKS
        vm.today();
        vm.toggleMin();

        // function disabled(data) {
        //     var date = data.date,
        //         mode = data.mode;
        //                     console.log(data);
        //                     console.log(data.date);
        //     console.log(data.date.getDay());
        //     return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);


        // }

        function getDayClass(data) {
            var date = data.date,
                mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < vm.events.length; i++) {
                    var currentDay = new Date(vm.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return vm.events[i].status;
                    }
                }
            }

            return '';
        }

        function getListingsByCity() {
            console.log($stateParams.city)
            listingsFactory.getListingsByCity(vm.city)
                .then(function(response) {
                    vm.listings = response.data;
                    console.log(response.data);
                }, function(err) {
                    toastr.warning(err);
                });
        }

        function getAllManagedHostels() {
            listingsFactory.getAllManagedHostels()
                .then(function(response) {
                    vm.hostels = response.data;
                    console.log(vm.hostels)
                }, function(err) {
                    toastr.warning("You darn fucked up!");
                })
        }


    }
})();
