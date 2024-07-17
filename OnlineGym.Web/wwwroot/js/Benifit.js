function DeleteItem(id) {
    url = `/Admin/Benefit/Delete/${id}`;
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
                            text: "Benefit has been deleted. Refreshing...",
                            icon: "success"
                        }).then(() => {
                            // Refresh the index page after showing the success message
                            $.ajax({
                                url: "/Admin/Benefit/Index",
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

var EmpArray = [];

class Benefit {

    selectedValue
    constructor() {
        this.item = '';
    }
}

function loadData() {
    let x = `<div style="text-align:center">Add jobs</div>`;

    for (let i = 0; i < EmpArray.length; i++) {
        x += EmpArray[i].item;
    }

    x += `<div>
            <button onclick="addd()" class="btn btn-primary">+</button>
          </div>`;


    document.getElementById("jobs").innerHTML = x;

    // Re-apply stored values after rendering
    for (let i = 0; i < EmpArray.length; i++) {
        let element = document.getElementById(`JobsId[${i}]`);
        if (element != null && EmpArray[i].selectedValue) {
            element.value = EmpArray[i].selectedValue;
        }
    }
}

function add() {
    console.log("Adding a new job");

    // Save the current state of items
    for (let i = 0; i < EmpArray.length; i++) {
        let element = document.getElementById(`JobsId[${i}]`).parentElement;
        if (element != null) {
            element = element.parentElement;
        }
        if (element != null) {
            EmpArray[i].item = element.outerHTML;
            EmpArray[i].selectedValue = document.getElementById(`JobsId[${i}]`).value;  // Save the current selected value
        }
    }

    let item = new Benefit();
    let template = document.getElementById("Warp").innerHTML;

    template = template.replaceAll("ben", `JobsId[${EmpArray.length}]`);


    let obj = `<div class="form-group">
                <label for="JobsId[${EmpArray.length}]">Job ${EmpArray.length + 1}</label>
                <div style="display:flex;justify-content:center">
                    ${template}
                    <button type="button" onclick="remove(${EmpArray.length})" class="btn btn-danger">X</button>
                </div>
               </div>`;

    item.item = obj;  // Store the HTML structure in the new item
    EmpArray.push(item);
    loadData();
}

function remove(id) {
    console.log("Removing job with id:", id);

    // Save the current state of items
    for (let i = 0; i < EmpArray.length; i++) {
        let element = document.getElementById(`JobsId[${i}]`).parentElement;


        if (element != null) {
            element = element.parentElement;
        }
        if (element != null) {
            EmpArray[i].item = element.outerHTML;
            EmpArray[i].selectedValue = document.getElementById(`JobsId[${i}]`).value;  // Save the current selected value
        }
    }

    // Remove the item at the specified index
    EmpArray.splice(id, 1);


    for (let i = id; i < EmpArray.length; i++) {

        EmpArray[i].item = EmpArray[i].item.replaceAll(`JobsId[${i + 1}]`, `JobsId[${i}]`);
        EmpArray[i].item = EmpArray[i].item.replaceAll(`remove(${i + 1})`, `remove(${i})`);
        EmpArray[i].item = EmpArray[i].item.replaceAll(`Job ${i + 2}`, `Job ${i + 1}`);

    }
    loadData();
}

