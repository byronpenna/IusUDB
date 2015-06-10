$(document).ready(function () {
    // plugins 
        // tabs 
            $('#horizontalTab').responsiveTabs();
    // eventos 
        
        // submit
            $(document).on("submit", "#frmInstitucional", function (e) {
                e.preventDefault();
                frm = serializeToJson($(this).serializeArray());
                console.log("Formulario a enviar es: ", frm);
                frmInstitucional(frm);
            });
            $(document).on("submit", "#frm", function (e) { // imagen
                var files = $("#file1")[0].files
                //try{
                    data = getObjFormData(files);
                    e.preventDefault();
                    $("#div_carga").show();
                    section = $(this);
                    getImageFromInputFile($("#file1")[0].files[0], function (imagen) {
                        formularioSubir(data, section.attr("action"), section, imagen);
                    });
                    
                //} catch (error) {
                    /*console.log("error get data", error);
                    alert(error.message);*/
                //}
                
            });
        // click
            $(document).on("click", ".btnEliminarImage", function () {
                var x = confirm("¿Esta seguro que desea eliminar esta imagen?")
                if (x) {
                    btnEliminarImage($(this).parents(".divImgIndividual"))
                }
            });
            $(document).on("click", ".btnDeshabilitarSliderImage", function () {
                var x = confirm("Esta seguro que desea cambiar estado de imagen");
                if (x) {
                    btnDeshabilitarSliderImage($(this).parents(".divImgIndividual"), $(this));
                }
            })
            $(document).on("click", ".iconQuitarValor", function () {
                var x = confirm("¿Esta seguro de quitar valor?");
                tr = $(this).parents("tr");
                if (x) {
                    iconQuitarValor(tr);
                }
                
            });
            $(document).on("click", ".btnAddValor", function () {
                frmSection = $(this).parents(".agregarValorSection");
                frm = serializeSection(frmSection);
                console.log("formulario a agregar", frm);
                btnAddValor(frm);
            })
});