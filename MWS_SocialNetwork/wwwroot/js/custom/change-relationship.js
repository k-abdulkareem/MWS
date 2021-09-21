
$("#rId").on("change", function (e) {
    
    e.preventDefault();


    var rIdValue =this.options[this.selectedIndex].value;
     
    var dataobj = { rId: rIdValue };
   
        $.ajax({
            type: 'POST',
            dataType: "html",
            data: dataobj,
            success: function (response) {
                console.log("success");
                $('div#RelationshipContainer').empty();
                $('div#RelationshipContainer').html(response);
                toastr.success('Successfully Changed Relationship');
            },
            error: function (jqXHR, status, err) {
                console.log(jqXHR.responseText);
                $('div#RelationshipContainer').empty();
                toastr.error('Failed Changed Relationship');
            }
        });
    });
