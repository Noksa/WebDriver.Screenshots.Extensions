function GetElementWithActiveScrollBar() {
    const elements = $("*");
    let elementWithScrollBar = FilteringElements(elements);
    if (elementWithScrollBar.length === 0) return null;
    if (elementWithScrollBar.length === 1) return elementWithScrollBar[0];
    elementWithScrollBar = elementWithScrollBar.filter(function() {
        return $(this)[0].tagName.toLowerCase() !== "html";
    });
    if (elementWithScrollBar.length === 1) return elementWithScrollBar[0];
    let str = "";
    elementWithScrollBar.each(function() {
        str = str + $(this)[0].tagName + ", ";
    });
    if (str !== "") {
        const index = str.lastIndexOf(", ");
        str = str.substr(str, index);
    }
    throw new DOMException(
        `Cant find only one element with active scrollbar. Count of elements: ${elementWithScrollBar.length}. Elements tags: ${str}. Please create issue about it at https://github.com/Noksa/WebDriver.Screenshots.Extensions/issues`);
}

function FilteringElements(elements) {
    return $(elements).filter(function() {
        return $(this)[0].scrollHeight > $(this)[0].clientHeight &&
            $(this)[0].scrollHeight > $(this).innerHeight() && $(this)[0].scrollHeight > $(window).height() &&
            !$(this).css("overflow") !== "hidden";
    });

}

return GetElementWithActiveScrollBar();