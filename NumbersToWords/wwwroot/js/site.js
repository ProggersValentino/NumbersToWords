
const form = document.getElementById("ntwForm");
form.addEventListener("submit", extractNumberWord);

const translatedNumberPara = document.getElementById("numbertrans");
const inputField = document.getElementById("numInput");
var submitBtnEl = document.querySelector("button[type=submit]");

currentID = 0;

async function callapi() {


    submitBtnEl.disabled = true;

    const input = document.getElementById("numInput").value;

    const data = {
        "NumInput": input,
        "NumConvertedOutput": ""
    };

    

    try {
        /*manually posting the data to the server*/
        const response = await fetch("/numspost",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

        // waiting for the server to respond to confirm its been done
        const result = await response.json();

        currentID = result.id;
        console.log(result);

        document.getElementById("numInput").value = "";
    }
    catch (error) {
        submitBtnEl.disabled = false;
    }

   

    
;
}

//post and extract the translated number inputted from use
async function extractNumberWord()
{
    inputField.setCustomValidity(``)
  
    translatedNumberPara.textContent = "";
    
    try {
        const postDataResult = await callapi();



        const response = await fetch(`/nums/${currentID}`);

        const result = await response.json();

        translatedNumberPara.textContent = result.numConvertedOutput;

        submitBtnEl.disabled = false;
    }
    catch (error) {
        console.log(error);
        submitBtnEl.disabled = false;

        /*switch (postDataResult.status) {
            case 400:
                translatedNumberPara.textContent = "Please enter a valid number!";
                break;
        }*/
    }
    

}