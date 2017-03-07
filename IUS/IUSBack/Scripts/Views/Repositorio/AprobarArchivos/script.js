$(document).ready(function () {
        
    // eventos
        // tabs 
            $(document).on("click", ".tabRevision", function () {

            });
            $(document).on("click", ".tabAprobar", function () {
                var frm = {};
                actualizarCatalogo(RAIZ + "/AprobarNoticiaAccion/sp_adminfe_aprobarNoticia_getNoticiasRevision", frm, function (data) {
                    console.log("Data es ", data);
                })
            });
        // click
            $(document).on("click", ".btnEliminarArchivo", function () {
                var x = confirm("¿Esta seguro que desea eliminar por completo archivo publico?");
                var tr = $(this).parents("tr");
                if (x) {
                    var frm = {
                        idArchivoPublico: tr.find(".txtHdIdArchivo").val()
                    }
                    console.log("frm es ", frm);
                    btnEliminarArchivo(frm, tr);
                }
            });
            $(document).on("click", ".btnAprobarArchivo", function () {
                var x = confirm("¿Esta seguro que desea cambiar estado de archivo?");
                var tr = $(this).parents("tr");
                if (x) {
                    var frm = {
                        idArchivo: tr.find(".txtHdIdArchivo").val()
                    }
                    btnAprobarArchivo(frm,tr);
                }
            })
});