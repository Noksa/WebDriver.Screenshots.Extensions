var IsElementHasScrollbar = function(element) {

    if (element.tagName === document.scrollingElement.tagName && typeof window.innerWidth === "number") {
        var scrollHeight = Math.max(
            document.body.scrollHeight, document.documentElement.scrollHeight,
            document.body.offsetHeight, document.documentElement.offsetHeight,
            document.body.clientHeight, document.documentElement.clientHeight
        );

        return scrollHeight > document.documentElement.clientHeight;
    }

    var overflowStyle;

    if (typeof element.currentStyle !== "undefined")
        overflowStyle = element.currentStyle.overflow;

    overflowStyle = overflowStyle || window.getComputedStyle(element, "").overflow;

    var overflowYStyle;

    if (typeof element.currentStyle !== "undefined")
        overflowYStyle = element.currentStyle.overflowY;

    overflowYStyle = overflowYStyle || window.getComputedStyle(element, "").overflowY;

    var contentOverflows = element.scrollHeight > element.clientHeight;
    var contentOverWindow = element.scrollHeight > $(window).height();
    var overflowShown = /^(visible|auto)$/.test(overflowStyle) || /^(visible|auto)$/.test(overflowYStyle);
    var alwaysShowScroll = overflowStyle === "scroll" || overflowYStyle === "scroll";

    return (contentOverflows && overflowShown && contentOverWindow) || (alwaysShowScroll);
};

var GetAllElementsWithScrollbar = function() {
    var elements = $("*");
    var elementsWithScrollBar = elements.filter(function() {
        return (IsElementHasScrollbar($(this)[0]) && IsElementNeedToBeAdded($(this)[0]));
    });
    return elementsWithScrollBar.toArray();
};

var IsElementNeedToBeAdded = function(element) {
    var elementTagName = element.tagName.toLowerCase();
    if (elementTagName === "body" || elementTagName === "html") {
        var scrollingElementTag = document.scrollingElement.tagName.toLowerCase();
        return elementTagName === scrollingElementTag;
    } else return true;
};

return GetAllElementsWithScrollbar();