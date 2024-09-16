






$(document).ready(function () {
    $.ajax({
        url: "/Coach/Client/GetOrderHasPlan?OrderId=",
        type: "Get",
        success: function (data) {
            if (data.success) {
               
                console.log("yes");
               
            }
            else {
                console.log("No");
              
            }
        }
    });
});
