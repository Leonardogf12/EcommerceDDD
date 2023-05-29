var objectSale = new Object();


objectSale.AddProductInCart = function (idProduct) {

    var name = $("#name_" + idProduct).val();
    var qty = $("qty_" + idProduct).val();

    $.ajax({
        type: 'POST',
        url: 'api/AddProductCart',
        dataType: 'JSON',
        cache: false,
        async: true,
        data: { "id": idProduct, "name": name, "qtd": qty },
        success: function (data) {
          
        }
    });

}

objectSale.LoadProducts = function (){

    $.ajax({

        type: 'GET',
        url: 'api/ListProductsWithStock',
        dataType: 'JSON',
        cache: false,
        async: true,
        success: function (data) {

            var htmlContent = "";

            data.forEach(function (entitie) {

                htmlContent += "<div class='col-xs-12 col-sm-4 col-md-4 col-lg-4'>";

                var idName = "name_" + entitie.id;
                var idQty = "qty_" + entitie.id;

                htmlContent += "<label id='id=" + idName + "'> Produto:" + entitie.name + "</label></br>";
                htmlContent += "<label Valor:" + entitie.value +"</label></br>";
                htmlContent += "Quantidade: <input type='number' value='1' id='" + idQty + "'>";
                htmlContent += "<input type='button' onclick='objectSale.AddProductInCart(" + entitie.id + ")' value='Comprar'></br>";
                htmlContent +="</div>"
            });

            $("#divSales").html(htmlContent);

          
        }
    });

}

$(function () {
    objectSale.LoadProducts();
});