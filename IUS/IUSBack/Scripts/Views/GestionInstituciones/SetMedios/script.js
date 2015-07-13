$(document).ready(function () {
    // eventos 
        // clicks 
           
                $(document).on("click", ".btnCancelar", function () {
                    var x = confirm("¿Esta seguro de cancelar edicion?");
                    if (x) {
                        trMedio = $(this).parents("tr");
                        controlesEdit(false, trMedio);
                    }
                })
                $(document).on("click", ".btnActualizar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    console.log(frm);
                    btnActualizar(frm, seccion);
                })
                $(document).on("click", ".btnEditar", function () {
                    trMedio = $(this).parents("tr");
                    btnEditar(trMedio);
                })
    
                $(document).on("click", ".btnAgregar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    var regex = new RegExp("^(http[s]?:\\/\\/(www\\.)?|ftp:\\/\\/(www\\.)?|www\\.){1}([0-9A-Za-z-\\.@:%_\+~#=]+)+((\\.[a-zA-Z]{2,3})+)(/(.)*)?(\\?(.)*)?");
                    if (regex.test(frm.txtEnlace)) {
                        regex = new RegExp("^http://");
                        if (!regex.test(frm.txtEnlace)) {
                            frm.txtEnlace = "http://" + frm.txtEnlace;
                        }
                        btnAgregar(frm);
                    } else {
                        alert("Favor ingresar una url valida");
                    }
                })
                $(document).on("click", ".btnEliminar", function () {
                    seccion = $(this).parents("tr");
                    frm = serializeSection(seccion);
                    btnEliminar(frm,seccion);
                });
})