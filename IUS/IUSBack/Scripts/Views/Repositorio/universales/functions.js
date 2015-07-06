function getDivNewFolder() {
    div = "<div class='col-lg-2 folder '>\
                    <input type='hidden' class='txtHdIdCarpeta' value=''/>\
                    <div class='row divHerramientasIndividual'>\
                        <a href='#' class='ico' title='Descargar'>\
                            <i class='fa fa-download'></i>\
                        </a>\
                        <a href='#' class='ico icoEliminarCarpeta' title='Eliminar'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                    </div>\
                    <div class='cuadritoIconoAdd cuadritoCarpeta' id='0'>\
                        <img src='" + RAIZ + "/Content/themes/iusback_theme/img/general/repositorio/folder-opacity.png' class='imgFolder' />\
                        <div class='detalleCarpeta sinRedirect'>\
                            <div class='normalMode sinRedirect hidden'>\
                                <h3 class='ttlNombreCarpeta'></h3>\
                            </div>\
                            <div class='row marginNull hidden editMode sinRedirect'>\
                                <div class='row marginNull inputNombreCarpeta'>\
                                    <input type='text' class='form-control txtNombreCarpeta'>\
                                </div>\
                                <div class='row marginNull'>\
                                    <button class='btn btn-xs btnEditarCarpeta'>Actualizar</button>\
                                    <button class='btn btn-xs btnCancelarEdicionCarpeta'>Cancelar</button>\
                                </div>\
                            </div>\
                            <div class='saveMode'>\
                                <div class='row marginNull inputNombreCarpeta'>\
                                    <input type='text' class='form-control txtNombreCarpetaSave'>\
                                </div>\
                                <div class='row marginNull'>\
                                    <button class='btn btn-xs btnGuardarCarpeta'>Guardar</button>\
                                    <button class='btn btn-xs btnCancelarGuardarCarpeta'>Cancelar</button>\
                                </div>\
                            </div>\
                        </div>\
                    </div>\
                </div>";
    return div;
}
// actualizar carpeta 
    function ttlNombreCarpeta(seccion, nombre) {
        seccion.find(".normalMode").addClass("hidden");
        seccion.find(".editMode").removeClass("hidden");
        folder = seccion.parents(".cuadritoIcono");
        folder.removeClass("cuadritoIcono");
        folder.addClass("cuadritoIconoAdd");
        seccion.find(".txtNombreCarpeta").val(nombre);
    }
    function btnCancelarEdicionCarpeta(seccion) {
        seccion.find(".editMode").addClass("hidden");
        seccion.find(".normalMode").removeClass("hidden");
        folder = seccion.parents(".cuadritoIcono");
        folder.addClass("cuadritoIcono");
        folder.removeClass("cuadritoIconoAdd");
    }