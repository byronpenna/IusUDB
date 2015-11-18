$(document).ready(function () {
    console.log("yeaah :D ");
    $(document).on("mouseover", ".divMenu", function () {
        console.log("entro");
        var panel = $(this).find(".panelMenuOp");
        var footer = $(this).find(".footerMenu");
        //panel.css("border-color", "wheat")
        panel.css("border-width", "medium")
        footer.css("background", "#ACC6DD");
        footer.css("color", "white");
    })
    $(document).on("mouseleave", ".divMenu", function () {
        console.log("salio");
        var footer = $(this).find(".footerMenu");
        var panel = $(this).find(".panelMenuOp");
        panel.attr("style", "");
        footer.attr("style", "");
    })
})