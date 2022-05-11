// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".statuslabel").each(function( index, value ){
    var text = $(this).text().trim();
    if (text == "Not Started"){
        $(this).css("color", "yellow");
    }else if(text == "In Progress"){
        $(this).css("color", "green");
    }else{
        $(this).css("color", "red");
    }
});