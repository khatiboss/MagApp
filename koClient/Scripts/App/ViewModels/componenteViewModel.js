var webApiServerUrl = "http://localhost:60948";



function componenteViewModel() {
    var self = this;
    
    self.infosComponenti = ko.observableArray();

    self.init = function () {
        // $.getJSON(webApiServerUrl + "/api/componenti", self.componenti);
        console.log("started!");
        $.getJSON(webApiServerUrl + "/api/componenti/", function (data) {
            console.log(data);
            self.infosComponenti(data);
        });

    }

    //delete componente from database using componente id
    self.remove = function (componenteToRemove) {
        console.log(componenteToRemove);
        swal({
            title: "Sei sicuro di cancellare questo Componente?",
            text: "Non puoi ripristinare più informazioni!",
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

    


}