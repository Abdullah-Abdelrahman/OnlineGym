

// Get Reference to our Elements  
const number = document.getElementById('number');
const daysPassedValue = document.getElementById('daysPased').value;

const daysPassed = parseInt(daysPassedValue, 10);

function getStyleSheetByName(name) {
    for (let i = 0; i < document.styleSheets.length; i++) {
        const styleSheet = document.styleSheets[i];
        // Check if the href or title matches the provided name
        if (styleSheet.href && styleSheet.href.includes(name)) {
            return styleSheet;
        } else if (styleSheet.title && styleSheet.title === name) {
            return styleSheet;
        }
    }
    return null; // Return null if the stylesheet isn't found
}

// Example usage: Get the stylesheet with a specific name or URL
const styleSheet = getStyleSheetByName('Customer.css');


// If found, you can insert rules or modify it
if (styleSheet) {
    const newKeyframes = `
      @keyframes newAnim {
        100% {
          stroke-dashoffset: ${450-(daysPassed* 450/28)};
        }
      }
    `;
    styleSheet.insertRule(newKeyframes, styleSheet.cssRules.length);

    // Apply the new animation to the element
    const circle = document.getElementById('circle');
    circle.style.animation = 'newAnim 2s forwards';
}










let counter = 0;

const intervalID = setInterval(() => {
    
    if (counter == daysPassed) {
        clearInterval(intervalID); 
    } else {
        counter += 1;
        number.innerHTML = `${counter}/28`;
    }
}, 30);