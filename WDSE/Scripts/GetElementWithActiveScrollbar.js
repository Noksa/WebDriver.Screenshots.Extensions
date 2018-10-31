/**
 * @return {boolean}
 */
let IsElementHasScrollbar = function (element) {

    if (element.tagName === document.scrollingElement.tagName && typeof window.innerWidth === 'number') {
        return window.innerWidth > document.documentElement.clientWidth;
    }

    let overflowStyle;

    if (typeof element.currentStyle !== 'undefined')
        overflowStyle = element.currentStyle.overflow;

    overflowStyle = overflowStyle || window.getComputedStyle(element, '').overflow;

    let overflowYStyle;

    if (typeof element.currentStyle !== 'undefined')
        overflowYStyle = element.currentStyle.overflowY;

    overflowYStyle = overflowYStyle || window.getComputedStyle(element, '').overflowY;

    let contentOverflows = element.scrollHeight > element.clientHeight;
    let contentOverWindow = element.scrollHeight > $(window).height();
    let overflowShown = /^(visible|auto)$/.test(overflowStyle) || /^(visible|auto)$/.test(overflowYStyle);
    let alwaysShowScroll = overflowStyle === 'scroll' || overflowYStyle === 'scroll';

    return (contentOverflows && overflowShown && contentOverWindow) || (alwaysShowScroll)
};

let GetElementWithActiveScrollBar = function () {
    const elements = $("*");
    let elementsWithScrollBar = elements.filter(function () {
        return (IsElementHasScrollbar($(this)[0]));
    });
    if (elementsWithScrollBar.length === 0) return null;
    if (elementsWithScrollBar.first().get(0) === document.scrollingElement) return elementsWithScrollBar.get(0);
    if (elementsWithScrollBar.length === 1) return elementsWithScrollBar.get(0);
    const scrollBarsHeight = elementsWithScrollBar.map(function () {
        return $(this)[0].scrollHeight;
    });
    const scrollBarWithMaxHeight = Math.max(...scrollBarsHeight.toArray());
    elementsWithScrollBar = elementsWithScrollBar.filter(function () {
        if ($(this)[0].scrollHeight === scrollBarWithMaxHeight) return $(this)[0];
    });
    return elementsWithScrollBar.get(0);
};

return GetElementWithActiveScrollBar();