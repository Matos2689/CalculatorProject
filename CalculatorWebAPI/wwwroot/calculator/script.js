function append(char) {
    const input = document.getElementById('expression');
    input.value += char;
}

function clearInput() {
    document.getElementById('expression').value = '';
    document.getElementById('result').textContent = '';
}

function backspace() {
    const input = document.getElementById('expression');
    input.value = input.value.slice(0, -1);
}

async function calculate() {
    const input = document.getElementById('expression');
    const expression = input.value;

    const response = await fetch('/api/calculator/calculate', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ expression: expression })
    });

    const json = await response.json();

    let result = "";
    if (json.quantityResult === null) {
        result = json.numericResult;
    } else {
        result = json.quantityResult;
    }

    input.value = result;
}