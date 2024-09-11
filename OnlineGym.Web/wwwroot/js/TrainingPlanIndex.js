

function openSelectedmodal(id) {
   
    $(`#GIModal-${id}`).modal('show');
    

    $(`#closeGIButton-${id}`).on('click', function () {
        $(`#GIModal-${id}`).modal('toggle');
    });
    $(`#MaincloseGIButton-${id}`).on('click', function () {
        $(`#GIModal-${id}`).modal('toggle');
    });
}



function DeleteItem(id) {
    url = `/Coach/TrainingPlan/Delete/${id}`;
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
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        Swal.fire({
                            title: "Deleted!",
                            text: "Plan has been deleted. Refreshing...",
                            icon: "success"
                        }).then(() => {
                            // Refresh the index page after showing the success message
                            $.ajax({
                                url: "/Coach/TrainingPlan/Index",
                                type: "GET",
                                success: function () {
                                    location.reload(); // This will reload the page to reflect the changes
                                }
                            });
                        });
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
