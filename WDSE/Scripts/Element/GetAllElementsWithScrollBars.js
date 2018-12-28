/**
 * @return {boolean}
 */
let IsElementHasScrollbar = function(element) {

    if (element.tagName === document.scrollingElement.tagName && typeof window.innerWidth === "number") {
        return window.innerWidth > document.documentElement.clientWidth;
    }

    let overflowStyle;

    if (typeof element.currentStyle !== "undefined")
        overflowStyle = element.currentStyle.overflow;

    overflowStyle = overflowStyle || window.getComputedStyle(element, "").overflow;

    let overflowYStyle;

    if (typeof element.currentStyle !== "undefined")
        overflowYStyle = element.currentStyle.overflowY;

    overflowYStyle = overflowYStyle || window.getComputedStyle(element, "").overflowY;

    const contentOverflows = element.scrollHeight > element.clientHeight;
    const contentOverWindow = element.scrollHeight > $(window).height();
    const overflowShown = /^(visible|auto)$/.test(overflowStyle) || /^(visible|auto)$/.test(overflowYStyle);
    const alwaysShowScroll = overflowStyle === "scroll" || overflowYStyle === "scroll";

    return (contentOverflows && overflowShown && contentOverWindow) || (alwaysShowScroll);
};

let GetAllElementsWithScrollbar = function() {
    const elements = $("*");
    const elementsWithScrollBar = elements.filter(function() {
        return (IsElementHasScrollbar($(this)[0]) && IsElementNeedToBeAdded($(this)[0]));
    });
    return elementsWithScrollBar.toArray();
};

/**
 * @return {boolean}
 */
let IsElementNeedToBeAdded = function(element) {
    const elementTagName = element.tagName.toLowerCase();
    if (elementTagName === "body" || elementTagName === "html") {
        const scrollingElementTag = document.scrollingElement.tagName.toLowerCase();
        return elementTagName === scrollingElementTag;
    } else return true;
};

return GetAllElementsWithScrollbar();