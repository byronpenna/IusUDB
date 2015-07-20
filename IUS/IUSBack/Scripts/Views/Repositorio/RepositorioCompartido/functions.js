// generics 
    function getDivArchivosCompartidos(archivoCompartido) {
        var div = "\
            <div class='divCarpetaPublica col-lg-6'>\
                <div class='row marginNull'>\
                    <a href='#' class='icoDejarDeCompartir' title='Compartir'>\
                        <i class='fa fa-trash-o'></i>\
                    </a>\
                </div>\
                <input type='hidden' class='txtHdIdArchivoCompartido' value='" + archivoCompartido._idArchivoCompartido + "'/>\
                <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/" + archivoCompartido._archivo._extension._tipoArchivo._icono + "' />\
                <h4 class='tituloCarpetaPublica'>" + archivoCompartido._archivo._nombre + "</h4>\
            </div>\
        ";
        return div;
    }
    function getDivUsuarios(usuario) {
        var div = "\
            <div class='divCarpetaPublica divCarpetaUsuarioCompartido col-lg-6'>\
                <input type='hidden' class='txtHdIdUsuario' value='"+usuario._idUsuario+"'/>\
                <img src='"+RAIZ+"/Content/themes/iusback_theme/img/general/profle.png' />\
                <h4 class='tituloCarpetaPublica'>"+usuario._usuario+"</h4>\
            </div>\
        ";
        return div;
    }
// scripts 
    function icoDejarDeCompartir(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_removeShareFile", frm, function (data) {
            console.log(data);
            if (data.estado) {
                seccion.remove();
            }
        });
    }
    function divCarpetaUsuarioCompartido(frm, seccion) {
        var nombreUsuarioCarpeta = frm.nombreCarpeta;
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_getFilesFromShareUserId", frm, function (data) {
            console.log("Respuesta de servidor",data);
            if (data.estado) {
                $(".divUsuarioCarpeta").find(".hUsuarioCarpeta").empty().append(nombreUsuarioCarpeta);
                $(".divUsuarioCarpeta").removeClass("hidden");
                var div = "";
                if (data.archivos !== null) {
                    $.each(data.archivosCompartidos, function (i, archivoCompartido) {
                        div += getDivArchivosCompartidos(archivoCompartido);
                    });
                }
                seccion.empty().append(div);
            } else {
                alert("Ocurrio un error cargando los archivos");
            }
        })
    }
    function icoCompartirFile(folder) {
        var nombreArchivo = folder.find(".ttlNombreCarpeta").text();
        var idArchivo = folder.find(".txtHdIdArchivo").val();
        $(".nombreFileCompartir").empty().append(nombreArchivo);
        $(".txtHdIdArchivoCompartir").val(idArchivo);
    }
    function btnShareArchivo(frm) {
        var form = frm;
        console.log(" ;) ", form);
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_compartirArchivo", frm, function (data) {
            console.log(data);
            console.log("Frm es D: ",form);
            if (data.estado) {
                var frm = {
                    idUserFile: form.idUsuario,
                    nombreCarpeta: form.nombreCarpeta
                }
                var seccion = $(".seccionCompartida");
                divCarpetaUsuarioCompartido(frm, seccion);
            }
        })

    }
    function spIrBuscar(frm) {
        actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_byRuta", frm, function (data) {
            console.log(data);
            if (data.estado) {
                window.location = RAIZ + "RepositorioCompartido/index/" + data.carpeta._idCarpeta;
            } else {
                if (data.error._mostrar) {
                    alert(data.error.Message);
                }
            }
        })
    }