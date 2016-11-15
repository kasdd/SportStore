var productsView = {

    init: function () {
        //$("form").submit(function () {
        //    $.post(this.action, $(this).serialize(), function (data) {
        //        $("#products").html(data);
        //    });
        //    return false;
        //});

        //Bij aanpassen van waarde in dropdownlist
        $("#CategoryId").change(function () {
            $.post(this.action, $(this).serialize(), function (data) {
                $("#products").html(data);
            });
        });

    }

}

$(function () {
    productsView.init();
});



