$(document).ready(function () {
    console.log("Fue un click");
    // click
        $(document).on("click", ".bloqueLink", function () {

            window.location.href = $(this).find(".txtHdUrlLink").val();
        })
})