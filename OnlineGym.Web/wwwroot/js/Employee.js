var dataTable;


function loadData() {

    dataTable = $("#EmpTable").DataTable({
        "ajax": {
            "url": "/Admin/Employee/GetData",
            "dataSrc": "data"
           
        },
        "columns": [
            { "data": "employeeId" },
            { "data": "name" },
            { "data": "email" },
            { "data": "phone" },
            {
                "data": "jobTitle",
                "render": function (data, type, row) {
                    return data ? data.jopName : ''; // Safely access jobTitle.jopName
                }
            },
            {
                "data": "employeeId",
                "render": function (data) {
                    return `
                    <a href="/Admin/Employee/Details/${data}" class="btn btn-info">More</a>

                    <a href="/Admin/Employee/Update/${data}" class="btn btn-success">update</a>
                    <a href="/Admin/Employee/Bouns/${data}" class="btn btn-warning">Bouns</a>

                    <a onClick=DeleteItem("/Admin/Employee/Delete/${data}") class="btn btn-danger">delete</a>
                    `
                }

            }
          
        

        ]
      
    });
  

}

$(document).ready(function () {
    loadData(); 
});



function DeleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        console.log("yes");
                        toastr.success(data.message);
                    }
                    else {
                        console.log("No");
                        toastr.error(data.message);
                    }
                }
            });
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}