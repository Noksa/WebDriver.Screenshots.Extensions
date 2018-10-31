let GetElementWithActiveScrollBar = function(elements) {
    let jQueryElements = $(elements);
    if (jQueryElements.length === 0) return null;
    if (jQueryElements.first().get(0) === document.scrollingElement) return jQueryElements.get(0);
    if (jQueryElements.length === 1) return jQueryElements.get(0);
    const scrollBarsHeight = jQueryElements.map(function() {
        return $(this)[0].scrollHeight;
    });
    const scrollBarWithMaxHeight = Math.max(...scrollBarsHeight.toArray());
    jQueryElements = jQueryElements.filter(function() {
        if ($(this)[0].scrollHeight === scrollBarWithMaxHeight) return $(this)[0];
    });
    return jQueryElements.get(0);
};

return GetElementWithActiveScrollBar(arguments[0]);