$(":checkbox[class='selected']").change(function () {
    if ($(":checkbox[class='selected']:checked").length == 1)
        $(':checkbox:not(:checked)').prop('disabled', true);
    else
        $(':checkbox:not(:checked)').prop('disabled', false);
});