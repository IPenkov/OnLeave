(function () {
    'use strict';

    var mainViewModel =
        {
            buildings: ko.observableArray(),

            name: ko.observable(),

            searchProperties : function () {
                console.log('searching for a ...');
                var self = this;
                self.buildings([]);
                fetch('http://localhost/OnLeave/api/booking/search', {
                    method: 'post',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    body: JSON.stringify(self.name())
                })
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