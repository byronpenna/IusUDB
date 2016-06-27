$(document).ready(function () {
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