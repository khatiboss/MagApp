var carrello = function (parentViewModel) {
    var self = this;
    self.id = ko.observable();
    self.matricola = ko.observable();
    self.annoArrivo = ko.observable();
    self.areaStock = ko.observable();
    self.locazione = ko.observable();

    self.matricola.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().matricolaErrors([]);
        } else {
            parentViewModel.modelState().matricolaErrors(["Matricola field is requried"]);
        }
    });

    self.annoArrivo.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().annoArrivoErrors([]);
        } else {
            parentViewModel.modelState().annoArrivoErrors(["AnnoArrivo field is requried"]);
        }
    });

    self.areaStock.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().areaStockErrors([]);
        } else {
            parentViewModel.modelState().areaStockErrors(["AreaStock field is requried"]);
        }
    });

    self.locazione.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().locazioneErrors([]);
        } else {
            parentViewModel.modelState().locazioneErrors(["Locazione field is requried"]);
        }
    });
    return self;
};

var errorKeys = function (mErr, aaErr,asErr,lErr) {
    var self = this;
    self.matricolaErrors = ko.observableArray(mErr);
    self.annoArrivoErrors = ko.observableArray(aaErr);
    self.areaStockErrors = ko.observableArray(asErr);
    self.locazioneErrors = ko.observableArray(lErr);
    return self;
}

function carrelloViewModel() {
    var self = this;

    self.shouldShowButton = ko.observable(false);

    self.newcarrello = ko.observable(new carrello(self));


    //Get tutti carrelli
    self.carrelli = ko.observableArray();

    self.modelState = ko.observable(new errorKeys());
    //Post new carrello
    self.save = function () {
        if (self.newcarrello().matricola() != undefined || self.newcarrello().annoArrivo() != undefined || self.newcarrello().areaStock() != undefined || self.newcarrello().locazione() != undefined) {
            $.ajax({
                type: "post",
                url: "/api/carrelli",
                data: ko.toJSON(self.newcarrello),
                //error:function(error){
                //    alert(error.responseText);
                //},
                statusCode: {
                    400: function (error) {
                        //  alert(error.responseText);
                        var response = JSON.parse(error.responseText); //error.responseJSON;
                        alert(response.message);
                        //alert(response.modelState["carrello.Matricola"]);

                        self.modelState(errorKeys(response.modelState["carrello.Matricola"], response.modelState["carrello.AnnoArrivo"], response.modelState["carrello.AreaStock"], response.modelState["carrello.locazione"]));
                    },
                    404: function (error) {
                        alert(error);
                    }
                },
                success: function (data) {
                    $("#newcarrello").modal("hide");
                    self.carrelli.push(data);
                    self.newcarrello(new carrello(self));
                },
                contentType: "Application/json"
            });
        } else {
            //alert("Name and Definition are required!");
            //name : carrello1
            //definition:undefined
            self.modelState(new errorKeys(["Matricola field is required"], ["AnnoArrivo field is required"], ["AreaStock field is required"], ["Locazione field is required"]));
        }
    };
    //Update Existing carrello
    self.loadExistingCarrello = function (carrelloToUpdate) {
        self.newcarrello(carrelloToUpdate);
    };
    self.creatNewCarrello = function () {
        self.newcarrello(new carrello(self));
        self.modelState(new errorKeys());
    };

    self.update = function () {
        $.ajax({
            type: "put",
            url: "/api/carrelli/" + self.newcarrello().id,
            data: ko.toJSON(self.newcarrello),
            error: function (jgxhr, status, error) {
                alert(error);
            },
            success: function (data) {
                $("#existingCarrello").modal("hide");

                //update carrellos array
                //var oldcarrello = $.grep(self.carrellos, function (t) { return t.id == self.newcarrello().id; })

                //self.carrelli.replace(oldcarrello, self.newcarrello());

                //Refresh carrelli array.

                //$.getJSON("/api/carrelli", self.carrellos);

                var tuttiCarrelli = self.carrelli();
                self.carrelli([]);
                self.carrelli(tuttiCarrelli);

                self.newcarrello(new carrello());
            },
            contentType: "Application/json"
        });
    };
    //delete carrello from database using carrello id
    self.remove = function (carrelloToRemove) {
        var result = confirm("are you sure you want delete!");
        if (result) {
            $.ajax({
                //api/carrelli/5
                url: "/api/carrelli/" + carrelloToRemove.id,
                success: function () {
                    self.carrelli.remove(carrelloToRemove);
                },
                contenttype: "application/json",
                type: "Delete"
            });
        }
    };

    self.init = function () {
        $.getJSON("/api/carrelli", self.carrelli);

       // $.getJSON("/Account/CheckVisibility", self.shouldShowButton);
    }

}