$(document).ready(function () {
    $(document).on("keyup", ".txtBusqueda", function (e) {
        
        if ($(this).val() == "") {
            $(".folder").removeClass("hidden");
        } else {
            //if (!$(".folder").hasClass("hidden")) {
                $(".folder").addClass("hidden");
            //}
            var folders = $(".folder .folderTitle:contains(" + $(this).val() + ")");
            folders = folders.parents(".folder");
            //folders.css("background", "yellow");
            folders.removeClass("hidden");
            /*folders.each(function (i, folder) {
                folder = folder.parents(".folder");
                console.log("este es", folder);
            })*/
        }
        
    })
})