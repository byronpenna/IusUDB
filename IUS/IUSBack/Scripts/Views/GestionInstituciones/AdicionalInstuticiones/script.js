$(document).ready(function () {
    iniciales();
    $(document).on("click", ".btnTab", function () {
        changeUrl($(".txtHdNombreClass").val(), $(".txtHdFuncion").val(), $(".txtHdIdInstitucion").val(), $(this).attr("id"));
    })
    $(document).on("click", ".btnGuardarCambiosOtros", function () {
        var frm = serializeSection($(".tab4"));
        frm.idInstitucion = $(".txtHdIdInstitucion").val()
        console.log("Frm es ", frm);
        btnGuardarCambiosOtros(frm)
    })
    $(document).on("click", ".btnAceptarEdicionRevista", function () {
        var frm = serializeSection($(this).parents("tr"));
        console.log("Frm a enviar es: ", frm);
        btnAceptarEdicionRevista(frm);
    })
    $(document).on("click", ".btnCancelarEdicionRevista", function () {
        var tr = $(this).parents("tr");
        controlesEdit(false, tr);
    })
    $(document).on("click", ".btnModificarRevista", function () {
        var tr = $(this).parents("tr");
        editMode(tr)
    })
    $(document).on("click", ".btnEliminarRevista", function () {
        var tr = $(this).parents("tr");
        var frm = {
            idRevista: tr.find(".txtHdIdRevista").val()
        }
        console.log("Frm eliminar es: ", frm);
        var x = confirm("¿Esta seguro que desea eliminar revista?");
        if (x) {
            btnEliminarRevista(frm,tr);
        }
    });
    $(document).on("click", ".tabRevistas", function () {
        var frm = {
            idInstitucion: $(".txtHdIdInstitucion").val()
        };
        console.log("Frm es: ", frm);
        tabRevistas(frm);
    })
    $(document).on("click", ".btnAddRevista", function (e) {
        var frm = serializeSection($(this).parents("tr"));
        frm.idInstitucion = $(".txtHdIdInstitucion").val();
        console.log("Frm es: ", frm);
        btnAddRevista(frm);
    })
    $(document).on("click", ".txtNumAlumnos", function (e) {
        e.preventDefault();
        e.stopPropagation();
    })
    $(document).on("click", ".divAreaCarrera,.cuadritoNivelEducacion", function () {
        //var padre = $(this).parents(".divAreaCarrera");
        if ($(this).find(".txtHdEstado").val() == 1) {
            $(this).removeClass("cuadritoSelected");
            $(this).find(".txtHdEstado").val(0)
            //$(this).find(".txtNumAlumnos").prop("disabled", false);
        } else {
            $(this).addClass("cuadritoSelected");
            $(this).find(".txtHdEstado").val(1)
            //$(".txtNumAlumnos").prop("disabled", true);
        }
    })
    $(document).on("click", ".cuadritoNivelEducacion", function () {
        $(this).find(".txtNumAlumnos").focus();
    })
    
    $(document).on("click", ".btnGuardarAreaConocimiento", function () {
        var frm = serializeSection($(".divAreaCarrera"));
        frm.areaCarrera = new Array();
        $.each(frm.txtHdIdEstadoArea, function (i, val) {
            if (val == 1) {
                frm.areaCarrera.push(frm.txtHdIdArea[i]);
            }
        })
        frm.idInstitucion = $(".txtHdIdInstitucion").val();
        btnGuardarAreaConocimiento(frm);
        
    })
    $(document).on("click", ".btnGuardarNivel", function () {
        var frm = serializeSection($(".rowOpcionesNiveles"));
        frm.estadoNivel = new Array();
        frm.alumnos     = new Array();
        $.each(frm.txtHdEstadoNivel, function (i, val) {
            if (val == 1) {
                frm.estadoNivel.push(frm.txtHdIdNivelEducacion[i]);
                frm.alumnos.push(frm.txtNumAlumnos[i]);
            }
        })
        frm.idInstitucion = $(".txtHdIdInstitucion").val();
        btnGuardarNivel(frm);
        /*var cuadritos = new Array();
        var frm = new Object();
        var cuadrito = null;
        frm.strEstadoNivel = ""; frm.strNumAlumnos = "";
        $(".cuadritoNivelEducacion").each(function (i) {
            console.log("El valor de i es: ", i);
            cuadrito = serializeSection($(this))
            if (cuadrito.txtHdEstadoNivel == 1) {
                if (i == 0) {
                    frm.strEstadoNivel += "";
                } else {
                    frm.strEstadoNivel += "," + "";
                }
                cuadritos.push(cuadrito);
            }
        })
        frm.cuadritos = cuadritos;
        frm.idInstitucion = $(".txtHdIdInstitucion").val();
        console.log("El otro frm es: ", frm);*/
        //btnGuardarNivel(frm);
        //console.log("Frm es: ", frm);
        //console.log("cuadritos", cuadritos);
    })
})