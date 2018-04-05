function attachAllEvents() {
    $('#linkHome').on('click', showHomeView);
    $('#linkLogin').on('click', showLoginView);
    $('#linkRegister').on('click', showRegisterView);
    $("#linkLogout").on('click', logoutUser);
    $("#linkListAds").on('click', listAds);
    $("#linkCreateAd").on('click', showCreateAdView);

    $("#buttonRegisterUser").on('click', registerUser);
    $("#buttonLoginUser").on('click', loginUser);
    $("#buttonCreateAd").on('click', createAd);
    $("#buttonEditAd").on('click', editAd);

    $("#infoBox, #errorBox").on('click', function () {
        $(this).fadeOut()
    });

    $(document).on({
        ajaxStart: function () {
            $("#loadingBox").show()
        },
        ajaxStop: function () {
            $("#loadingBox").hide()
        }
    });
}