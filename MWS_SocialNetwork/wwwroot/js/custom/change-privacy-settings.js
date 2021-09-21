
$('#SavePrivacySettings').on('click', function (e) {
        e.preventDefault();
    //owner Model
    var r1 = [];
    $.each($("#Owner-Relationship option:selected"), function () { r1.push($(this).val()); });
    var re1 = [];
    $.each($("#Owner-Relationship-Except option:selected"), function () { re1.push($(this).val()); });
    var g1 = [];
    $.each($("#Owner-Groups option:selected"), function () { g1.push($(this).val()); });
    var ge1 = [];
    $.each($("#Owner-Groups-Except option:selected"), function () { ge1.push($(this).val()); });
    var i1 = [];
    $.each($("#Owner-Individual option:selected"), function () { i1.push($(this).val()); });
        var ownerModel = {
            Relationships: r1,
            ExceptRelationshipsContacts: re1,
            Groups: g1,
            ExceptGroupsMembers: ge1,
            Individuals:i1
    };
    //Contributor Model
    var r2 = [];
    $.each($("#Contributor-Relationship option:selected"), function () { r2.push($(this).val()); });
    var re2 = [];
    $.each($("#Contributor-Relationship-Except option:selected"), function () { re2.push($(this).val()); });
    var g2 = [];
    $.each($("#Contributor-Groups option:selected"), function () { g2.push($(this).val()); });
    var ge2 = [];
    $.each($("#Contributor-Groups-Except option:selected"), function () { ge2.push($(this).val()); });
    var i2 = [];
    $.each($("#Contributor-Individual option:selected"), function () { i2.push($(this).val()); });
    var contributorModel = {
        Relationships: r2,
        ExceptRelationshipsContacts: re2,
        Groups: g2,
        ExceptGroupsMembers: ge2,
        Individuals: i2
    };
    //Tagged Model
    var r3 = [];
    $.each($("#Tagged-Relationship option:selected"), function () { r3.push($(this).val()); });
    var re3 = [];
    $.each($("#Tagged-Relationship-Except option:selected"), function () { re3.push($(this).val()); });
    var g3 = [];
    $.each($("#Tagged-Groups option:selected"), function () { g3.push($(this).val()); });
    var ge3 = [];
    $.each($("#Tagged-Groups-Except option:selected"), function () { ge3.push($(this).val()); });
    var i3 = [];
    $.each($("#Tagged-Individual option:selected"), function () { i3.push($(this).val()); });
    var taggedModel = {
        Relationships: r3,
        ExceptRelationshipsContacts: re3,
        Groups: g3,
        ExceptGroupsMembers: ge3,
        Individuals: i3
    };
    //ShareHolder Model
    var r4 = [];
    $.each($("#ShareHolder-Relationship option:selected"), function () { r4.push($(this).val()); });
    var re4 = [];
    $.each($("#ShareHolder-Relationship-Except option:selected"), function () { re4.push($(this).val()); });
    var g4 = [];
    $.each($("#ShareHolder-Groups option:selected"), function () { g4.push($(this).val()); });
    var ge4 = [];
    $.each($("#ShareHolder-Groups-Except option:selected"), function () { ge4.push($(this).val()); });
    var i4 = [];
    $.each($("#ShareHolder-Individual option:selected"), function () { i4.push($(this).val()); });
    var shareHolderModel = {
        Relationships: r4,
        ExceptRelationshipsContacts: re4,
        Groups: g4,
        ExceptGroupsMembers: ge4,
        Individuals: i4
    };
    //Main Model
    var viewModel = {
        OwnerPrivacySettings: ownerModel,
        ContributorPrivacySettings: contributorModel,
        TaggedPrivacySettings: taggedModel,
        ShareHolderPrivacySettings: shareHolderModel
    };

    var dataobj = { model: viewModel, operation: "PrivacySettings" };
       
        $.ajax({
            type: 'POST',
            dataType: "html",
            data: dataobj,
            traditional : true,
            success: function (response, status, xhr) {
                toastr.success('Successfully update privacy setttings');
            },
          
            error: function (jqXHR, status, err) {
                console.log(jqXHR.responseText);
                toastr.error('Failed update privacy settings');
            }
        });
    });

