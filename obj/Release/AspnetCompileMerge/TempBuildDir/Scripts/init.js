(function () {
    'use strict';

    $('.datepicker').datepicker()

    $('[title]').tooltip();

    $(".copyButton").click(function () {

        $(this).focus().select();

        // copy the selection
        var succeed;
        try {
            succeed = document.execCommand("copy");
        } catch (e) {
            succeed = false;
        }
        if (succeed)
            toastr.success('Text Copied!');
        else
            toastr.warning('Could Not Copy Text!');
    });

    $("#Src").keyup(function () {
        var txt = $("#Src").val();
        if (txt.length < 8) {
            $(".not-available").show();
            $(".available").hide();
        } else {

            $.ajax({
                url: "/Forwards/Available",
                context: txt
            }).done(function (data) {
                var isAvailable = jQuery.parseJSON(data);
                if (isAvailable) {
                    $(".available").show();
                    $(".not-available").hide();
                }
                else {
                    $(".available").hide();
                    $(".not-available").show();
                }
            });
        }

    });

}())