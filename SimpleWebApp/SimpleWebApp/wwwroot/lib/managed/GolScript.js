
let grid;
let cols;
let rows;
let resolution = 40;

const c = document.getElementById("myCanvas");
const ctx = c.getContext("2d");
let scale = 1.0;

//Timer
var timer = null;
interval = 1000;

function make2DArray(col, row) {
    let arr = new Array(col);
    for (let i = 0; i < arr.length; i++) {
        arr[i] = new Array(row);
    }
    return arr;
}

$("#start").click(function () {
    if (timer !== null) return;
    timer = window.setInterval(runSimulation, interval);
    $("#start").prop('disabled', true);
    $("#reset").prop('disabled', true);
    $("#generate").prop('disabled', true);
    $("#stop").prop('disabled', false);
});

$("#stop").click(function () {
    clearInterval(timer);
    timer = null;
    $("#stop").prop('disabled', true);
    $("#start").prop('disabled', false);
    $("#reset").prop('disabled', false);
    $("#generate").prop('disabled', false);
});

$("#reset").click(function () {
    if (timer !== null) return;
    setup();
    timer = window.setInterval(runSimulation, interval);
    $("#start").prop('disabled', true);
    $("#reset").prop('disabled', true);
    $("#generate").prop('disabled', true);
    $("#stop").prop('disabled', false);
});

$("#generate").click(function () {
    if (timer !== null) return;
    createPattern();
});

$("#plus").click(function () {
    if (scale < 1.6) {
        scale += 0.2;
        $("#wrapper").css({ transform: `scale(${scale})` });
    }
});

$("#minus").click(function () {
    if (scale > 1) {
        scale -= 0.2;
        $("#wrapper").css({ transform: `scale(${scale})` });
    }
});

function setup() {
    cols = c.clientWidth / resolution;
    rows = c.clientHeight / resolution;

    grid = make2DArray(cols, rows);

    // initial pattern setup
    createPattern();
}

function createPattern() {

    for (let i = 0; i < cols; i++) {
        for (let j = 0; j < rows; j++) {
            var x = Math.floor(Math.random() * 2);
            grid[i][j] = x;
        }
    }
    plotData(grid);
}

function runSimulation() {
    getNextGeneratioDataFromData(grid, cols, rows);
}

function getNextGeneratioDataFromData(info, col, row) {
    var uri = "api/dataClient/getNextGeneratioDataFromData";
    var url = uri + "?col=" + col + "&row=" + row;
    var errorText = "Sorry, something went wrong. Please try again later.";

    try {
        fetch(url, {
            method: "POST",
            cache: "no-cache",
            credentials: "include",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(info)
        }).then((response) => {
            if (!response.ok) {
                throw new Error(errorText);
            }
            return response.json();
        }).then((data) => {
            //console.log(data);
            if (data == null || data == "") {
                console.log(errorText);
            }
            else {
                grid = data;
                plotData(grid);
            }
        });

    } catch (e) {
        console.log(e);
    }
}

function plotData(data) {

    ctx.fillStyle = "#F9F3CC";
    ctx.fillRect(0, 0, c.width, c.height);

    for (let i = 0; i < cols; i++) {
        for (let j = 0; j < rows; j++) {
            let x = i * resolution;
            let y = j * resolution;
            if (data[i][j] == 1) {
                ctx.fillStyle = "green";
                ctx.fillRect(x, y, resolution - 1, resolution - 1);
            }
        }
    }
}

document.addEventListener("DOMContentLoaded", (evt) => {
    setup();
});