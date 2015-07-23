// generics
    // acciones genericas   
    
    function cambiarEstado(txt) {
        switch (txt) {
            case "buscar":
                {
                    $(".btnBuscarCarpeta").empty().append("<i class='fa fa-times'></i>");
                    $(".btnBuscarCarpeta").addClass("btnBuscando");
                    break;
                }
            case "cancelar_busqueda":
                {
                    $(".btnBuscarCarpeta").empty().append("Buscar");
                    $(".btnBuscarCarpeta").removeClass("btnBuscando");
                    break;
                }
        }
    }
    function cancelarBusqueda(frm) {
        actualizarCatalogo(RAIZ + "Repositorio/getArchivosSinBusqueda", frm, function (data) {
            console.log(data);
            if (data.estado) {
                var div = "";
                if (data.carpetas !== undefined && data.carpetas !== null) {
                    $.each(data.carpetas, function (i, carpeta) {
                        div += getDivCarpeta(carpeta);
                    })
                }
                if (data.archivos !== undefined && data.archivos !== null) {
                    $.each(data.archivos, function (i, archivo) {
                        div += getDivArchivo(archivo);
                    })
                }
                $(".folders").empty().append(div);
                cambiarEstado("cancelar_busqueda");
                $(".tituloPrincipal").empty().append(data.carpetaPadre._nombre);

            } else {
                alert("Ocurrio un error mientras se buscaba");
            }
        });
    }
    function buscarCarpeta(nombre) {
        $(".folder").addClass("hidden");
        var folders = $(".folder .folderTitle:containsi(" + nombre + ")");
        folders = folders.parents(".folder");
        folders.removeClass("hidden");
    }
    //div 
        function getDivCarpeta(carpeta) {
            var div = "<div class='col-xs-6 col-sm-4 col-md-3 col-lg-2 folder'>\
                    <a href='>\
                        <div class='row divHerramientasIndividual visiHidden'>\
                            <i class='fa fa-download'></i>\
                        </div>\
                        <div class='cuadritoIcono cuadritoCarpeta'>\
                            <img src='"+RAIZ+"/Content/images/views/repositorio/"+carpeta.getIconoFront+"' />\
                            <h3 class='folderTitle'>"+carpeta._nombre+"</h3>\
                        </div>\
                    </a>\
                </div>";
            return div;
        }
        function getDivArchivo(archivo){
            var div = "<div class='col-xs-6 col-sm-4 col-md-3 col-lg-2 folder'>\
                      <div class='row divHerramientasIndividual'>\
                          <a href='" + RAIZ + "/Repositorio/downloadFile/" + archivo._idArchivoPublico + "' class='ico' title='Descargar'>\
                              <i class='fa fa-download iconoHerramientas'></i>\
                          </a>\
                      </div>\
                      <div class='cuadritoIcono '>\
                          <img src='"+RAIZ+"/Content/images/views/repositorio/"+archivo._archivoUsuario._extension._tipoArchivo._icono+"' />\
                          <h3 class='folderTitle'>"+archivo._nombre+"</h3>\
                      </div>\
                  </div> ";
            return div;
        }
// acciones scripts 
    function btnBuscarCarpeta(frm,seccion) {
        actualizarCatalogo(RAIZ + "Repositorio/sp_repo_searchArchivoPublico", frm, function (data) {
            console.log("Respuesta de busqueda",data);
            if (data.estado) {
                $(".tituloPrincipal").empty().append("Resultados busqueda");
                var div = "";
                if (data.archivos !== undefined && data.archivos !== null) {
                    $.each(data.archivos,function(i,archivo){
                        div += getDivArchivo(archivo);
                    })
                } else {
                    div += "\
                    <div>\
                        No se encontraron resultados\
                    </div>\
                    ";
                }
                $(".folders").empty().append(div);
                cambiarEstado("buscar");
                /*seccion.empty().append("<i class='fa fa-times'></i>");
                seccion.addClass("btnBuscando");*/
            } else {
                alert("Ocurrio un error agregando");
            }
            
        })
    }
    function spIrBuscar(frm, seccion) {
        actualizarCatalogo(RAIZ + "Repositorio/sp_repo_front_getCarpetaPublicaByRuta", frm, function (data) {
            console.log(data);
            if (data.estado) {
                var accionControlador = "";
                if (frm.txtIdFiltro == -1) {
                    accionControlador = "AllFiles";
                } else {
                    accionControlador = "FileByCategory";
                }
                window.location = RAIZ + "Repositorio/" + accionControlador + "/" + data.carpetaPublica._idCarpetaPublica + "/" + frm.txtIdFiltro;
            } else {
                alert("No se encontro la carpeta");
            }
        })
    }