$(function () {
    $("#tblDepartments").DataTable();
    
    $("#tblDepartments").on("click", ".btnDeleteDepartment", function () {
        var btn = $(this);
        bootbox.confirm("This department will be deleted permanently. Are you sure?", function (result) {
            if (result)
            {
                var id = btn.data("id");
                $.ajax({
                    type: "GET",
                    url: "/Department/Delete/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                    }
                });
            }

        })
    });
});

$(function () {
    $("#tblPersonnels").DataTable();
});