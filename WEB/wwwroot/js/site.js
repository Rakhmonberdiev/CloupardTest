// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getProductById(id) {
    $.ajax({
        url: '/Product/GetById',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            $('#modalContent').html(data);
            $('#myModal').modal('show');
        },
        error: function () {

        }
    });
}
