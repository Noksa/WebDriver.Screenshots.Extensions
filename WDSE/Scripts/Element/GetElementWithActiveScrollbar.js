let GetElementWithActiveScrollBar = function(elements) {
    elements = $(elements);
    if (elements.length === 0) return null;
    if (elements.first().get(0) === document.scrollingElement) return elements.get(0);
    if (elements.length === 1) return elements.get(0);
    const scrollBarsHeight = elements.map(function() {
        return $(this)[0].scrollHeight;
    });
    const scrollBarWithMaxHeight = Math.max(...scrollBarsHeight.toArray());
    elements = elements.filter(function() {
        if ($(this)[0].scrollHeight === scrollBarWithMaxHeight) return $(this)[0];
    });
    return elements.get(0);
};

return GetElementWithActiveScrollBar();