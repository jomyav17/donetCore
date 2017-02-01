(function () {
    $('#toggle-sidebar').on('click', function () {
        $('#sidebar').toggleClass('display-sidebar');
        $('#wrapper').toggleClass('display-sidebar');
        $icon = $('#toggle-sidebar i');
        if ($('#sidebar').hasClass('display-sidebar')) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        }
        else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });

})();