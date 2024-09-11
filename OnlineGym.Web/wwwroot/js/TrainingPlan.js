
function openn(id) {
    
    var hiddenPart = document.getElementById(id);
    if (hiddenPart) {
        if (hiddenPart.hidden == true) {
            hiddenPart.hidden = false;

        }
        else {
            hiddenPart.hidden = true;

        }
    } else {
        console.error('Element not found for id:', id);
    }
}




class Day {

    exerciseList
    constructor() {
        this.exerciseList = [];
    }
}

class Exercise {

    selectedValue
    constructor() {
        this.item = '';
    }
}
var weekArray = [];
function loadData(week,day) {
    let x = `<div style="text-align:center">Add Exerceses</div>`;

    for (let i = 0; i < weekArray[week - 1][day].exerciseList.length; i++) {
        x += weekArray[week - 1][day].exerciseList[i].item;
    }

    x += `
    <button style="margin-left: 42%;" class="btn btn-primary" onclick="add(${week},${day})" type="button">+</button>`;


    document.getElementById(`week${week}HiddenPartday-${day}`).innerHTML = x;

    // Re-apply stored values after rendering
    for (let i = 0; i < weekArray[week - 1][day].exerciseList.length; i++) {

        let element = document.getElementById(`DayExercisesIds[${(week - 1) * 7 + day}][${i}]`);
        if (element != null && weekArray[week - 1][day].exerciseList[i].selectedValue) {
            element.value = weekArray[week - 1][day].exerciseList[i].selectedValue;
        }
    }
}

function add(week,day) {
    
    // Save the current state of items
    for (let i = 0; i < weekArray[week - 1][day].exerciseList.length; i++) {

        let element = document.getElementById(`DayExercisesIds[${(week - 1) * 7 + day}][${i}]`).parentElement;
        if (element != null) {
            element = element.parentElement;
        }
        if (element != null) {
            weekArray[week - 1][day].exerciseList[i].item = element.outerHTML;
            weekArray[week - 1][day].exerciseList[i].selectedValue = document.getElementById(`DayExercisesIds[${(week - 1) * 7 + day}][${i}]`).value;  // Save the current selected value
        }
    }

    let item = new Exercise();
    let template = document.getElementById("Warp").innerHTML;

    template = template.replaceAll("ben", `DayExercisesIds[${(week - 1) * 7 + day}][${weekArray[week - 1][day].exerciseList.length}]`);


    let obj = `<div class="form-group">
                <label for="DayExercisesIds[${(week - 1) * 7 + day}][${weekArray[week - 1][day].exerciseList.length}]">Exersice ${weekArray[week - 1][day].exerciseList.length + 1}</label>
                <div style="display:flex;justify-content:center">
                    ${template}
                    <button onclick="remove(${week},${day},${weekArray[week - 1][day].exerciseList.length})" class="btn btn-danger">X</button>
                </div>
               </div>`;

    item.item = obj;  // Store the HTML structure in the new item
    weekArray[week-1][day].exerciseList.push(item);
    loadData(week,day);
}

function remove(week,day,id) {
    console.log("Removing benefit with id:", week," ", day," ", id);

    // Save the current state of items
    for (let i = 0; i < weekArray[week - 1][day].exerciseList.length; i++) {

        let element = document.getElementById(`DayExercisesIds[${(week - 1) * 7 + day}][${i}]`).parentElement;
        if (element != null) {
            element = element.parentElement;
        }
        if (element != null) {
            weekArray[week - 1][day].exerciseList[i].item = element.outerHTML;
            weekArray[week - 1][day].exerciseList[i].selectedValue = document.getElementById(`DayExercisesIds[${(week - 1) * 7 + day}][${i}]`).value;  // Save the current selected value
        }
    }

    // Remove the item at the specified index
    weekArray[week - 1][day].exerciseList.splice(id, 1);


    for (let i = id; i < weekArray[week - 1][day].exerciseList.length; i++) {

        weekArray[week - 1][day].exerciseList[i].item = weekArray[week - 1][day].exerciseList[i].item.replaceAll(`DayExercisesIds[${(week - 1) * 7 + day}][${i + 1}]`, `DayExercisesIds[${(week - 1) * 7 + day}][${i}]`);
        weekArray[week - 1][day].exerciseList[i].item = weekArray[week - 1][day].exerciseList[i].item.replaceAll(`remove(${week},${day},${i + 1})`, `remove(${week},${day},${i})`);
        weekArray[week - 1][day].exerciseList[i].item = weekArray[week - 1][day].exerciseList[i].item.replaceAll(`Exersice ${i + 2}`, `Exersice ${i + 1}`);

    }
    loadData(week,day);
}




function start(s) {
    //add 28 day
    for (var i = 0; i < 4; i++) {

        var list = [];
        for (var j = 0; j < 7; j++) {
            var item = new Day();
            item.exerciseList = [];

            let exer = new Exercise();
          



            let obj = `
            <div class="form-group" hidden>
                <div style="display:flex;justify-content:center">
                   <input id="DayExercisesIds[${(i) * 7 + j}][${0}]" name="DayExercisesIds[${(i) * 7 + j}][${0}]" value="0" hidden/>

                </div>
               </div>
              
                `;

            exer.item = obj; 


            item.exerciseList.push(exer);
            list.push(item);

        }
       
       
        weekArray.push(list);
        for (var j = 0; j < 7; j++) {
            
            loadData(i + 1, j);
        }

    }

 
    


}

start();






