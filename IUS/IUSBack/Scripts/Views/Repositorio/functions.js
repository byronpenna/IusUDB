// generics 
    function getDivNewFolder() {
        div = "<div class='col-lg-2 folder'>\
                    <div class='cuadritoIconoAdd'>\
                        <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder-opacity.png' class='imgFolder' />\
                        <div class='detalleCarpeta'>\
                            <div class='row marginNull inputNombreCarpeta'>\
                                <input type='text' class='form-control txtNombreCarpeta'>\
                            </div>\
                            <div class='row marginNull'>\
                                <button class='btn btn-xs btnGuardarCarpeta'>Guardar</button>\
                                <button class='btn btn-xs btnCancelarGuardarCarpeta'>Cancelar</button>\
                            </div>\
                        </div>\
                    </div>\
                </div>";
        return div;
    }
// scripts 
    function btnGuardarCarpeta(frm,seccion) {
        actualizarCatalogo(RAIZ + "/Repositorio/sp_repo_insertCarpeta", frm, function (data) {
            console.log("Respuesta server", data);
            if (data.estado) {
                seccion.find(".imgFolder").attr("src", RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder.png");
                adentro = "<h3>"+data.carpeta._nombre+"</h3>";
                seccion.find(".detalleCarpeta").empty().append(adentro);
                x = seccion.find(".cuadritoIconoAdd");
                x.removeClass("cuadritoIconoAdd");
                x.addClass("cuadritoIcono");
            } else {
                alert("Ocurrio un error intentando agregar carpeta");
            }
        });
    }