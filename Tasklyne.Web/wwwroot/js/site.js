// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Show in popup
$(function (){
    window.showInPopup = (url, title) =>{
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#form-modal .modal-body').html(res);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal('show');
            }
        });
    };
    $(document).on('submit', '#form-modal form', function (e){
        e.preventDefault();
        var form = $(this);
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        $.post(actionUrl, dataToSend).done(function (res){
            if (res.isValid) {
                // $('#view-all').html(res.html);
                // $('#form-modal .modal-body').html('');
                // $('#form-modal .modal-title').html('');
                $('#form-modal').modal('hide');
                location.reload();
            }
            else {
                $('#form-modal .modal-body').html(res.html);
            }
        });
    })
});