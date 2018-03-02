(function () {
    'use strict';

    var mainViewModel =
        {
            buildings: ko.observableArray(),

            searchProperties : function () {
                console.log('searching for a ...');
                var self = this;
                self.buildings([]);
                fetch('http://localhost/OnLeave/api/booking/topoffers', { method: 'post' })
                    .then(function (response) {
                        return response.text();
                    })
                    .then(function (text) {                        
                        self.buildings(JSON.parse(text));
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        };

    ko.applyBindings(mainViewModel, document.getElementById('main'));
})();