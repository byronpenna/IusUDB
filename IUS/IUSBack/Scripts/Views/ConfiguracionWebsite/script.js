$(document).ready(function () {
    // plugins 
        // tabs 
            $('#horizontalTab').responsiveTabs();
    // eventos 
        // change 
            $(document).on("change", "#file1", function (e) {
                /*var files = e.target.files;
                console.log("data es: ", files);
                if (files.length > 0) {
                    if (window.FormData !== undefined) {
                        var data = new FormData();
                        for (var x = 0; x < files.length; x++) {
                            data.append("file" + x, files[x]);
                        }
                        console.log("data a enviar es: ", data);
                        $.ajax({
                            type: "POST",
                            url: $("#frm").attr("action"),
                            contentType: false,
                            processData: false,
                            data: data,
                            success: function (result) {
                                console.log(result);
                            },
                            error: function (xhr, status, p3, p4) {
                                var err = "Error " + " " + status + " " + p3 + " " + p4;
                                if (xhr.responseText && xhr.responseText[0] == "{")
                                    err = JSON.parse(xhr.responseText).Message;
                                console.log(err);
                            }
                        });
                    } else {
                        alert("This browser doesn't support HTML5 file uploads!");
                    }
                }*/
            });// eliminar cuando todo este seguro
        // submit
            $(document).on("submit", "#frmInstitucional", function (e) {
                e.preventDefault();
                frm = serializeToJson($(this).serializeArray());
                console.log("Formulario a enviar es: ", frm);
                frmInstitucional(frm);
            });
            $(document).on("submit", "#frm", function (e) {
                var files = $("#file1")[0].files
                try{
                    data = getObjFormData(files);
                    console.log("form data es: ", data);
                    frm(data, $(this).attr("action"));
                } catch (error) {
                    console.log("error get data", error);
                    alert(error.message);
                }
                e.preventDefault();
            });
        // click
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