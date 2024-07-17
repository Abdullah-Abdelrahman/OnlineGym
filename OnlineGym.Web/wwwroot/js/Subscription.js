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

// Initial call to load data if necessary
loadData();
