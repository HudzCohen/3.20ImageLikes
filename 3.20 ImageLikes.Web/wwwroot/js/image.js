
$(() => {

    setInterval(() => {
        getLikes();
       // isLiked();
    }, 1000);


    $("#like-btn").on('click', function () {
        const id = $(this).data('image-id');
        $.post("/home/incrementlikes", { id }, function () {
        });
        getLikes();
        isLiked();
    });


    function getLikes() {
        const id = $("#image-id").val();
        $.get("/home/getlikesbyimageid", { id }, function (likes) {
            $("#likes-count").text(likes);
            //console.log({ likes });
        });
    }

    function isLiked() {
        var id = $("#image-id").val();
        $.get("/home/isimageliked", { id }, function (isLiked) {
            if (isLiked) {
                $("#like-btn").prop('disabled', true);
            }
        });
        
    }
});

