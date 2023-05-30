var objectSale = new Object();


objectSale.AddProductInCart = function (idProduct) {

    var name = $("#name_" + idProduct).val();
    var qty = $("#qty_" + idProduct).val();
  
    $.ajax({
        type: 'POST',
        url: 'api/AddProductCart',
        dataType: 'JSON',
        cache: false,
        async: true,
        data: { "id": idProduct, "name": name, "qty": qty },
        success: function (data) {
            if (data.success) {
                alert("ADICIONADO.");
            } else {
                alert("USUARIO NAO LOGADO.");
            }
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

                htmlContent += "<label id='" + idName + "'> Produto: " + entitie.name + "</label></br>";                
                htmlContent += "<label> Valor: " + entitie.value +"</label></br>";
                htmlContent += "Quantidade : <input class='form-control mt-1' type='number' value='1' id='" + idQty + "'>";
                htmlContent += "<input type='button' class='btn btn-primary mt-3' onclick='objectSale.AddProductInCart(" + entitie.id + ")' value ='Comprar'> </br> ";
                htmlContent +="</div>"
            });
           
            $("#divSales").html(htmlContent);

          
        }
    });

}

$(function () {
    objectSale.LoadProducts();
});