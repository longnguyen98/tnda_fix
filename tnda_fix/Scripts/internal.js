function updateAvatar() {
    $.LoadingOverlay('show');
    var formData = new FormData();
    var file = document.getElementById('file_input').files[0];
    formData.append('file', file);
    formData.append('id', id);
    $.ajax({
        url: '/person/setImg',
        method: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            $.LoadingOverlay('hide');
            if (response.success) {
                window.location.reload();
            } else {
                jAlert('ERROR ' + response.message);
            }
        }
    });
}