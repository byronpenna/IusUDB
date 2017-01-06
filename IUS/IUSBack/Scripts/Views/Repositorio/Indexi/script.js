$(document).ready(function () {
    // eventos 
        // click 
            // Modal
                
                $(document).on("click", ".imgSearchRepo", function () {
                    console.log("Busqueda es: ");
                    var buscando = $(".txtHdBuscando").val();

                    if (buscando == 0) {
                        buscando = 1;
                        //$(".imgSearchRepo").attr(".")
                    } else {
                        buscando = 0;
                    }

                    var frm = {
                        txtBusqueda: $(".inputSearchRepo").val()
                    }
                    //console.log("Frm es: ", frm);
                    var seccion = null;
                    //console.log("Busqueda es: ");
                    btnBusqueda(frm, seccion);

                })
                $(document).on("click", ".icoSubirFichero", function (e) {
                    abrirModal(e,function () {
                        $(".divSubirArchivoModal").show();
                    });
                })
                /*
                $(document).on("click", ".icoNuevaCarpeta", function (e) {
                    
                    abrirModal(e,function () {
                        $(".divNuevaCarpetaModal").show();
                    });
                })
                */
                $(document).on("click", ".contenedorUpload", function (e) {
                    e.stopPropagation();
                });
                
                

                $(document).on("click", ".lkbSubMenu", function (e) {
                    window.location = $(this).attr("href");

                })
                
                // eliminar
                    $(document).on("click", ".btnEliminarArchivo", function (e) {
                        e.preventDefault();
                        var x = confirm("Esta seguro que desea eliminar este archivo");
                        var tr = $(this).parents("tr");
                        if (x) {
                            var frm = {
                                idArchivo: tr.find(".txtHdIdArchivo").val()
                            }
                            console.log("frm es: ", frm);
                            btnEliminarArchivo(frm,tr);
                        }
                    })
                    $(document).on("click", ".btnEliminarCarpeta", function () {
                        var tr = $(this).parents("tr");
                        var x = confirm("¿Esta seguro que desea eliminar esta carpeta?");
                        if (x) {
                            frm = {
                                idCarpeta: tr.find(".txtHdIdCarpeta").val()
                            }
                            btnEliminarCarpeta(frm,tr);
                            console.log("Frm es : ", frm);
                            
                        }
                    })
                /*$(document).on("click", ".spIrBuscar", function () {
                    frm = { txtRuta: $(".txtDireccion").val() }
                    if (frm.txtRuta.slice(-1) != "/") {
                        frm.txtRuta += "/";
                    }
                    spIrBuscar(frm);
                });*/
                $(document).on("click", ".btnCambiarNombre", function () {
                    var seccion = $(this).parents("tr");
                    seccion.find(".txtArchivoNombre").val(seccion.find(".spNombre").text());
                    controlesEdit(true, seccion);
                    console.log("Estas por cambiar el nombre");
                })
                $(document).on("click", ".btnCancelarEdicionCarpeta", function () {
                    var seccion = $(this).parents("tr");
                    controlesEdit(false, seccion);
                })
                $(document).on("click", ".btnEditarNombre", function () {
                    var seccion = $(this).parents("tr");
                    var frm = {
                        idArchivo:      seccion.find(".txtHdIdArchivo").val(),
                        nombreArchivo: seccion.find(".txtArchivoNombre").val()
                        /*
                        txtHdIdCarpeta: folder.find(".txtHdIdCarpeta").val(),
                        nombre: folder.find(".txtArchivoNombre").val()*/
                    }
                    btnEditarNombre(frm,seccion);
                    console.log("frm es: ", frm);
                })
                $(document).on("submit", "#frmSubir", function (e) {
                    var inputFile = $("#flArchivos"); var divMensajes = $(".divMensajes");
                    var files = inputFile[0].files;
                    var formulariohtml = $(this);

                    var frm = { txtHdIdCarpetaPadre: $(".txtHdIdCarpetaPadre").val() };

                    e.preventDefault();

                    if (inputFile.val() != "") {
                        if (frm.txtHdIdCarpetaPadre != -1 || frm.txtHdIdCarpetaPadre != '-1') {
                            cn = 0;
                            totalFiles = files.length;
                            $.each(files, function (file) {
                                frm.cn = cn;
                                var data = getIndividualFormData(files[cn], frm);
                                frmSubir(data, formulariohtml.attr("action"), totalFiles);
                                cn++;
                            })
                        } else {
                            alert("No se pueden subir sobre la carpeta raiz");
                        }
                    } else {
                        alert("Por favor seleccione un archivo")
                    }
                })
                $(document).on("click", ".btnNuevaCarpeta", function (e) {
                    var frm = {
                        idCarpetaPadre: $(".txtHdIdCarpetaPadre").val(),
                        nombre: $(this).parents(".divNuevaCarpetaModal").find(".txtNombreCarpeta").val()
                    }
                    console.log("El formulario es");
                    if (frm.nombre != "") {
                        btnNuevaCarpeta(frm);
                    } else {
                        alert("No puedes dejar vacios");
                    }
                    
                })
            // para menu 
                /*
                $(document).on("click", ".NewActionLi", function () {
                    var elemento = $(this).find(".ulSub");
                    if (elemento.is(":visible")) {
                        elemento.hide();
                        console.log("Mostrar");
                    } else {
                        //$(this).find(".ulSub").hide();
                        elemento.show();
                        console.log("Ocultar");
                    }
                });*/
})