// eventos 
    // change 
            $(document).on("change", ".rdBusqueda", function () {
                if ($(this).val() == 0) {
                    $(".btnBusqueda").addClass("visibilitiHidden");
                    $(".divBusquedaArchivos").removeClass("input-group");
                    buscarEnCarpeta($(".txtBusqueda").val());
                } else {
                    $(".divBusquedaArchivos").addClass("input-group");
                    $(".btnBusqueda").removeClass("visibilitiHidden");
                    $(".folders .folder").removeClass("hidden");
                }
            })
    // click
            // cambiar nombre carpeta
            $(document).on("click", ".sinRedirect", function (e) {
                e.stopPropagation();
            })
        
        // vistas 
            $(document).on("click", ".iconoVista", function (e) {
                e.preventDefault();
                if ($(this).hasClass("iconoVistaCuadricula")) {
                    verCuadricula();
                } else if ($(this).hasClass("icoVistaLista")) {
                    verLista();
                }
            })
        // folders  
            $(document).on("click", ".btnCancelarGuardarCarpeta", function (e) {
                div = $(this).parents(".folder");
                div.remove();
            })
        // directorio 
            $(document).on("click", ".spIrBuscar", function () {
                frm = { txtRuta: $(".txtDireccion").val() }
                if (frm.txtRuta.slice(-1) != "/") {
                    frm.txtRuta += "/";
                }
                spIrBuscar(frm);
            });
    // keyup
        // directorio
            $(document).on("keyup", ".txtBusqueda", function (e) {
                var charCode = e.which;
                if (charCode == 27) { // tecla esc cancela todo
                    $(this).val("");
                }
                //console.log(charCode);
                console.log($(".rdBusqueda:checked").val());
                if ($(".rdBusqueda:checked").val() == 0) {
                    buscarEnCarpeta($(this).val());
                } /*else if ($(".rdBusqueda:checked").val() == 0) {

                }*/
            })
            $(document).on("keyup", ".txtDireccion", function (e) {
                var charCode = e.which;
                console.log(charCode);
                if (charCode == 13) {// tecla enter
                    frm = { txtRuta: $(".txtDireccion").val() }
                    if (frm.txtRuta.slice(-1) != "/") {
                        frm.txtRuta += "/";
                    }
                    spIrBuscar(frm);
                }
            })
// generics
    function getNumVistaActual() {
        var vista = getVistaActual();
        var n = -1;
        switch (vista) {
            case "cuadricula": {
                n = -1;
                break;
            }
            case "lista": {
                n = 1;
                break;
            }
        }
        return n; 
    }
    function getVistaActual() {
        var vista = "";
        if ($(".iconoVistaCuadricula").hasClass("activeVista")) {
            vista = "cuadricula";
        } else if ($(".icoVistaLista").hasClass("activeVista")) {
            vista = "lista";
        }
        return vista;
    }
    function cambiarVistas(vistaVer) {
        var lista = $(".listView"); var cuadricula = $(".cuadriculaView");
        var pestaLista = $(".icoVistaLista"); var pestaCuadricula = $(".iconoVistaCuadricula");
        //$(".activeVista").removeClass("activeVista");
        $(".herramientaVistaSection .activeVista").removeClass("activeVista");
        switch (vistaVer) {
            case "cuadricula": {
                lista.addClass("hidden");
                cuadricula.removeClass("hidden");
                pestaCuadricula.addClass("activeVista");
                //pestaLista.removeClass("activeLista");
                break;
            }
            case "lista": {
                cuadricula.addClass("hidden");
                lista.removeClass("hidden");
                pestaLista.addClass("activeVista");
                //pestaCuadricula.removeClass("activeLista");
                break;
            }
        }
    }
    function buscarEnCarpeta(txt) {
        var vistaActual = getVistaActual();
        var folder; var parents;
        switch (vistaActual) {
            case "cuadricula":
                {
                    folder  = $(".folders .cuadriculaView .folder");
                    parents = ".folder";
                    break;
                }
            case "lista":
                {
                    folder  = $(".folders .listView .folderDetalles");
                    parents = ".folderDetalles";
                    break;
                }
        }
        console.log("yeah");
        if (txt == "") {
            //$(".folders .folder").removeClass("hidden");
            folder.removeClass("hidden");
        } else {
            //$(".folders .folder").addClass("hidden");
            folder.addClass("hidden");
            //var folders = $(".folder .ttlNombreCarpeta:containsi(" + txt + ")");
            var folders = folder.find(".ttlNombreCarpeta:containsi(" + txt + ")");
            folders = folders.parents(parents);
            folders.removeClass("hidden");
            //folders.css("background", "red");
        }
    }
    function getDivNewFolder() {
            /**/
        div = "<div class='col-lg-2 folder '>\
                    <input type='hidden' class='txtHdIdCarpeta' value=''/>\
                    <div class='row divHerramientasIndividual' style='display:none'>\
                        <a href='#' class='ico icoEliminarCarpeta' title='Eliminar'>\
                            <i class='fa fa-trash-o'></i>\
                        </a>\
                        <a href='#' class='ico icoWebsiteFolder' title='Ver website'>\
                            <i class='fa fa-globe'></i>\
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
                                <div class='row marginNull mensajeNewFolder'></div>\
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
        folder = seccion.parents(".cuadritoIconoAdd");

        folder.addClass("cuadritoIcono");
        folder.removeClass("cuadritoIconoAdd");
    }
