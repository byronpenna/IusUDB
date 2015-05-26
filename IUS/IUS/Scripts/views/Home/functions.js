// iniciales
    function setIdiomaPreferido() {
        var idIdioma = $.cookie('IUSidIdioma');
        if (idIdioma !== undefined) {
            $(".cbIdioma option[value='" + idIdioma + "']").attr("selected", true);
        }
    }
    function getNextImage(img,direccion) {
        var next;
        if (direccion == 1) {
            next = img.next();
            if (next.attr("src") === undefined) {
                //next = img.first();
                next = img.parents(".slider").find("img").first();
            }
        } else {
            next = img.prev();
            if (next.attr("src") === undefined) {
                //next = img.last();
                next = img.parents(".slider").find("img").last();
            }
        }
        return next;
    }
// acciones scripts 
    function navBtn(divSlider, direccion) {
        img = divSlider.find(".activeSliderImage");
        next = getNextImage(img, direccion);

        img.removeClass("activeSliderImage");
        img.addClass("hidden");
        next.addClass("activeSliderImage");
        next.removeClass("hidden");
    }
    function cbIdioma(idIdioma) {
        frm = { idIdioma: idIdioma };
        console.log("formulario a enviar es: ", frm);
        actualizarCatalogo(RAIZ + "Home/sp_trl_getIdiomaFromIds", frm, function (data) {
            console.log("La respuesta del servidor es: ", data);
            if (data.estado) {
                $.removeCookie('IUSidioma')
                $.cookie("IUSidioma", data.lang);
                $.cookie("IUSidIdioma", data.idIdioma);
                location.reload();
            } else {
                alert("ocurrio un error");
            }
        });
    }