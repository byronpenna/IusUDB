$(document).ready(function () {
    // plugins
        $(".cbUsuarios").chosen({ no_results_text: "No existe ese usuario", width: '100%' });
    //eventos
        // doble click
            $(document).on("dblclick", ".divCarpetaUsuarioCompartido", function (e) {
                var frm = {
                    idUserFile: $(this).find(".txtHdIdUsuario").val()
                }
                console.log(frm);
                var seccion = $(this).parents(".seccionCompartida");
                divCarpetaUsuarioCompartido(frm,seccion);
            });
        // click
            $(document).on("click", ".icoCompartidoBack", function () {
                actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_getUsuariosArchivosCompartidos", frm, function (data) {
                    console.log("Respuesta servidor", data);
                    if (data.estado) {
                        var div = "";
                        if (data.usuarios !== null) {
                            $.each(data.usuarios, function (i, usuario) {

                            })
                        }
                        $(".seccionCompartida").empty().append(div);
                    } else {
                        alert("Ocurrio un error regresando");
                    }
                })
            });
            $(document).on("click", ".btnShareArchivo", function (e) {
                var frm = {
                    idArchivo: $(".txtHdIdArchivoCompartir").val(),
                    idUsuario: $(".cbUsuarios").val()
                }
                console.log(frm);
                btnShareArchivo(frm)
            })
            $(document).on("click", ".icoCompartirFile", function (e) {
                e.preventDefault();
                var folder = $(this).parents(".folder");
                icoCompartirFile(folder);
            })
            $(document).on("click", ".cuadritoCarpeta", function () {
                var estado = $(this).attr("id");
                if (estado != '0') {
                    window.location = RAIZ + "RepositorioCompartido/index/" + $(this).parents(".folder").find(".txtHdIdCarpeta").val();
                }
            });
})