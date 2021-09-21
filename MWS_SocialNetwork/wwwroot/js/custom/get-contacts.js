var el = document.getElementsByClassName("Relationship");

for (var i = 0; i < el.length; i++) {
    var elment ="#" + document.getElementsByClassName('Relationship')[i].id;
    $(elment).on('change', function (e) {
        var x=1;
        switch (elment)
        {
            case "#Owner-Relationship":
                {
                    var selectedRelationships = $(this).children("option:selected").val();
                    var except = $("#Owner-Relationship-Except");
                    except.empty();
                    if (selectedRelationships != null && selectedRelationships != '')
                    {
                        $.getJSON('@Url.Action("GetRelationshipsContacts")', { relationships: selectedRelationships, type: "Owner" }, function (excepts) {
                            if (excepts != null && !jQuery.isEmptyObject(excepts))
                            {
                                $.each(excepts, function (index, ex) {
                                    except.append($('<option/>', {
                                        value: ex.Value,
                                        text: ex.Text
                                    }));
                                });
                            }
                        });
                    }
                    break;
                }
            default:
                break;
        }

      
      
       
        
    });

}
