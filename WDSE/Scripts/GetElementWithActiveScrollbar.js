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
    let elementWithScroll = elements.filter(function () {
        return (IsElementHasScrollbar($(this)[0]));
    });
    if (elementWithScroll.length === 0) return null;
    if (elementWithScroll.first()[0] === document.scrollingElement) return elementWithScroll.get(0);
    if (elementWithScroll.length === 1) return elementWithScroll.get(0);
    let str = "";
    elementWithScroll.each(function () {
        str = str + $(this)[0].tagName + ", ";
    });
    if (str !== "") {
        const index = str.lastIndexOf(", ");
        str = str.substr(str, index);
    }
    throw new DOMException(
        `Cant find only one element with active scrollbar. Count of elements: ${elementWithScroll.length
        }. Elements tags: ${str
        }. Please create issue about it at https://github.com/Noksa/WebDriver.Screenshots.Extensions/issues`);
}

return GetElementWithActiveScrollBar();