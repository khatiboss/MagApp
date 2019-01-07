var webApiServerUrl = "http://localhost:60948";

var carrello = function (parentViewModel) {
    var self = this;
    self.carrelloId = ko.observable();
    self.matricola = ko.observable();
    self.annoArrivo = ko.observable();
    self.areaStock = ko.observable();
    self.locazione = ko.observable();

    self.matricola.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().matricolaErrors([]);
        } else {
            parentViewModel.modelState().matricolaErrors(["Campo obligatorio"]);
        }
    });

    self.annoArrivo.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().annoArrivoErrors([]);
        } else {
            parentViewModel.modelState().annoArrivoErrors(["Campo obligatorio"]);
        }
    });

    self.areaStock.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().areaStockErrors([]);
        } else {
            parentViewModel.modelState().areaStockErrors(["Campo obligatorio"]);
        }
    });

    self.locazione.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().locazioneErrors([]);
        } else {
            parentViewModel.modelState().locazioneErrors(["Campo obligatorio"]);
        }
    });
    return self;
};

var errorKeys = function (mErr, aaErr, asErr, lErr) {
    var self = this;
    self.matricolaErrors = ko.observableArray(mErr);
    self.annoArrivoErrors = ko.observableArray(aaErr);
    self.areaStockErrors = ko.observableArray(asErr);
    self.locazioneErrors = ko.observableArray(lErr);
    return self;
}

function carrelloViewModel() {
    var self = this;

    self.shouldShowButton = ko.observable(true);

    self.newcarrello = ko.observable(new carrello(self));



    //Get tutti carrelli
    self.carrelli = ko.observableArray();

    self.modelState = ko.observable(new errorKeys());
    //Post new carrello
    self.save = function () {
        if (self.newcarrello().matricola() != undefined || self.newcarrello().annoArrivo() != undefined || self.newcarrello().areaStock() != undefined || self.newcarrello().locazione() != undefined) {
            $.ajax({
                type: "post",
                url: webApiServerUrl + "/api/carrelli",
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
                    $("#newCarrello").modal("hide");
                    self.carrelli.push(data);
                    self.newcarrello(new carrello(self));
                },
                contentType: "Application/json"
            });
        } else {

            self.modelState(new errorKeys(["Campo Matricola obligatorio"], ["Campo AnnoArrivo obligatorio"], ["Campo AreaStock obligatorio"], ["Campo Locazione obligatorio"]));
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
            url: webApiServerUrl + "/api/carrelli/" + self.newcarrello().carrelloID,
            data: ko.toJSON(self.newcarrello),
            error: function (jgxhr, status, error) {
                alert(error);
            },
            success: function (data) {
                $("#existingCarrello").modal("hide");
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
        console.log(carrelloToRemove);
        swal({
            title: "Sei sicuro di voler cancellare ?",
            text: "Non puoi ripristinare queste informazioni!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        //api/carrelli/5
                        url: webApiServerUrl + "/api/carrelli/" + carrelloToRemove.carrelloID,
                        success: function () {
                            self.carrelli.remove(carrelloToRemove);
                        },
                        contenttype: "application/json",
                        type: "Delete"
                    });
                    swal("Cancellazione fatta con successo!", {
                        icon: "success",

                    });

                } else {
                    // swal("Your imaginary file is safe!");
                }
            });


    };

    self.init = function () {
        $.getJSON(webApiServerUrl + "/api/carrelli", self.carrelli);


    }

    self.infos = ko.observableArray();
    self.show = function (carrello) {
        console.log(carrello);
        $.getJSON(webApiServerUrl + "/api/carrelli/" + carrello.carrelloID, function (data) {
            console.log(carrello);
            self.infos(data);
        });
    };

  
}