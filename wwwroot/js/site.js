$(document).ready(function () {

    console.log("javascript loaded");
    $('.categories tr').click(function (event) {
        if (event.target.type !== 'checkbox') {
            $(':checkbox', this).trigger('click');
        }
    });

    $("input[type='checkbox']").change(function (e) {
        if ($(this).is(":checked")) {
            $(this).closest('tr').addClass("highlight_row");
        } else {
            $(this).closest('tr').removeClass("highlight_row");
        }
    });

    var modal = $('#myModal');

    $('.item-image').on('click', function () {
        price = $(this).parent().children('.caption').children('.row').find('.price').text() //.parent().prev().find('.price').text();
        productTitle = $(this).parent().children('.caption').children('.product-title').text() //.$(this).parent().parent().prev().text();
        //console.log($(this).parent().parent().prev());
        img = $(this).attr('src') //parent().parent().parent().prev().attr('src');


        modalContent = modal.find('.modal-content');
        modalContent.find('.modal-image').attr('src', img);
        modalContent.find('.modal-item-price').text('Price:' +  price);
        modalContent.find('.modal-item-title').text(productTitle);

        modal.css("display", "block");
    });

    $('.close').on('click', function () {
        modal.css("display", "none");
    });


    window.onclick = function (event) {
        if (event.target == document.getElementById('myModal')) {
            document.getElementById('myModal').style.display = "none";
        }
    } 
});