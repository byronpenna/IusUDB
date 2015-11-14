function paginacionNormal(frm,target) {
    actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_front_getNoticiasPagina", frm, function (data) {
        var divPost = "";
        if (data.estado) {
            if (data.posts !== undefined && data.posts !== null && data.posts.length > 0) {
                $.each(data.posts, function (i, post) {
                    divPost += getDivPost(post);
                })
            }
            target.empty().append(divPost);
            var url = "/Noticias/todas/" + $.trim(frm.pagina) + "/" + frm.cn + "";
            
            window.history.pushState(0, "0", url);
        }
    }, function () {
        target.empty().append("\
            <div class='row text-center'>\
                <img src='" + RAIZ + "/Content/images/generales/ajax-loader.gif' />\
            </div>\
        ")
    })
}
function numPaginacion(frm) {
    var target = $(".divTarjetasNoticias");
    if ($(".txtHdBuscando").val() == 1) {
        $(".txtHdNumPage").val(frm.pagina);
        $(".txtHdRango").val(frm.cn);
        $(".btnBuscar").click();
        //---
        var url = "/Noticias/todas/" + $.trim(frm.pagina) + "/" + frm.cn + "";
        
        window.history.pushState(0, "0", url);
    } else {
        paginacionNormal(frm,target);
    }
}
function getDivPost(post) {
    var srcImg = IMG_GENERALES+"image.png";
    
    if (post.convertMiniatura !== null) {
        console.log
        srcImg = "data:image/png;base64," + post.convertMiniatura;
    } else {
        
    }
    //
    //
    /*var div = "\
        <div class='col-lg-4'>\
            <h3 class='text-center'>"+post._titulo+"</h3>\
            <div class='imgNoticiaTarjeta'>\
                <img src='"+srcImg+"' />\
            </div>\
            <p>"+post._descripcion+"</p>\
            <div class='row marginNull text-center'>\
                <a href='#'>Leer noticia</a>\
            </div>\
        </div>";*/
    var div = "\
        <div class='col-lg-4'>\
                <div class='panel panel-default'>\
                    <div class='panel-heading'>\
                        <h3 class='text-center'>"+post._titulo+"</h3>\
                    </div>\
                    <div class='panel-body bodyPanel'>\
                        <div class='imgNoticiaTarjeta'>\
                            <img src='" + srcImg + "' />\
                        </div>\
                        <p class='pPost'>"+post._descripcion+"</p>\
                        \
                    </div>\
                    <div class='panel-footer text-center footerPanel'>\
                        <!--<div class='row marginNull text-center'>-->\
                        <div class='row marginNull'>\
                            <div class='col-lg-6 text-center'>\
                                <a class='btn btn-default' href=''>\
                                    Leer noticia\
                                </a>\
                            </div>\
                            <div class='col-lg-6 text-center'>\
                                "+post._fechaCreacion+"\
                            </div>\
                        </div>\
                    </div>\
                </div>\
            </div>\
    ";

    return div;
}
function getNums(num,numPage) {
    var i; var classMostrar = "";
    var div = ""; var abierto = false; var activeNum = "";
    for (i = 0; i < num; i++) {
        if (!abierto) {
            if (i == 0) {
                classMostrar = "grupoNumActivo";
            } else {
                classMostrar = "hidden";
            }
            div += "<div class='divContenedorNumeritos " + classMostrar + "'>";
            abierto = true;
        }
        if (i + 1 == numPage) {
            activeNum = "activeNum";
        } else {
            activeNum = "";
        }
        div += "\
            <div class='num text-center "+activeNum+" pointer numPaginacion '>\
                "+(i + 1)+"\
            </div>\
        "
        if ((i + 1) % 4 == 0 && abierto) {
            div += "</div>";
            abierto = false;
        }
    }
    if (abierto) {
        div += "</div>";
    }
    return div;
}
function cancelarBuscando(frm) {
    actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_front_getNoticiasPagina", frm, function (data) {
        
        var posts = "";
        if (data.estado) {
            if (data.posts !== undefined && data.posts !== null && data.posts.length > 0) {
                $.each(data.posts, function (i, post) {
                    posts += getDivPost(post);
                })
            }
        }
        $(".divTarjetasNoticias").empty().append(posts);
        $(".btnBuscar").empty().append("Buscar");
        $(".txtHdBuscando").val(1);
    }, function () {
        $(".divTarjetasNoticias").empty().append("\
            <div class='row text-center'>\
                <img src='" + RAIZ + "/Content/images/generales/ajax-loader.gif' />\
            </div>\
        ")
    })
}
function btnBuscar(frm,btn) {
    actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_front_buscarNoticias", frm, function (data) {
        
        var posts = "";
        if (data.estado) {
            if (data.posts !== undefined && data.posts !== null && data.posts.length > 0) {
                $.each(data.posts, function (i, post) {
                    posts += getDivPost(post);
                })
            }
        }
        $(".divTarjetasNoticias").empty().append(posts);
        var divNums = getNums(data.numPage, frm.txtHdNumPage);
        $(".numRow").empty().append(divNums);

        btn.empty().append("Cancelar busqueda");
        $(".txtHdBuscando").val(1);
    }, function () {
        $(".divTarjetasNoticias").empty().append("\
            <div class='row text-center'>\
                <img src='" + RAIZ + "/Content/images/generales/ajax-loader.gif' />\
            </div>\
        ")
    })
}