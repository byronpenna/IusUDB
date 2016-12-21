$(document).ready(function () {
    // eventos 
        // click 
            // Modal

                $(document).on("click", ".icoSubirFichero", function (e) {
                    abrirModal(e,function () {
                        $(".divSubirArchivoModal").show();
                    });
                })

                $(document).on("click", ".icoNuevaCarpeta", function (e) {
                    /*e.preventDefault();
                    $(".modalContenido").hide();
                    $(".divUpload").fadeIn(400, function () {

                    });*/
                    abrirModal(e,function () {
                        $(".divNuevaCarpetaModal").show();
                    });
                })

                $(document).on("click", ".contenedorUpload", function (e) {
                    e.stopPropagation();
                });
                $(document).on("click", ".divUpload", function (e) {
                    $(this).fadeOut();
                })
                $(document).on("click", ".closeModal", function (e) {
                    $(".divUpload").click();
                })
                $(document).on("click", ".btnEliminarCarpeta", function () {
                    var tr = $(this).parents("tr");
                    var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                    if (x) {
                        frm = {
                            idCarpeta: tr.find(".txtHdIdCarpeta").val()
                        }
                        btnEliminarCarpeta(frm);
                        console.log("Frm es : ", frm);
                        tr.remove();
                    }
                })
                $(document).on("click", ".spIrBuscar", function () {
                    frm = { txtRuta: $(".txtDireccion").val() }
                    if (frm.txtRuta.slice(-1) != "/") {
                        frm.txtRuta += "/";
                    }
                    spIrBuscar(frm);
                });
                $(document).on("click", ".btnNuevaCarpeta", function (e) {
                    var frm = {
                        idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                        nombre: $(this).parents(".divNuevaCarpetaModal").find(".txtNombreCarpeta").val()
                    }
                    console.log("El formulario es");
                    if (frm.nombre != "") {
                        btnNuevaCarpeta(frm);
                    } else {
                        alert("No puedes dejar vacios");
                    }
                    
                })
            // para menu 
                $(document).on("click", ".NewActionLi", function () {
                    var elemento = $(this).find(".ulSub");
                    if (elemento.is(":visible")) {
                        elemento.hide();
                        console.log("Mostrar");
                    } else {
                        //$(this).find(".ulSub").hide();
                        elemento.show();
                        console.log("Ocultar");
                    }
                });
})