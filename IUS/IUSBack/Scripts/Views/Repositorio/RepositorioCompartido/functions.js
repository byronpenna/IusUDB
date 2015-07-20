﻿// generics 
    function getDivArchivosCompartidos(archivo) {
        var div = "\
            <div class='divCarpetaPublica col-lg-6'>\
                <input type='hidden' class='txtHdIdArchivoCompartido' value=''/>\
                <img src='"+RAIZ+"/Content/themes/iusback_theme/img/general/repositorio/"+archivo._extension._tipoArchivo._icono+"' />\
                <h4 class='tituloCarpetaPublica'>"+archivo._nombre+"</h4>\
            </div>\
        ";
        return div;
    }
// scripts 
    function divCarpetaUsuarioCompartido(frm,seccion) {
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_getFilesFromShareUserId", frm, function (data) {
            console.log("Respuesta de servidor",data);
            if (data.estado) {
                var div = "";
                if (data.archivos !== null) {
                    $.each(data.archivos, function (i, archivo) {
                        div += getDivArchivosCompartidos(archivo);
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
        actualizarCatalogo(RAIZ + "/RepositorioCompartido/sp_repo_compartirArchivo", frm, function (data) {
            console.log(data);
            if (data.estado) {

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