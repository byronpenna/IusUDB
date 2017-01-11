$(document).ready(function () {
    console.log("Este es: ");
    // eventos 
        // click
            $(document).on("click", ".imgSearchRepo", function () {
                var frm = inicialSearch();
                if (frm.buscando == 1) {
                    frm.txtBusqueda = $(".inputSearchRepo").val();
                    console.log("formulario es: ", frm);
                    btnBusqueda(frm);
                } else {

                }

            })
            $(document).on("click", ".btnNuevaCarpeta", function () {
        
                var seccion = $(this).parents(".modalContenido");
                var frm = {
                    idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                    nombre: seccion.find(".txtNombreCarpeta").val()
                }
                console.log("Formulario a enviar", frm);
                if (frm.nombre != "") {
                    btnGuardarCarpeta(frm, seccion);
                } else {
                    alert("El nombre de la carpeta no puede ser vacio");
                    //printMessage($(".mensajeNewFolder"), "El nombre de la carpeta no puede ser vacio", false);
                }
            })
            // eliminar 
                $(document).on("click", ".btnEliminarArchivo", function () {
                    var x = confirm("¿Esta seguro que desea dejar de compartir este archivo?");
                    if (x) {
                        var tr = $(this).parents("tr");
                        var frm = {
                            idArchivoPublico: tr.find(".txtHdIdArchivoPublico").val()
                        }
                        console.log("Formulario a enviar es: ", frm); 
                        btnEliminarArchivo(frm, tr);
                    }
                })
                $(document).on("click", ".btnEliminarCarpeta", function () {
                    var tr = $(this).parents("tr");
                    var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                    frm = { idCarpeta: tr.find(".txtHdIdCarpeta").val() }
                    if (x) {
                        btnEliminarCarpeta(frm, tr)
                    }
                })
    
})