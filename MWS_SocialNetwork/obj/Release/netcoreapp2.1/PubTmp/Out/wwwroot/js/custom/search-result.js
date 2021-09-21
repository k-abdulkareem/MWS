
function findContact(event){
    $('div#ResultContainer').empty();
    event.preventDefault();
    var search = $("#searchString").val();
   debugger
    var dataobj = { searchString: search };
 
    $.ajax({
        type: 'POST',
        dataType: "html",
        data: dataobj,
        success: function (response) {
            debugger
            console.log("success");
            $('div#ResultContainer').html(response); 
        },
        error: function (jqXHR, status, err) {
            debugger
            console.log(jqXHR.responseText);
            $('div#ResultContainer').empty();
        }
    });
}
