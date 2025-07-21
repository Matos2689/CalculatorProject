// -------- Dark/Light Mode --------
function toggleTheme() {
    document.body.classList.toggle("dark");
    let btn = document.getElementById('theme-toggle');
    if (document.body.classList.contains("dark")) {
        btn.innerText = "☀️ Light Mode";
    } else {
        btn.innerText = "🌙 Dark Mode";
    }
}

// -------- Calculator --------
let lastWasResult = false;
let history = [];

function press(val) {
    const display = document.getElementById('display');
    if (lastWasResult) {
        if (['+', '-', '*', '/'].includes(val)) {
            display.value += val;
        } else {
            display.value = val;
        }
        lastWasResult = false;
    } else {
        display.value += val;
    }
}
function clearDisplay() {
    document.getElementById('display').value = '';
    lastWasResult = false;
}

async function calc() {
    const display = document.getElementById('display');
    const expression = display.value;
    if (!expression) return;
    const resp = await fetch('/api/calculator/calculate', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ expression })
    });
    const data = await resp.json();
    let showResult = "";
    if (data.error) {
        showResult = "Erro!";
        lastWasResult = true;
    } else if (data.unitResult) {
        showResult = `${data.unitResult}`;
        lastWasResult = true;
        addToHistory(expression, showResult);
    } else {
        showResult = `${data.numericResult}`;
        lastWasResult = true;
        addToHistory(expression, showResult);
    }
    display.value = showResult;
}

// -------- Memory --------
function addToHistory(expr, res) {
    history.unshift({ expr, res });
    if (history.length > 20) history = history.slice(0, 20);
    renderHistory();
}
function renderHistory() {
    const list = document.getElementById('history-list');
    list.innerHTML = '';
    if (history.length === 0) {
        list.innerHTML = '<li style="text-align:center;color:#888">Histórico vazio</li>';
    } else {
        history.forEach(item => {
            const li = document.createElement('li');
            li.textContent = `${item.expr} = ${item.res}`;
            list.appendChild(li);
        });
    }
}
function clearHistory() {
    history = [];
    renderHistory();
}

function backspace() {
    const display = document.getElementById('display');
    if (lastWasResult) {
        clearDisplay();
    } else {
        display.value = display.value.slice(0, -1);
    }
}

// -------- Physical Keyboard --------
document.addEventListener('keydown', function (e) {
    if ((e.key >= '0' && e.key <= '9') || ['+', '-', '*', '/', '.', 'm', 'c'].includes(e.key)) {
        press(e.key);
    } else if (e.key === 'Enter' || e.key === '=') {
        calc();
    } else if (e.key === 'Backspace') {
        let display = document.getElementById('display');
        if (lastWasResult) {
            clearDisplay();
        } else {
            display.value = display.value.slice(0, -1);
        }
    }
});

renderHistory();
