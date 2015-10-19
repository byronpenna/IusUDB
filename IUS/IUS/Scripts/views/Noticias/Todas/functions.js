function numPaginacion(frm) {
    var target = $(".divTarjetasNoticias");
    actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_front_getNoticiasPagina", frm, function (data) {
        console.log(data);
        var divPost = "";
        if (data.estado) {
            if (data.posts !== undefined && data.posts !== null && data.posts.length > 0) {
                $.each(data.posts, function (i, post) {
                    divPost += getDivPost(post);
                })
            }
            target.empty().append(divPost);
            var url = "/Noticias/todas/" + $.trim(frm.pagina) + "/" + frm.cn + "";
            console.log("url",url);
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
function getDivPost(post) {
    var srcImg = IMG_GENERALES+"image.png";
    
    if (post.convertMiniatura !== null) {
        console.log("entro aqui");
        srcImg = "data:image/png;base64," + post.convertMiniatura;
    } else {
        console.log("entro al else");
    }
    //console.log("D:", post.convertMiniatura);
    //console.log("D:", srcImg);
    var div = "\
        <div class='col-lg-3'>\
            <h3 class='text-center'>"+post._titulo+"</h3>\
            <div class='imgNoticiaTarjeta'>\
                <img src='"+srcImg+"' />\
            </div>\
            <p>"+post._descripcion+"</p>\
            <div class='row marginNull text-center'>\
                <a href='#'>Leer noticia</a>\
            </div>\
        </div>";
    return div;
}
function getNums(num) {
    var i; var classMostrar = "";
    var div = ""; var abierto = false;
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
        div += "\
            <div class='num text-center pointer numPaginacion '>\
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
function btnBuscar(frm) {
    actualizarCatalogo(RAIZ + "/Noticias/sp_adminfe_front_buscarNoticias", frm, function (data) {
        console.log("la data despues de buscar es:",data);
        var posts = "";
        if (data.estado) {
            if (data.posts !== undefined && data.posts !== null && data.posts.length > 0) {
                $.each(data.posts, function (i, post) {
                    posts += getDivPost(post);
                })
            }
        }
        $(".divTarjetasNoticias").empty().append(posts);
        var divNums = getNums(data.numPage);
        $(".numRow").empty().append(divNums);
    }, function () {
        $(".divTarjetasNoticias").empty().append("\
            <div class='row text-center'>\
                <img src='" + RAIZ + "/Content/images/generales/ajax-loader.gif' />\
            </div>\
        ")
    })
}