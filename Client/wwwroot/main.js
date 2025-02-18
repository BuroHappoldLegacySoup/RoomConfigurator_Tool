window.addEventListener('beforeunload', function (e) {
    e.preventDefault();
    e.returnValue = '';
});

window.setTitle = (title) => {
    document.title = title;
}

function updateVegaChart(chartId, specificationsFile, data) {
    fetch(specificationsFile)
        .then(res => res.json())
        .then(spec => renderVegaChart(chartId, spec, data))
        .catch(err => console.error(err));
};

function renderVegaChart(chartId, specifications, data) {
    for (var key in data) {
        var candidates = specifications.data.filter(x => x.name == key);
        if (candidates.length > 0)
            candidates[0].values = data[key];
    }

    var view = new vega.View(vega.parse(specifications), {
        renderer: 'svg',
        container: chartId,
        hover: true
    });

    return view.runAsync();
}
