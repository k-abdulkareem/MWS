
$("#CreateGroup").on("click", function (e) {
    $('div#CreateResultContainer').empty();
    e.preventDefault();

    var model = {
        Name: $("#GroupName").val(),
        Image: $("#GroupPhoto").files[0],
        MyContacts: null,
        InvitedContacts :null
    }
    
   
    var dataobj = { CreateGroupModel: model };
       
    $.ajax({
            url: 'Create',
            type: 'POST',
            dataType: "html",
            data: dataobj,
            success: function (response) {
                console.log("success");
               // $('div#CreateResultContainer').html(response);
                location.reload();
                toastr.success('Successfully Create Group And Send Invitations');
            },
            error: function (jqXHR, status, err) {
                console.log(jqXHR.responseText);
                //$('div#CreateResultContainer').empty();
                location.reload();
                toastr.error('Something went wrong');
            }
        });
    });
