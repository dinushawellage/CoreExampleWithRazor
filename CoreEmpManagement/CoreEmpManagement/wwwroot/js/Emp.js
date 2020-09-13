var dataTable;

$(document).ready(function () {
    loadDatatable();
});

function loadDatatable() {
    dataTable = $('#dtEmp').DataTable({
        "ajax": {
            "url": "/api/employee",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "age", "width": "20%" },
            { "data": "location", "width": "20%" },
            { "data": "salary", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div >
                                <a href="/Employees/Edit?id=${data}" class='btn btn-success'>Edit</a>&nbsp;
                                <a onClick="Delete('api/Employee?id='+${data})" class='btn btn-danger'>Delete</a>
                            </div>`
                },
                "width": "20%"
            },
        ],
        "language":
        {
            "emptyTable": "No data found"
        },
        "width": "100%"
    }
    );
}

function Delete(url) {
    swal(
        {
            "title": "Are you sure?",
            "text": "Once you delete, it can not be recovered.",
            "icon": "warning",
            "dangerMode":true
        }).then(del => {
            if (del) {
                $.ajax(
                    {
                        url: url,
                        type: "DELETE",
                        success: function (data) {
                            if (data.success) {
                                toastr.success(data.message);
                                dataTable.ajax.reload();
                            }
                            else {
                                toastr.error(data.message);
                            }
                        }
                }
                );
            }
        }
        );
}
