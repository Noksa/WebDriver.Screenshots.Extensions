var  GetElementWithActiveScrollBar = function(elements) {
    var jQueryElements = $(elements);
    if (jQueryElements.length === 0) return null;
    if (jQueryElements.first().get(0) === document.scrollingElement) return jQueryElements.get(0);
    if (jQueryElements.length === 1) return jQueryElements.get(0);
    var scrollBarsHeight = jQueryElements.map(function() {
        return $(this)[0].scrollHeight;
    });
    var scrollBarWithMaxHeight = Math.max.apply(null, scrollBarsHeight.toArray());
    jQueryElements = jQueryElements.filter(function() {
        if ($(this)[0].scrollHeight === scrollBarWithMaxHeight) return $(this)[0];
    });
    return jQueryElements.get(0);
};

return GetElementWithActiveScrollBar(arguments[0]);