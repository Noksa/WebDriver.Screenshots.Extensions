function GetElementWithActiveScrollBar() {
    if (IsDocumentHasScrollbar()) return document.scrollingElement;
    const elements = $("*");
    let elementWithScrollBar = FilteringElements(elements);
    elementWithScrollBar = elementWithScrollBar.filter(function () {
        return $(this)[0].tagName !== document.scrollingElement.tagName;
    });
    if (elementWithScrollBar.length == 1) return elementWithScrollBar[0];
    let str = "";
    elementWithScrollBar.each(function () {
        str = str + $(this)[0].tagName + ", ";
    });
    if (str !== "") {
        const index = str.lastIndexOf(", ");
        str = str.substr(str, index);
    }
    throw new DOMException(
        `Cant find only one element with active scrollbar. Count of elements: ${elementWithScrollBar.length
        }. Elements tags: ${str
        }. Please create issue about it at https://github.com/Noksa/WebDriver.Screenshots.Extensions/issues`);

}

function FilteringElements(elements) {
    return $(elements).filter(function () {
        return $(this)[0].scrollHeight > $(this)[0].clientHeight &&
            $(this)[0].scrollHeight > $(this).innerHeight() &&
            $(this)[0].scrollHeight > $(window).height() &&
            !$(this).css("overflow") !== "hidden";
    });

}

/**
 * @return {boolean}
 */
var IsDocumentHasScrollbar = function () {
    // The Modern solution
    if (typeof window.innerWidth === 'number')
        return window.innerWidth > document.documentElement.clientWidth

    // rootElem for quirksmode
    var rootElem = document.documentElement || document.body

    // Check overflow style property on body for fauxscrollbars
    var overflowStyle

    if (typeof rootElem.currentStyle !== 'undefined')
        overflowStyle = rootElem.currentStyle.overflow

    overflowStyle = overflowStyle || window.getComputedStyle(rootElem, '').overflow

    // Also need to check the Y axis overflow
    var overflowYStyle

    if (typeof rootElem.currentStyle !== 'undefined')
        overflowYStyle = rootElem.currentStyle.overflowY

    overflowYStyle = overflowYStyle || window.getComputedStyle(rootElem, '').overflowY

    var contentOverflows = rootElem.scrollHeight > rootElem.clientHeight
    var overflowShown = /^(visible|auto)$/.test(overflowStyle) || /^(visible|auto)$/.test(overflowYStyle)
    var alwaysShowScroll = overflowStyle === 'scroll' || overflowYStyle === 'scroll'

    return (contentOverflows && overflowShown) || (alwaysShowScroll)
}