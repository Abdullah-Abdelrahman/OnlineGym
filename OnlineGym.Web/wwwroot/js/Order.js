var dataTable;


function loadData() {

    dataTable = $("#OrderTable").DataTable({
        "ajax": {
            "url": "/Admin/Order/GetData",
            "dataSrc": "data"

        },
        "columns": [
            { "data": "clientSubscriptionId" },
            { "data": "client.name" },
            { "data": "client.email" },
            { "data": "subscription.subscriptionName" },
            { "data": "subscription.price" },
            { "data":"price"},
            { "data": "status" },
            {
                "data": "clientSubscriptionId",
                "render": function (data) {
                    return `
                    <a href="/Admin/Order/Details/${data}" class="btn btn-info">More</a>

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