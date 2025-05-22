




var ClsSettings = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7155/api/Setting", {}, "json",
            function (data) {
                $("#lnkFacebook").attr("href", data.facebookLink);
            }, function () { });
    }
}

ClsSettings.GetAll();