
/*document.getElementById("numberForm").addEventListener("submit", callapi);
*/

currentID = 0;

async function callapi() {

    //e.preventDefault();

    const data = {
        "NumInput": document.getElementById("numInput").value,
        "NumConvertedOutput": "raaaaaaa"
    };

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

    currentID = result.id
    console.log(result);
}

//post and extract the translated number inputted from use
async function extractNumberWord()
{
    await callapi();
    const response = await fetch(`/nums/${currentID}`)

    const result = await response.json();

    const translatedNumberPara = document.getElementById("numbertrans");
    translatedNumberPara.textContent = result.numConvertedOutput;
}