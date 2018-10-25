function GetElementWithActiveScrollBar() {
    const elements = $("*");
    let elementWithScrollBar = FilteringElements(elements);
    if (elementWithScrollBar.length === 0) return null;
    if (elementWithScrollBar.length === 1) return elementWithScrollBar[0];
    elementWithScrollBar = elementWithScrollBar.slice(1);
    elementWithScrollBar = FilteringElements(elementWithScrollBar);
    if (elementWithScrollBar.length === 1) return elementWithScrollBar[0];
    throw new DOMException(
        "Cant find only one element with active scrollbar. Please create issue about it at https://github.com/Noksa/WebDriver.Screenshots.Extensions/issues");
}

function FilteringElements(elements) {
    return $(elements).filter(function() {
        return $(this)[0].scrollHeight > $(window).height() &&
            $(this)[0].scrollHeight > $(this)[0].clientHeight &&
            $(this)[0].scrollHeight > $(this).innerHeight() &&
            ($(this).css("overflow").includes("auto") ||
                $(this).css("overflow").includes("scroll") ||
                $(this).css("overflow").includes("visible"));
    });

}

return GetElementWithActiveScrollBar();