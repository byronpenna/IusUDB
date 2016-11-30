$(document).ready(function () {
    // plugins 
        $(".txtFechaCaducidad").datepicker({
            dateFormat: "dd/mm/yy"
        });
    // eventos 
        // submit 
            // modal
                $(document).on("submit", ".frmAprobar", function (e) {
                    e.preventDefault();
                    var frm = serializeToJson($(this).serializeArray());
                    console.log("El formulario es: ", frm);

                    if (frm.txtFechaCaducidad != "") {
                        console.log("El formulario es: ", frm);
                        frmAprobar(frm);
                    } else {
                        alert("Por favor introducir fecha de caducidad");
                    }
                })
                $(document).on("submit", ".frmCambios", function (e) {
                    e.preventDefault();
                    var frm = serializeToJson($(this).serializeArray());
                    console.log("Formulario es: ", frm);
                    var x = confirm("¿Esta seguro que desea ejecutar esta acción?");
                    if (x) {
                        btnEnviarSolicitud(frm);
                    }
                    
                })
        //click
            // botones de acción
                $(document).on("click", ".btnEliminarSolicitud", function () {
                    /*var x = confirm("¿Esta seguro que desea eliminar la solicitud ?");
                    if (x) {*/
                        $(".divRechazar .tituloSecciones").empty().append("Eliminar solicitud");
                        $(".divRechazar .lbTitulo").empty().append("Motivos eliminación");
                        $(".txtHdIdAccion").val(2);
                        openConMotivos();
                    //}
                })
                $(document).on("click", ".btnSolicitarCambio", function (e) {
                    //icoSubirFichero
                    //console.log("Esta por solicitar cambio");
                    $(".divRechazar .tituloSecciones").empty().append("Solicitud de cambio");
                    $(".divRechazar .lbTitulo").empty().append("Motivos revisión");
                    $(".txtHdIdAccion").val(1);
                    openConMotivos();
                })
                $(document).on("click", ".btnAprobar", function () {
                    
                    $(".divRechazar").css("display", "none");
                    $(".divAprobar").css("display", "block");

                    $(".divUpload").fadeIn(400, function () {

                    });
                    $(".contenedorUpload").click();
                    console.log("Entro aquí a aprobar");
                })
                
            // cuadro modal
                
                $(document).on("click", ".divUpload", function () {
                    $(this).fadeOut();
                })
                $(document).on("click", ".contenedorUpload", function (e) {
                    e.stopPropagation();
                });
                
                
});