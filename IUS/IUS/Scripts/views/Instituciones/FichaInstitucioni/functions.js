function iniciales() {
    var idContinente = $(".txtHdIdContinente").val();
    $(".menuLateral").find("#" + idContinente + "").addClass("activeInstitucion");
    console.log("El id del contenido es: ", idContinente);

    

    $(".menuLateral").find("#" + idContinente + "").find(".imgMap").attr("src", $(".menuLateral").find("#" + idContinente + "").find(".txtHdRoja").val());
    $(this).addClass("activeInstitucion");
}