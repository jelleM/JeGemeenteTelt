$(document).ready(function(){
    $(".text-box").click(function () {
        this.select();
        console.log("textbox clicked")
    });
    $("#salary").click(function () {
        this.select();
    });
    $(".search-city").click(function () {
        this.select();
    });
});
