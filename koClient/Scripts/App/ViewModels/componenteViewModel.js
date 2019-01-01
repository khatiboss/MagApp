var webApiServerUrl = "http://localhost:60948";

var componente = function (parentViewModel) {
    var self = this;
    self.componenteId = ko.observable();
    self.codice = ko.observable();
    self.descrizione = ko.observable();
    self.note = ko.observable();
  
    self.codice.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().codiceErrors([]);
        } else {
            parentViewModel.modelState().codiceErrors(["Campo Codice è obligatorio"]);
        }
    });

    self.descrizione.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().descrizioneErrors([]);
        } else {
            parentViewModel.modelState().descrizioneErrors(["Campo Descrizione è obligatorio"]);
        }
    });

    self.note.subscribe(function (newValue) {
        if (newValue != "") {
            parentViewModel.modelState().noteErrors([]);
        } else {
            parentViewModel.modelState().noteErrors([""]);
        }
    });

   
    return self;
};

var errorKeys = function (cErr, dErr, nErr) {
    var self = this;
    self.codiceErrors = ko.observableArray(cErr);
    self.descrizioneErrors = ko.observableArray(dErr);
    self.noteErrors = ko.observableArray(nErr);
    return self;
}

function componenteViewModel() {
    var self = this;

    self.shouldShowButton = ko.observable(true);

    self.newcomponente = ko.observable(new componente(self));


    //Get tutti componenti
    self.componenti = ko.observableArray();

    self.modelState = ko.observable(new errorKeys());
    //Post new componente
    self.save = function () {
        if (self.newcomponente().codice() != undefined || self.newcomponente().descrizione() != undefined || self.newcomponente().note() != undefined) {
            $.ajax({
                type: "post",
                url: webApiServerUrl + "/api/componenti",
                data: ko.toJSON(self.newcomponente),
                //error:function(error){
                //    alert(error.responseText);
                //},
                statusCode: {
                    400: function (error) {
                        //  alert(error.responseText);
                        var response = JSON.parse(error.responseText); //error.responseJSON;
                        alert(response.message);
                        //alert(response.modelState["componente.codice"]);

                        self.modelState(errorKeys(response.modelState["componente.codice"], response.modelState["componente.descrizione"], response.modelState["componente.note"]));
                    },
                    404: function (error) {
                        alert(error);
                    }
                },
                success: function (data) {
                    $("#newComponente").modal("hide");
                    self.componenti.push(data);
                    self.newcomponente(new componente(self));
                },
                contentType: "Application/json"
            });
        } else {
            //alert("Name and Definition are required!");
            //name : componente1
            //definition:undefined
            self.modelState(new errorKeys(["Campo Codice è obligatorio"], ["Campo Descrizione è obligatorio"]));
        }
    };
    //Update Existing componente
    self.loadExistingComponente = function (componenteToUpdate) {
        self.newcomponente(componenteToUpdate);
    };
    self.creatNewComponente = function () {
        self.newcomponente(new componente(self));
        self.modelState(new errorKeys());
    };

    self.update = function () {
        $.ajax({
            type: "put",
            url: webApiServerUrl + "/api/componenti/" + self.newcomponente().componenteID,
            data: ko.toJSON(self.newcomponente),
            error: function (jgxhr, status, error) {
                alert(error);
            },
            success: function (data) {
                $("#existingComponente").modal("hide");

                //update componentes array
                //var oldcomponente = $.grep(self.componentes, function (t) { return t.id == self.newcomponente().id; })

                //self.componenti.replace(oldcomponente, self.newcomponente());

                //Refresh componenti array.

                //$.getJSON("/api/componenti", self.componentes);

                var tuttiComponenti = self.componenti();
                self.componenti([]);
                self.componenti(tuttiComponenti);

                self.newcomponente(new componente());
            },
            contentType: "Application/json"
        });
    };
    //delete componente from database using componente id
    self.remove = function (componenteToRemove) {
        console.log(componenteToRemove);
        swal({
            title: "Sei sicuro di cancellare questo Componente con tutti componenti ?",
            text: "Non puoi ripristinare più questo Record!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        //api/componenti/5
                        url: webApiServerUrl + "/api/componenti/" + componenteToRemove.componenteID,
                        success: function () {
                            self.componenti.remove(componenteToRemove);
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
        $.getJSON(webApiServerUrl + "/api/componenti", self.componenti);

        // $.getJSON("/Account/CheckVisibility", self.shouldShowButton);
    }

}