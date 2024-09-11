var BenArray = [];

class Subscription
{
    
    selectedValue
    constructor() {
        this.item = '';
    }
}

function loadData() {
    let x = `<div style="text-align:center">Add Benefits</div>`;

    for (let i = 0; i < BenArray.length; i++) {
        x += BenArray[i].item;
    }

    x += `<div>
            <button onclick="add()" class="btn btn-primary">+</button>
          </div>`;


    document.getElementById("Benefits").innerHTML = x;

    // Re-apply stored values after rendering
    for (let i = 0; i < BenArray.length; i++) {
        let element = document.getElementById(`BenefitIDs[${i}]`);
        if (element !=null && BenArray[i].selectedValue) {
            element.value = BenArray[i].selectedValue;
        }
    }
}

function add() {
    console.log("Adding a new benefit");

    // Save the current state of items
    for (let i = 0; i < BenArray.length; i++) {
        let element = document.getElementById(`BenefitIDs[${i}]`).parentElement;
        if (element != null) {
            element = element.parentElement;
        }
        if (element !=null) {
            BenArray[i].item = element.outerHTML;
            BenArray[i].selectedValue = document.getElementById(`BenefitIDs[${i}]`).value;  // Save the current selected value
        }
    }

    let item = new Subscription();
    let template = document.getElementById("Warp").innerHTML;

    template = template.replaceAll("ben", `BenefitIDs[${BenArray.length}]`);


    let obj = `<div class="form-group">
                <label for="BenefitIDs[${BenArray.length}]">Benefit ${BenArray.length + 1}</label>
                <div style="display:flex;justify-content:center">
                    ${template}
                    <button onclick="remove(${BenArray.length})" class="btn btn-danger">X</button>
                </div>
               </div>`;

    item.item = obj;  // Store the HTML structure in the new item
    BenArray.push(item);
    loadData();
}

function remove(id) {
    console.log("Removing benefit with id:", id);

    // Save the current state of items
    for (let i = 0; i < BenArray.length; i++) {
        let element = document.getElementById(`BenefitIDs[${i}]`).parentElement;


        if (element != null) {
            element = element.parentElement;
        }
        if (element != null) {
            BenArray[i].item = element.outerHTML;
            BenArray[i].selectedValue = document.getElementById(`BenefitIDs[${i}]`).value;  // Save the current selected value
        }
    }

    // Remove the item at the specified index
    BenArray.splice(id, 1);


    for (let i = id; i < BenArray.length; i++) {
       
        BenArray[i].item = BenArray[i].item.replaceAll(`BenefitIDs[${i + 1}]`, `BenefitIDs[${i}]`);
        BenArray[i].item = BenArray[i].item.replaceAll(`remove(${i + 1})`, `remove(${i})`);
        BenArray[i].item = BenArray[i].item.replaceAll(`Benefit ${i + 2}`, `Benefit ${i+1}`);

    }
    loadData();
}




let Activated = 0;


function start(s) {


    for (let i = 0; i < s; i++) {
        let element = document.getElementById(`BenefitIDs[${i}]`).parentElement;
        if (element != null) {
            element = element.parentElement;
        }
        if (element != null) {
            BenArray.push(new Subscription());
            BenArray[i].item = element.outerHTML;
            BenArray[i].selectedValue = document.getElementById(`BenefitIDs[${i}]`).value;  // Save the current selected value
        }
    }

    $.ajax({
        url: `/Admin/Subscription/ChangeState/${-1}`,
        type: "GET",
        success: function (data) {
            if (data.success) {

                let parsedData = JSON.parse(data.data);

                console.log(parsedData.count);
                   ShapechangeState(parsedData.count);
                   Activated = parsedData.count;
            } 
        }
    });
}







function ChangeState(id) {

    var btn = document.getElementById(`usebtn[${id}]`);
    if (Activated == 3 && btn.classList.contains("use")) {
        toastr.error("you cant Activate more than 3!!");
    }
    else {
        $.ajax({
            url: `/Admin/Subscription/ChangeState/${id}`,
            type: "GET",
            success: function (data) {
                if (data.success) {
                    
                    toastr.success(data.message);
                    Swal.fire({
                        title: "Success!",
                        text: data.message,
                        icon: "success"
                    }).then(() => {
                        let parsedData = JSON.parse(data.data);


                        if (btn.classList.contains("use")) {
                            btn.classList.remove("use");
                            btn.classList.add("disuse");
                            btn.innerText = "DISUSE";
                        }
                        else {
                            btn.classList.remove("disuse");
                            btn.classList.add("use");
                            btn.innerText = "USE";
                        }
                        ShapechangeState(parsedData.count);
                        Activated = parsedData.count;
                    });
                } else {
                    toastr.error(data.message);
                }
            }
        });
    }

  

}







function ShapechangeState(currentState) {

    console.log(currentState);
    const diamond = document.getElementById('diamond');
    const percentage = document.getElementById('percentage');
    

    switch (currentState) {
        case 0:
            diamond.className = 'state0';
            percentage.textContent = '0/3';
            break;
        case 1:
            diamond.className = 'state1';
            percentage.textContent = '1/3';
            break;
        case 2:
            diamond.className = 'state2';
            percentage.textContent = '2/3';
            break;
        case 3:
            diamond.className = 'state3';
            percentage.textContent = '3/3';
            break;

    }
}


