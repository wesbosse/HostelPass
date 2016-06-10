(function () {
    'use strict';
    var controllerId = 'profileCtrl';
    angular.module('app').controller(controllerId, ['common', profile]);

    function profile(common) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.userName = 'USER NAME';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Profile View'); });
        }
    }
})();