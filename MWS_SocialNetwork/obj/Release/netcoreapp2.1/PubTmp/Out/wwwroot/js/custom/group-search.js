
$("#GroupSearch").on("click", function (e) {
    $('div#SearchResultContainer').empty();
        e.preventDefault();
    var search = $("#searchString").val();
     
    var dataobj = { searchString: search };
       
        $.ajax({
            type: 'POST',
            dataType: "html",
            data: dataobj,
            success: function (response) {
                console.log("success");
                $('div#SearchResultContainer').html(response);
            },
            error: function (jqXHR, status, err) {
                console.log(jqXHR.responseText);
                $('div#SearchResultContainer').empty();
            }
        });
    });
