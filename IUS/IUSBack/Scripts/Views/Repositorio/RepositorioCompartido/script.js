$(document).ready(function () {
    // plugins
        $(".cbUsuarios").chosen({ no_results_text: "No existe ese usuario", width: '100%' });
    //eventos
        // doble click
            $(document).on("dblclick", ".divCarpetaUsuarioCompartido", function (e) {
                var frm = {
                    idUserFile: $(this).find(".txtHdIdUsuario").val(),
                    nombreCarpeta: $(this).find(".tituloCarpetaPublica").text()
                }
                console.log(frm);
                var seccion = $(this).parents(".seccionCompartida");
                divCarpetaUsuarioCompartido(frm,seccion);
            });
        // click
            $(document).on("click", ".icoCancelShare", function (e) {
                e.preventDefault();
                $(".nombreFileCompartir").empty();
                $(".txtHdIdArchivoCompartir").val(-1);
            })
            $(document).on("click", ".icoDejarDeCompartir", function (e) {
                e.preventDefault();
                var seccion = $(this).parents(".divCarpetaPublica");
                var frm = {
                    idArchivo: seccion.find(".txtHdIdArchivoCompartido").val()
                }
                var x = confirm("¿Esta seguro que desea dejar de compartir archivo?");
                console.log("formulario a envar es", frm);
                if (x) {
                    icoDejarDeCompartir(frm, seccion);
                }
            })
            $(document).on("click", ".icoCompartidoBack", function () {
                var frm = {};
                actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_getUsuariosArchivosCompartidos", frm, function (data) {
                    console.log("Respuesta servidor", data);
                    if (data.estado) {
                        $(".divUsuarioCarpeta").addClass("hidden");
                        var div = "";
                        if (data.usuarios !== null) {
                            $.each(data.usuarios, function (i, usuario) {
                                div += getDivUsuarios(usuario);
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
                    idUsuario: $(".cbUsuarios").val(),
                    nombreCarpeta: $(".cbUsuarios option:selected").text()
                }
                console.log(frm);
                if (frm.idUsuario != -1) {
                    btnShareArchivo(frm)
                } else {
                    printMessage($(".divMessageCompartir"), "Por favor seleccione un usuario para compartir", false);
                }
                
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