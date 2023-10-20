
var xArray = medication;
var yArray = frequency;


const data = [{
    x: xArray,
    y: yArray,
    type: "bar",
    orientation: "v",
    marker: { color: "#3A7DD5" },
}];

const layout = {
    title: "Medicine Consumption Frequency",
    xaxis: {
        showgrid: false, // Hide x-axis grid lines
    },
    yaxis: {
        showgrid: false, // Hide y-axis grid lines
    },
};
const config = {
    displayModeBar: false
};

Plotly.newPlot("myPlot", data, layout, config);