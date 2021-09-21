
$("#Membership").on("change", function (e) {
    e.preventDefault();
    var value =this.options[this.selectedIndex].value;
    var dataobj = { value: value };
        $.ajax({
            type: 'POST',
            dataType: "html",
            data: dataobj,
            success: function (response) {
                console.log("success");
                $('div#MembershipContainer').empty();
                $('div#MembershipContainer').html(response);
                toastr.success('Successfully Changed Membership');
            },
            error: function (jqXHR, status, err) {
                console.log(jqXHR.responseText);
                $('div#MembershipContainer').empty();
                toastr.error('Failed Changed Membership');
            }
        });
    });
